using VZAggregator.DTOs;

namespace VZAggregator.Interfaces.Repositories
{
    public interface IAddressesService
    {
         ///<summary>
        /// Gets address
        ///</summary>
        Task<IEnumerable<AddressDto>> GetAddressesAsync();

        ///<summary>
        /// Gets addresses list
        ///</summary>
        Task<AddressDto> GetAddressAsync(int id);

        ///<summary>
        /// Creates address
        ///</summary>
        Task<bool> CreateAsync(AddressDto address);

        ///<summary>
        /// Updates address
        ///</summary>
        Task<bool> UpdateAsync(int id, AddressDto address);

        ///<summary>
        /// Deletes address
        ///</summary>
        Task<bool> DeleteAsync(int id);
    }
}