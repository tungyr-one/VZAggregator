using AutoMapper;
using VZAggregator.DTOs;
using VZAggregator.Models;
using VZAggregator.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;

namespace VZAggregator.Services
{
    public class UsersService:IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public UsersService(IUsersRepository usersRepository,
            IMapper mapper,
            UserManager<AppUser> userManager)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<UserDto> GetUserAsync(int id)
        {
            var user = await _usersRepository.GetUserAsync(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            var users = await _usersRepository.GetUsersAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);  
        }

        public async Task<bool> CreateAsync(UserDto newUser)
        {
            var userToDb = _mapper.Map<AppUser>(newUser);
            newUser.Created = DateTime.UtcNow;
            return await _usersRepository.CreateAsync(userToDb);
        }

        public async Task<UserDto> UpdateAsync(int id, UserUpdateDto userUpdate)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) throw new ArgumentException("User not found");
            _mapper.Map(userUpdate, user);
            user.Updated = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new ArgumentException("Failed to update user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            var updatedUserDto = _mapper.Map<UserDto>(user);
            return updatedUserDto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _usersRepository.DeleteAsync(id);
        }
    }
}