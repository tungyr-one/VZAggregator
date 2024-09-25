using Microsoft.EntityFrameworkCore;
using VZAggregator.Models;
using VZAggregator.Interfaces.Repositories;
using api.DTOs;
using System.Linq.Expressions;
using api.Helpers;

namespace VZAggregator.Data.Repositories
{
    public class TripsRepository:ITripsRepository
    {
        private readonly DataContext _context;

        public TripsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Trip> GetTripAsync(int id)
        {
            return await _context.Trips.AsNoTracking()         
            .FirstOrDefaultAsync(x => x.TripId == id);
        }


// {
//   "offset": 0,
//   "pageSize": 5,
//   "filterBy": "",
//   "sortBy": "name",
//   "sortOrder": "asc"
// }

        public async Task<Trip[]> GetTripsAsync(UserParams userParams)
        {

            userParams.SortBy = userParams.SortBy.CapitalizeFirstLetter();

            var query = _context.Trips
            .Include(t => t.Transport)
            .Include(t => t.Carrier)
            .Include(t => t.DepartureAddress)
            .Include(t => t.DestinationAddress)
            .AsQueryable();

            if(!string.IsNullOrWhiteSpace(userParams.FilterBy))
            {
                query = query.Where(t => t.Carrier.Name.StartsWith(userParams.FilterBy));
            }

            query = userParams.SortOrder == "asc"
            ? query.OrderBy(ResolveOrderFieldExpression(userParams))
            : query.OrderByDescending(ResolveOrderFieldExpression(userParams));

            return await query.AsNoTracking().ToArrayAsync();
        }

        public async Task<bool> CreateAsync(Trip trip)
        {
           _context.Trips.
           Add(trip).State = EntityState.Added;
           return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Trip trip)
        {            
           _context.Entry(trip).State = EntityState.Modified;
           return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tripToDelete = await _context.Trips.FindAsync(id);
            _context.Entry(tripToDelete).State = EntityState.Deleted; 
            return await _context.SaveChangesAsync() > 0;
        }

        private static Expression<Func<Trip, object>> ResolveOrderFieldExpression(UserParams userParams)
        => userParams.SortBy switch
        {
            nameof(Trip.Carrier.Name) => x => x.Carrier.Name,
            nameof(Trip.DepartureAddress.City) => x => x.DepartureAddress.City,
            nameof(Trip.Created) => x => x.Created,
            nameof(Trip.TripDateTime) => x => x.TripDateTime,
            nameof(Trip.TripPrice) => x => x.TripPrice,
            nameof(Trip.TripType) => x => x.TripType,
            _ => x => x.TripId
        };
    }
}