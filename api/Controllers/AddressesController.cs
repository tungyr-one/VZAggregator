using Microsoft.AspNetCore.Mvc;
using VZAggregator.DTOs;
using VZAggregator.Interfaces;
using VZAggregator.Interfaces.Repositories;

namespace VZAggregator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressesService _addresssService;

        public AddressesController(IAddressesService addresssService)
        {
            _addresssService = addresssService;
        }

        ///<summary>
        /// Gets address from service, if null returns NotFound
        ///</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<AddressDto>> GetAddress(int id)
        {
            return await _addresssService.GetAddressAsync(id) switch
            {
                null => NotFound(),
                var address => Ok(address)
            };
        }

        ///<summary>
        /// Gets addresss list
        ///</summary>
        [HttpGet]
        public async Task<ActionResult<AddressDto[]>> GetAddresses()
        {
            var addresss = await _addresssService.GetAddressesAsync();
            HttpContext.Response.StatusCode = 200;
            await HttpContext.Response.WriteAsJsonAsync(addresss);
            return new EmptyResult();
        }

        ///<summary>
        /// Creates address 
        ///</summary>
        [HttpPost("new")]
        public async Task<ActionResult> CreateAddressAsync(AddressDto newAddress)
        {
            if (await _addresssService.CreateAsync(newAddress)) return Ok();
            return BadRequest("Failed to create address");
        }

            ///<summary>
            /// Updates address 
            ///</summary>
        [HttpPut("{id}")]
        public Task Update(int id, AddressDto addressUpdate) => _addresssService.UpdateAsync(id, addressUpdate);

        ///<summary>
        /// Deletes address
        ///</summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAddressAsync(int id)
        {
            if (await _addresssService.DeleteAsync(id)) return Ok();
            return BadRequest("Failed to delete address");
        }
    }
}