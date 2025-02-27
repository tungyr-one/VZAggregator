using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs;
using VZAggregator.Models;

namespace VZAggregator.Interfaces.Repositories
{
    public interface ITripsRepository
    {
        ///<summary>
        /// Gets trip
        ///</summary>
        Task<Trip> GetTripAsync(int id);

        ///<summary>
        /// Gets categories list
        ///</summary>
        Task<Trip[]> GetTripsAsync(UserParams userParams);

        ///<summary>
        /// Creates trip
        ///</summary>
        Task<bool> CreateAsync(Trip cateogory);

        ///<summary>
        /// Updates trip
        ///</summary>
        Task<bool> UpdateAsync(Trip cateogory);

        ///<summary>
        /// Deletes trip
        ///</summary>
        Task<bool> DeleteAsync(int id);
    }
}