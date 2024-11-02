using Microsoft.AspNetCore.Mvc;
using VZAggregator.DTOs;
using VZAggregator.Interfaces;

namespace VZAggregator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        ///<summary>
        /// Gets user from service, if null returns NotFound
        ///</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            return await _usersService.GetUserAsync(id) switch
            {
                null => NotFound(),
                var user => Ok(user)
            };
        }

        ///<summary>
        /// Gets users list
        ///</summary>
        [HttpGet]
        public async Task<ActionResult<UserDto[]>> GetUsers()
        {
            var users = await _usersService.GetUsersAsync();
            HttpContext.Response.StatusCode = 200;
            await HttpContext.Response.WriteAsJsonAsync(users);
            return new EmptyResult();
        }

        ///<summary>
        /// Creates user 
        ///</summary>
        [HttpPost("new")]
        public async Task<ActionResult> CreateUserAsync(UserDto newUser)
        {
            if (await _usersService.CreateAsync(newUser)) return Ok();
            return BadRequest("Failed to create user");
        }

        ///<summary>
        /// Updates user 
        ///</summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> Update(int id, UserUpdateDto userUpdate)
        {
            return await _usersService.UpdateAsync(id, userUpdate) switch
            {
                null => BadRequest(),
                var user => Ok(user)
            };
        }

        ///<summary>
        /// Deletes user
        ///</summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserAsync(int id)
        {
            if (await _usersService.DeleteAsync(id)) return Ok();
            return BadRequest("Failed to delete user");
        }

        ///<summary>
        /// Sets users subscription
        ///</summary>
        [HttpPut("subs/{id}")]
        public async Task<ActionResult> SetUrersSubscriptionAsync(int id)
        {

            if (await _usersService.DeleteAsync(id)) return Ok();
            return BadRequest("Failed to delete user");
        }
    }
}