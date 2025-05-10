
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ViaPlan.Services;
using ViaPlan.Services.Common;
using ViaPlan.Services.DTO;

namespace ViaPlan.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripsController : Controller
    {
        private readonly ILogger<TripsController> _logger;
        private readonly TripServices _tripServices;

        public TripsController(ILogger<TripsController> logger, TripServices tripServices)
        {
            _tripServices = tripServices;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            var result = await _tripServices.GetAllTripsAsync();

            if (!result.Success)
            {
                _logger.LogError("TripService error: {Message}", result.ErrorMessage);
                return StatusCode(500, result.ErrorMessage);
            }

            if (result.Data == null || !result.Data.Any())
            {
                return NotFound("No trips found.");
            }

            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrip(int id)
        {
            var result = await _tripServices.GetTripByIdAsync(id);

            if (!result.Success)
            {
                _logger.LogError("TripService error: {Message}", result.ErrorMessage);
                return StatusCode(500, result.ErrorMessage);
            }

            if (result.Data == null)
            {
                return NotFound("Trip not found.");
            }

            return Ok(result.Data);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateTrip([FromBody] CreateTripDTO tripDto)
        {
            if (tripDto == null)
            {
                return BadRequest("Trip data is required.");
            }

            var result = await _tripServices.CreateTripAsync(tripDto);

            if (!result.Success)
            {
                _logger.LogError("TripService error: {Message}", result.ErrorMessage);
                return StatusCode(500, result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrip(int id, [FromBody] CreateTripDTO tripDto)
        {
            if (tripDto == null)
            {
                return BadRequest("Trip data is required.");
            }

            var result = await _tripServices.UpdateTripAsync(id, tripDto);

            if (!result.Success)
            {
                _logger.LogError("TripService error: {Message}", result.ErrorMessage);
                return StatusCode(500, result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrip(int id)
        {
            var result = await _tripServices.DeleteTripAsync(id);

            if (!result.Success)
            {
                _logger.LogError("TripService error: {Message}", result.ErrorMessage);
                return StatusCode(500, result.ErrorMessage);
            }

            return Ok(result.Data);
        }
    }
}