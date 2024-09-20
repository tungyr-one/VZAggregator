using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VZAggregator.Entities;

namespace VZAggregator.Interfaces.Repositories
{
    public interface IOrdersRepository
    {
        ///<summary>
        /// Gets order
        ///</summary>
        Task<Order> GetOrderAsync(int id);

        ///<summary>
        /// Gets categories list
        ///</summary>
        Task<Order[]> GetOrdersAsync();

        ///<summary>
        /// Creates order
        ///</summary>
        Task<bool> CreateAsync(Order cateogory);

        ///<summary>
        /// Updates order
        ///</summary>
        Task<bool> UpdateAsync(Order cateogory);

        ///<summary>
        /// Deletes order
        ///</summary>
        Task<bool> DeleteAsync(int id);
    }
}