using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VZAggregator.DTOs;

namespace VZAggregator.Interfaces.Repositories
{
    public interface ITripsService
    {
         ///<summary>
        /// Gets trip
        ///</summary>
        Task<IEnumerable<TripDto>> GetTripsAsync();

        ///<summary>
        /// Gets tripes list
        ///</summary>
        Task<TripDto> GetTripAsync(int id);

        ///<summary>
        /// Creates trip
        ///</summary>
        Task<bool> CreateAsync(TripDto trip);

        ///<summary>
        /// Updates trip
        ///</summary>
        Task<bool> UpdateAsync(int id, TripDto trip);

        ///<summary>
        /// Deletes trip
        ///</summary>
        Task<bool> DeleteAsync(int id);
    }
}