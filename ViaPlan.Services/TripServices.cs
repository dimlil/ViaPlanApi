﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ViaPlan.Data;
using ViaPlan.Entities;
using ViaPlan.Services.Common;
using ViaPlan.Services.DTO;
using ViaPlan.Services.External.HotelApi;
using ViaPlan.Services.External.WeatherApi;

namespace ViaPlan.Services;

public class TripServices
{
    private readonly ViaPlanContext _context;
    private readonly IMapper _mapper;
    private readonly OpenWeatherServices _openWeatherServices;
    private readonly HotelService _hotelService;
    public TripServices(ViaPlanContext context, IMapper mapper, OpenWeatherServices openWeatherServices, HotelService hotelService)
    {
        _context = context;
        _mapper = mapper;
        _openWeatherServices = openWeatherServices;
        _hotelService = hotelService;
    }

    public async Task<ServiceResult<List<TripDTO>>> GetAllTripsAsync()
    {
        try
        {
            var trips = await _context.Trips
            .Include(t => t.User)
            .ToListAsync();
            return new ServiceResult<List<TripDTO>> { Success = true, Data = trips.Select(t => _mapper.Map<TripDTO>(t)).ToList() };
        }
        catch (Exception ex)
        {
            return new ServiceResult<List<TripDTO>> { Success = false, ErrorMessage = ex.Message };
        }
    }

    public async Task<ServiceResult<TripDTO>> GetTripByIdAsync(int id)
    {
        try
        {
            var trip = await _context.Trips
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (trip == null)
            {
                return new ServiceResult<TripDTO> { Success = false, ErrorMessage = "Trip not found." };
            }

            return new ServiceResult<TripDTO> { Success = true, Data = _mapper.Map<TripDTO>(trip) };
        }
        catch (Exception ex)
        {
            return new ServiceResult<TripDTO> { Success = false, ErrorMessage = ex.Message };
        }
    }

    public async Task<ServiceResult<TripDTO>> CreateTripAsync(CreateTripDTO createTripDTO)
    {
        try
        {
            var trip = _mapper.Map<Trip>(createTripDTO);
            trip.User = await _context.Users.Where(u => u.Id == createTripDTO.UserId).FirstOrDefaultAsync();

            if (trip.User == null)
                return ServiceResult<TripDTO>.Failure("User not found");

            var getLatAndLonResult = await _openWeatherServices.GetLocationAsync(trip.Destination);
            var getSummaryResult = await _openWeatherServices.GetWeatherDataAsync(getLatAndLonResult.lat, getLatAndLonResult.lon);

            trip.WeatherSummary = getSummaryResult.daily.FirstOrDefault()?.weather.FirstOrDefault()?.description;

            var hotel = await _hotelService.GetHotelAsync(getLatAndLonResult.lat, getLatAndLonResult.lon);

            trip.HotelRecommendation = hotel?.display_name;

            await _context.Trips.AddAsync(trip);
            await _context.SaveChangesAsync();
            return new ServiceResult<TripDTO> { Success = true, Data = _mapper.Map<TripDTO>(trip) };
        }
        catch (Exception ex)
        {
            return new ServiceResult<TripDTO> { Success = false, ErrorMessage = ex.Message };
        }

    }

    public async Task<ServiceResult<TripDTO>> UpdateTripAsync(int id, CreateTripDTO tripDTO)
    {
        try
        {
            var trip = await _context.Trips.FindAsync(id);
            if (trip == null)
            {
                return new ServiceResult<TripDTO> { Success = false, ErrorMessage = "Trip not found." };
            }

            _mapper.Map(tripDTO, trip);
            await _context.SaveChangesAsync();
            return new ServiceResult<TripDTO> { Success = true, Data = _mapper.Map<TripDTO>(trip) };
        }
        catch (Exception ex)
        {
            return new ServiceResult<TripDTO> { Success = false, ErrorMessage = ex.Message };
        }
    }

    public async Task<ServiceResult<bool>> DeleteTripAsync(int id)
    {
        try
        {
            var trip = await _context.Trips.FindAsync(id);
            if (trip == null)
            {
                return new ServiceResult<bool> { Success = false, ErrorMessage = "Trip not found." };
            }

            _context.Trips.Remove(trip);
            await _context.SaveChangesAsync();
            return new ServiceResult<bool> { Success = true, Data = true };
        }
        catch (Exception ex)
        {
            return new ServiceResult<bool> { Success = false, ErrorMessage = ex.Message };
        }
    }
}
