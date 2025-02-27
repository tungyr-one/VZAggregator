using api.DTOs;
using api.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VZAggregator.DTOs;
using VZAggregator.Interfaces.Repositories;

namespace VZAggregator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripsController : ControllerBase
    {
        private readonly ITripsService _tripsService;

        public TripsController(ITripsService tripsService)
        {
            _tripsService = tripsService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminOnly()
        {
            return Ok();
        }

        ///<summary>
        /// Gets trip from service, if null returns NotFound
        ///</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<TripDto>> GetTrip(int id)
        {
            return await _tripsService.GetTripAsync(id) switch
            {
                null => NotFound(),
                var trip => Ok(trip)
            };
        }

        ///<summary>
        /// Gets trips list
        ///</summary>
        [HttpPost]
        public async Task<ActionResult<Pagination<TripDto>>> GetTrips(UserParams userParams)
        {
            var trips = await _tripsService.GetTripsAsync(userParams);
            return Ok(trips);
        }

        ///<summary>
        /// Creates trip 
        ///</summary>
        [HttpPost("new")]
        public async Task<ActionResult> CreateTripAsync(TripDto newTrip)
        {
            if (await _tripsService.CreateAsync(newTrip)) return Ok();
            return BadRequest("Failed to create trip");
        }

        ///<summary>
        /// Updates trip 
        ///</summary>
        [HttpPut("{id}")]
        public Task Update(int id, TripDto tripUpdate) => _tripsService.UpdateAsync(id, tripUpdate);

        ///<summary>
        /// Deletes trip
        ///</summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTripAsync(int id)
        {
            if (await _tripsService.DeleteAsync(id)) return Ok();
            return BadRequest("Failed to delete trip");
        }
    }
}