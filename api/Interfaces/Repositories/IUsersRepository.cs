using api.Models;
using VZAggregator.Models;

namespace VZAggregator.Interfaces
{
    public interface IUsersRepository
    {
      ///<summary>
      /// Gets user by id
      ///</summary>
      Task<AppUser> GetUserAsync(int id);

      ///<summary>
      /// Gets user by name
      ///</summary>
      Task<AppUser> GetUserByNameAsync(string userName);

      ///<summary>
      /// Gets categories list
      ///</summary>
      Task<AppUser[]> GetUsersAsync();

      ///<summary>
      /// Creates user
      ///</summary>
      Task<bool> CreateAsync(AppUser cateogory);

      ///<summary>
      /// Updates user
      ///</summary>
      Task<bool> UpdateAsync(AppUser cateogory);

      ///<summary>
      /// Deletes user
      ///</summary>
      Task<bool> DeleteAsync(int id);
    }
}