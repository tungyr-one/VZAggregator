using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VZAggregator.DTOs;

namespace VZAggregator.Interfaces.Repositories
{
    public interface IOrdersService
    {
         ///<summary>
        /// Gets order
        ///</summary>
        Task<IEnumerable<OrderDto>> GetOrdersAsync();

        ///<summary>
        /// Gets orderes list
        ///</summary>
        Task<OrderDto> GetOrderAsync(int id);

        ///<summary>
        /// Creates order
        ///</summary>
        Task<bool> CreateAsync(OrderDto order);

        ///<summary>
        /// Updates order
        ///</summary>
        Task<bool> UpdateAsync(int id, OrderDto order);

        ///<summary>
        /// Deletes order
        ///</summary>
        Task<bool> DeleteAsync(int id);
    }
}