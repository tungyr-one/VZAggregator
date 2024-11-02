using VZAggregator.DTOs;

namespace VZAggregator.Interfaces
{
    public interface IUsersService
    {
         ///<summary>
        /// Gets user
        ///</summary>
        Task<IEnumerable<UserDto>> GetUsersAsync();

        ///<summary>
        /// Gets users list
        ///</summary>
        Task<UserDto> GetUserAsync(int id);

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