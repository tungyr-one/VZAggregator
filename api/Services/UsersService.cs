using AutoMapper;
using VZAggregator.DTOs;
using VZAggregator.Entities;
using VZAggregator.Interfaces;

namespace VZAggregator.Services
{
    public class UsersService:IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public UsersService(IUsersRepository usersRepository,
            IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
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
            var userToDb = _mapper.Map<User>(newUser);
            newUser.Created = DateTime.UtcNow;
            return await _usersRepository.CreateAsync(userToDb);
        }

        public async Task<bool> UpdateAsync(int id, UserDto userUpdate)
        {
            var userDb = await _usersRepository.GetUserAsync(id);
            _mapper.Map(userUpdate, userDb);
            userUpdate.Updated = DateTime.UtcNow;
            if(!await _usersRepository.UpdateAsync(userDb)) 
            {
                throw new ArgumentException("Failed to update user");
            }
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _usersRepository.DeleteAsync(id);
        }
    }
}