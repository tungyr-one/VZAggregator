using Microsoft.EntityFrameworkCore;
using VZAggregator.Entities;
using VZAggregator.Interfaces.Repositories;

namespace VZAggregator.Data.Repositories
{
    public class TransportsRepository:ITransportsRepository
    {
        private readonly DataContext _context;

        public TransportsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Transport> GetTransportAsync(int id)
        {
            return await _context.Transports.AsNoTracking()         
            .FirstOrDefaultAsync(x => x.TransportId == id);
        }

        public async Task<Transport[]> GetTransportsAsync()
        {
            var query = _context.Transports.AsQueryable();
            return await query.AsNoTracking().ToArrayAsync();
        }

        public async Task<bool> CreateAsync(Transport transport)
        {
           _context.Transports.
           Add(transport).State = EntityState.Added;
           return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Transport transport)
        {            
           _context.Entry(transport).State = EntityState.Modified;
           return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var transportToDelete = await _context.Transports.FindAsync(id);
            _context.Entry(transportToDelete).State = EntityState.Deleted; 
            return await _context.SaveChangesAsync() > 0;
        }
    }
}