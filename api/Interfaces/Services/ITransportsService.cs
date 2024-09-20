using VZAggregator.DTOs;

namespace VZAggregator.Interfaces.Repositories
{
    public interface ITransportsService
    {
         ///<summary>
        /// Gets transport
        ///</summary>
        Task<IEnumerable<TransportDto>> GetTransportsAsync();

        ///<summary>
        /// Gets transportes list
        ///</summary>
        Task<TransportDto> GetTransportAsync(int id);

        ///<summary>
        /// Creates transport
        ///</summary>
        Task<bool> CreateAsync(TransportDto transport);

        ///<summary>
        /// Updates transport
        ///</summary>
        Task<bool> UpdateAsync(int id, TransportDto transport);

        ///<summary>
        /// Deletes transport
        ///</summary>
        Task<bool> DeleteAsync(int id);
    }
}