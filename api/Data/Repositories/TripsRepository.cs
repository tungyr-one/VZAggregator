using Microsoft.EntityFrameworkCore;
using VZAggregator.Entities;
using VZAggregator.Interfaces.Repositories;

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

        public async Task<Trip[]> GetTripsAsync()
        {
            var query = _context.Trips.AsQueryable();
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
    }
}