using VZAggregator.Entities;

namespace VZAggregator.Interfaces.Repositories
{
    public interface ITransportsRepository
    {
                ///<summary>
        /// Gets transport
        ///</summary>
        Task<Transport> GetTransportAsync(int id);

        ///<summary>
        /// Gets categories list
        ///</summary>
        Task<Transport[]> GetTransportsAsync();

        ///<summary>
        /// Creates transport
        ///</summary>
        Task<bool> CreateAsync(Transport cateogory);

        ///<summary>
        /// Updates transport
        ///</summary>
        Task<bool> UpdateAsync(Transport cateogory);

        ///<summary>
        /// Deletes transport
        ///</summary>
        Task<bool> DeleteAsync(int id);
    }
}