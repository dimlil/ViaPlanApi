using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ViaPlan.Data;
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
}
