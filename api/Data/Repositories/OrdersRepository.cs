using Microsoft.EntityFrameworkCore;
using VZAggregator.Models;
using VZAggregator.Interfaces.Repositories;

namespace VZAggregator.Data.Repositories
{
    public class OrdersRepository:IOrdersRepository
    {
        private readonly DataContext _context;

        public OrdersRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Order> GetOrderAsync(int id)
        {
            return await _context.Orders.AsNoTracking()         
            .FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<Order[]> GetOrdersAsync()
        {
            var query = _context.Orders.AsQueryable();
            return await query.AsNoTracking().ToArrayAsync();
        }

        public async Task<bool> CreateAsync(Order order)
        {
           _context.Orders.
           Add(order).State = EntityState.Added;
           return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Order order)
        {            
           _context.Entry(order).State = EntityState.Modified;
           return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var orderToDelete = await _context.Orders.FindAsync(id);
            _context.Entry(orderToDelete).State = EntityState.Deleted; 
            return await _context.SaveChangesAsync() > 0;
        }
    }
}