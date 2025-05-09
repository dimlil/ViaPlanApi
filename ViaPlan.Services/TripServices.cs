using Microsoft.EntityFrameworkCore;
using ViaPlan.Data;
using ViaPlan.Entities;

namespace ViaPlan.Services;

public class TripServices
{
    private readonly ViaPlanContext _context;
    public TripServices(ViaPlanContext context)
    {
        _context = context;
    }
    public async Task<List<Trip>> GetAllTripsAsync()
    {
        return await _context.Trips.ToListAsync();
    }
}
