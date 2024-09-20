using VZAggregator.Entities;

namespace VZAggregator.Interfaces.Repositories
{
    public interface IAddressesRepository
    {
        ///<summary>
        /// Gets address
        ///</summary>
        Task<Address> GetAddressAsync(int id);

        ///<summary>
        /// Gets categories list
        ///</summary>
        Task<Address[]> GetAddressesAsync();

        ///<summary>
        /// Creates address
        ///</summary>
        Task<bool> CreateAsync(Address cateogory);

        ///<summary>
        /// Updates address
        ///</summary>
        Task<bool> UpdateAsync(Address cateogory);

        ///<summary>
        /// Deletes address
        ///</summary>
        Task<bool> DeleteAsync(int id);
    }
}