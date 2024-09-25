using VZAggregator.Models;

namespace VZAggregator.Interfaces
{
    public interface IUsersRepository
    {
      ///<summary>
      /// Gets user
      ///</summary>
      Task<User> GetUserAsync(int id);

      ///<summary>
      /// Gets categories list
      ///</summary>
      Task<User[]> GetUsersAsync();

      ///<summary>
      /// Creates user
      ///</summary>
      Task<bool> CreateAsync(User cateogory);

      ///<summary>
      /// Updates user
      ///</summary>
      Task<bool> UpdateAsync(User cateogory);

      ///<summary>
      /// Deletes user
      ///</summary>
      Task<bool> DeleteAsync(int id);
    }
}