using Microsoft.AspNetCore.Mvc;
using VZAggregator.DTOs;
using VZAggregator.Interfaces;
using VZAggregator.Interfaces.Repositories;

namespace VZAggregator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarriersController : ControllerBase
    {
        private readonly ICarriersService _CarriersService;

        public CarriersController(ICarriersService CarriersService)
        {
            _CarriersService = CarriersService;
        }

            ///<summary>
            /// Gets Carrier from service, if null returns NotFound
            ///</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<CarrierDto>> GetCarrier(int id)
        {
            return await _CarriersService.GetCarrierAsync(id) switch
            {
                null => NotFound(),
                var Carrier => Ok(Carrier)
            };
        }

        ///<summary>
        /// Gets Carriers list
        ///</summary>
        [HttpGet]
        public async Task<ActionResult<CarrierDto[]>> GetCarriers()
        {
            var Carriers = await _CarriersService.GetCarriersAsync();
            HttpContext.Response.StatusCode = 200;
            await HttpContext.Response.WriteAsJsonAsync(Carriers);
            return new EmptyResult();
        }

        ///<summary>
        /// Creates Carrier 
        ///</summary>
        [HttpPost("new")]
        public async Task<ActionResult> CreateCarrierAsync(CarrierDto newCarrier)
        {
            if (await _CarriersService.CreateAsync(newCarrier)) return Ok();
            return BadRequest("Failed to create Carrier");
        }

            ///<summary>
            /// Updates Carrier 
            ///</summary>
        [HttpPut("{id}")]
        public Task Update(int id, CarrierDto CarrierUpdate) => _CarriersService.UpdateAsync(id, CarrierUpdate);

        ///<summary>
        /// Deletes Carrier
        ///</summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCarrierAsync(int id)
        {
            if (await _CarriersService.DeleteAsync(id)) return Ok();
            return BadRequest("Failed to delete Carrier");
        }
    }
}