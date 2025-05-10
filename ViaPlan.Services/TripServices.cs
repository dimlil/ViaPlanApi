using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ViaPlan.Data;
using ViaPlan.Entities;
using ViaPlan.Services.Common;
using ViaPlan.Services.DTO;

namespace ViaPlan.Services;

public class TripServices
{
    private readonly ViaPlanContext _context;
    private readonly IMapper _mapper;
    public TripServices(ViaPlanContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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
}
