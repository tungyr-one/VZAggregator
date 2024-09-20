using AutoMapper;
using VZAggregator.DTOs;
using VZAggregator.Entities;
using VZAggregator.Interfaces;
using VZAggregator.Interfaces.Repositories;

namespace VZAggregator.Services
{
    public class CarriersService:ICarriersService
    {
               private readonly ICarriersRepository _carriersRepository;
        private readonly IMapper _mapper;

        public CarriersService(ICarriersRepository carriersRepository,
            IMapper mapper)
        {
            _carriersRepository = carriersRepository;
            _mapper = mapper;
        }
        public async Task<CarrierDto> GetCarrierAsync(int id)
        {
            var Carrier = await _carriersRepository.GetCarrierAsync(id);
            return _mapper.Map<CarrierDto>(Carrier);
        }

        public async Task<IEnumerable<CarrierDto>> GetCarriersAsync()
        {
            var Carriers = await _carriersRepository.GetCarriersAsync();
            return _mapper.Map<IEnumerable<CarrierDto>>(Carriers);  
        }

        public async Task<bool> CreateAsync(CarrierDto newCarrier)
        {
            var CarrierToDb = _mapper.Map<Carrier>(newCarrier);
            return await _carriersRepository.CreateAsync(CarrierToDb);
        }

        public async Task<bool> UpdateAsync(int id, CarrierDto CarrierUpdate)
        {
            var CarrierDb = await _carriersRepository.GetCarrierAsync(id);
            _mapper.Map(CarrierUpdate, CarrierDb);
            if(!await _carriersRepository.UpdateAsync(CarrierDb)) 
            {
                throw new ArgumentException("Failed to update Carrier");
            }
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _carriersRepository.DeleteAsync(id);
        }
    }
}