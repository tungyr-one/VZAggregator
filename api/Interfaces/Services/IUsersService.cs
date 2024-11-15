using VZAggregator.DTOs;

namespace VZAggregator.Interfaces
{
    public interface IUsersService
    {
         ///<summary>
        /// Gets users list
        ///</summary>
        Task<IEnumerable<UserDto>> GetUsersAsync();

        ///<summary>
        /// Gets user by id
        ///</summary>
        Task<UserDto> GetUserAsync(int id);

        ///<summary>
        /// Gets user by name
        ///</summary>
        Task<UserDto> GetUserByNameAsync(string name);

        ///<summary>
        /// Creates user
        ///</summary>
        Task<bool> CreateAsync(UserDto user);

        ///<summary>
        /// Updates user
        ///</summary>
        Task<UserDto> UpdateAsync(int id, UserUpdateDto user);

        ///<summary>
        /// Deletes user
        ///</summary>
        Task<bool> DeleteAsync(int id);
    }
}