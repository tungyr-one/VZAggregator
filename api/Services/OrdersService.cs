using AutoMapper;
using VZAggregator.DTOs;
using VZAggregator.Entities;
using VZAggregator.Interfaces;
using VZAggregator.Interfaces.Repositories;

namespace VZAggregator.Services
{
    public class OrdersService:IOrdersService
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IMapper _mapper;

        public OrdersService(IOrdersRepository ordersRepository,
            IMapper mapper)
        {
            _ordersRepository = ordersRepository;
            _mapper = mapper;
        }
        public async Task<OrderDto> GetOrderAsync(int id)
        {
            var order = await _ordersRepository.GetOrderAsync(id);
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersAsync()
        {
            var orders = await _ordersRepository.GetOrdersAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);  
        }

        public async Task<bool> CreateAsync(OrderDto newOrder)
        {
            var orderToDb = _mapper.Map<Order>(newOrder);
            return await _ordersRepository.CreateAsync(orderToDb);
        }

        public async Task<bool> UpdateAsync(int id, OrderDto orderUpdate)
        {
            var orderDb = await _ordersRepository.GetOrderAsync(id);
            _mapper.Map(orderUpdate, orderDb);
            if(!await _ordersRepository.UpdateAsync(orderDb)) 
            {
                throw new ArgumentException("Failed to update order");
            }
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _ordersRepository.DeleteAsync(id);
        }
    }
}