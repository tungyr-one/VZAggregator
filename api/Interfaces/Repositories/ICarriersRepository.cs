using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VZAggregator.Models;

namespace VZAggregator.Interfaces.Repositories
{
    public interface ICarriersRepository
    {
        ///<summary>
        /// Gets Carrier
        ///</summary>
        Task<Carrier> GetCarrierAsync(int id);

        ///<summary>
        /// Gets categories list
        ///</summary>
        Task<Carrier[]> GetCarriersAsync();

        ///<summary>
        /// Creates Carrier
        ///</summary>
        Task<bool> CreateAsync(Carrier cateogory);

        ///<summary>
        /// Updates Carrier
        ///</summary>
        Task<bool> UpdateAsync(Carrier cateogory);

        ///<summary>
        /// Deletes Carrier
        ///</summary>
        Task<bool> DeleteAsync(int id);
    }
}