
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ViaPlan.Services;
using ViaPlan.Services.Common;

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
    }
}