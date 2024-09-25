using AutoMapper;
using VZAggregator.DTOs;
using VZAggregator.Models;
using VZAggregator.Interfaces.Repositories;

namespace VZAggregator.Services
{
    public class AddressesService:IAddressesService
    {
               private readonly IAddressesRepository _addressesRepository;
        private readonly IMapper _mapper;

        public AddressesService(IAddressesRepository addressesRepository,
            IMapper mapper)
        {
            _addressesRepository = addressesRepository;
            _mapper = mapper;
        }
        public async Task<AddressDto> GetAddressAsync(int id)
        {
            var address = await _addressesRepository.GetAddressAsync(id);
            return _mapper.Map<AddressDto>(address);
        }

        public async Task<IEnumerable<AddressDto>> GetAddressesAsync()
        {
            var addresses = await _addressesRepository.GetAddressesAsync();
            return _mapper.Map<IEnumerable<AddressDto>>(addresses);  
        }

        public async Task<bool> CreateAsync(AddressDto newAddress)
        {
            var addressToDb = _mapper.Map<Address>(newAddress);
            return await _addressesRepository.CreateAsync(addressToDb);
        }

        public async Task<bool> UpdateAsync(int id, AddressDto addressUpdate)
        {
            var addressDb = await _addressesRepository.GetAddressAsync(id);
            _mapper.Map(addressUpdate, addressDb);
            if(!await _addressesRepository.UpdateAsync(addressDb)) 
            {
                throw new ArgumentException("Failed to update address");
            }
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _addressesRepository.DeleteAsync(id);
        }
    }
}