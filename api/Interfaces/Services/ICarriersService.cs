using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VZAggregator.DTOs;

namespace VZAggregator.Interfaces.Repositories
{
    public interface ICarriersService
    {
         ///<summary>
        /// Gets Carrier
        ///</summary>
        Task<IEnumerable<CarrierDto>> GetCarriersAsync();

        ///<summary>
        /// Gets Carrieres list
        ///</summary>
        Task<CarrierDto> GetCarrierAsync(int id);

        ///<summary>
        /// Creates Carrier
        ///</summary>
        Task<bool> CreateAsync(CarrierDto Carrier);

        ///<summary>
        /// Updates Carrier
        ///</summary>
        Task<bool> UpdateAsync(int id, CarrierDto Carrier);

        ///<summary>
        /// Deletes Carrier
        ///</summary>
        Task<bool> DeleteAsync(int id);
    }
}