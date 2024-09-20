using Microsoft.AspNetCore.Mvc;
using VZAggregator.DTOs;
using VZAggregator.Interfaces;
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
        [HttpGet]
        public async Task<ActionResult<TripDto[]>> GetTrips()
        {
            var trips = await _tripsService.GetTripsAsync();
            HttpContext.Response.StatusCode = 200;
            await HttpContext.Response.WriteAsJsonAsync(trips);
            return new EmptyResult();
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