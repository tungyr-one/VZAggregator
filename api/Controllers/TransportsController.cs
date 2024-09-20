using Microsoft.AspNetCore.Mvc;
using VZAggregator.DTOs;
using VZAggregator.Interfaces;
using VZAggregator.Interfaces.Repositories;

namespace VZAggregator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransportsController : ControllerBase
    {
        private readonly ITransportsService _transportsService;

        public TransportsController(ITransportsService transportsService)
        {
            _transportsService = transportsService;
        }

            ///<summary>
            /// Gets transport from service, if null returns NotFound
            ///</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<TransportDto>> GetTransport(int id)
        {
            return await _transportsService.GetTransportAsync(id) switch
            {
                null => NotFound(),
                var transport => Ok(transport)
            };
        }

        ///<summary>
        /// Gets transports list
        ///</summary>
        [HttpGet]
        public async Task<ActionResult<TransportDto[]>> GetTransports()
        {
            var transports = await _transportsService.GetTransportsAsync();
            HttpContext.Response.StatusCode = 200;
            await HttpContext.Response.WriteAsJsonAsync(transports);
            return new EmptyResult();
        }

        ///<summary>
        /// Creates transport 
        ///</summary>
        [HttpPost("new")]
        public async Task<ActionResult> CreateTransportAsync(TransportDto newTransport)
        {
            if (await _transportsService.CreateAsync(newTransport)) return Ok();
            return BadRequest("Failed to create transport");
        }

            ///<summary>
            /// Updates transport 
            ///</summary>
        [HttpPut("{id}")]
        public Task Update(int id, TransportDto transportUpdate) => _transportsService.UpdateAsync(id, transportUpdate);

        ///<summary>
        /// Deletes transport
        ///</summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTransportAsync(int id)
        {
            if (await _transportsService.DeleteAsync(id)) return Ok();
            return BadRequest("Failed to delete transport");
        }
    }
}