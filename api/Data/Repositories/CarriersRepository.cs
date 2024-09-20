using Microsoft.EntityFrameworkCore;
using VZAggregator.Entities;
using VZAggregator.Interfaces.Repositories;

namespace VZAggregator.Data.Repositories
{
    public class CarriersRepository:ICarriersRepository
    {

        private readonly DataContext _context;

        public CarriersRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Carrier> GetCarrierAsync(int id)
        {
            return await _context.Carriers.AsNoTracking()         
            .FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<Carrier[]> GetCarriersAsync()
        {
            var query = _context.Carriers.AsQueryable();
            return await query.AsNoTracking().ToArrayAsync();
        }

        public async Task<bool> CreateAsync(Carrier Carrier)
        {
           _context.Carriers.
           Add(Carrier).State = EntityState.Added;
           return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Carrier Carrier)
        {            
           _context.Entry(Carrier).State = EntityState.Modified;
           return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var CarrierToDelete = await _context.Carriers.FindAsync(id);
            _context.Entry(CarrierToDelete).State = EntityState.Deleted; 
            return await _context.SaveChangesAsync() > 0;
        }
    }
}