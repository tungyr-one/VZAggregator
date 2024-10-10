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

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, IMapper mapper)
        {
            this._mapper = mapper;
            this._userManager = userManager;
            this._tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username)) return BadRequest(new { message = "Username has already taken"});
            
            var user = _mapper.Map<AppUser>(registerDto);

            user.Name = registerDto.Username.ToLower();

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

            return Ok(new { message = "Registration successful"});
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users
            .SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

            if (user == null) return Unauthorized(new { message = "Invalid username" });
            
            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if(!result) return Unauthorized(new { message = "Invalid password" });

            return new UserDto
            {
                Name = user.Name,
                Token = await _tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}