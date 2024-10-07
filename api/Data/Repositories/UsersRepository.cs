using Microsoft.EntityFrameworkCore;
using VZAggregator.Models;
using VZAggregator.Interfaces;
using api.Models;

namespace VZAggregator.Data.Repositories
{
    public class UsersRepository: IUsersRepository
    {
        private readonly DataContext _context;

        public UsersRepository(DataContext context)
        {
            _context = context;
        }

       public async Task<AppUser> GetUserAsync(int id)
        {
            return await _context.Users.AsNoTracking()
            .Include(u => u.Addresses)           
            .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<AppUser[]> GetUsersAsync()
        {
            var query = _context.Users.Include(d => d.Addresses).AsQueryable();
            return await query.AsNoTracking().ToArrayAsync();
        }

        public async Task<bool> CreateAsync(AppUser user)
        {
           _context.Users.
           Add(user).State = EntityState.Added;
           return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(AppUser user)
        {            
           _context.Entry(user).State = EntityState.Modified;
           return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var userToDelete = await _context.Users.FindAsync(id);
            _context.Entry(userToDelete).State = EntityState.Deleted; 
            return await _context.SaveChangesAsync() > 0;
        }
    }
}