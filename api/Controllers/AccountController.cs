using api.DTOs;
using api.Interfaces;
using api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VZAggregator.DTOs;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController:ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, 
                                    ITokenService tokenService, 
                                    IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username)) return BadRequest(new { message = "Username has already taken"});
            
            var user = _mapper.Map<AppUser>(registerDto);

            user.UserName = registerDto.Username.ToLower();

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if(!result.Succeeded)
            {
                var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
                return BadRequest(new { Message = errorMessages });
            } 

            var roleResult = await _userManager.AddToRoleAsync(user, "Member");

            if(!roleResult.Succeeded)            
            {
                var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
                return BadRequest(new { Message = errorMessages });
            } 

            var newUser = _mapper.Map<UserDto>(user);
            newUser.Token = await _tokenService.CreateToken(user);

            return Ok(newUser);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {

            var user = await _userManager.Users
            .SingleOrDefaultAsync(x => x.UserName == loginDto.Username);
            
            if (user == null) return Unauthorized(new { message = "Invalid username" });

            if (await _userManager.IsLockedOutAsync(user))
            {
                return Unauthorized(new { message = "Account locked due to too many failed attempts. Try again later." });
            }
                       
            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if(!result)
            {
                await _userManager.AccessFailedAsync(user);
                return Unauthorized(new { message = "Invalid password" });
            }

            var loggedUser = _mapper.Map<UserDto>(user);
            loggedUser.UserRoles = await _userManager.GetRolesAsync(user);

            loggedUser.Token = await _tokenService.CreateToken(user);

            return Ok(loggedUser);
        }

        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}