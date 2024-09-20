using Microsoft.EntityFrameworkCore;
using VZAggregator.Entities;
using VZAggregator.Interfaces.Repositories;

namespace VZAggregator.Data.Repositories
{
    public class AddressesRepository:IAddressesRepository
    {
         private readonly DataContext _context;

        public AddressesRepository(DataContext context)
        {
            _context = context;
        }

       public async Task<Address> GetAddressAsync(int id)
        {
            return await _context.Addresses.AsNoTracking()         
            .FirstOrDefaultAsync(x => x.AddressId == id);
        }

        public async Task<Address[]> GetAddressesAsync()
        {
            var query = _context.Addresses.AsQueryable();
            return await query.AsNoTracking().ToArrayAsync();
        }

        public async Task<bool> CreateAsync(Address adress)
        {
           _context.Addresses.
           Add(adress).State = EntityState.Added;
           return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Address adress)
        {            
           _context.Entry(adress).State = EntityState.Modified;
           return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var adressToDelete = await _context.Addresses.FindAsync(id);
            _context.Entry(adressToDelete).State = EntityState.Deleted; 
            return await _context.SaveChangesAsync() > 0;
        }
    }
}