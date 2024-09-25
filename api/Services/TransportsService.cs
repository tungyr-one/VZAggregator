using AutoMapper;
using VZAggregator.DTOs;
using VZAggregator.Models;
using VZAggregator.Interfaces;
using VZAggregator.Interfaces.Repositories;

namespace VZAggregator.Services
{
    public class TransportsService:ITransportsService
    {
        private readonly ITransportsRepository _transportsRepository;
        private readonly IMapper _mapper;

        public TransportsService(ITransportsRepository transportsRepository,
            IMapper mapper)
        {
            _transportsRepository = transportsRepository;
            _mapper = mapper;
        }
        public async Task<TransportDto> GetTransportAsync(int id)
        {
            var transport = await _transportsRepository.GetTransportAsync(id);
            return _mapper.Map<TransportDto>(transport);
        }

        public async Task<IEnumerable<TransportDto>> GetTransportsAsync()
        {
            var transports = await _transportsRepository.GetTransportsAsync();
            return _mapper.Map<IEnumerable<TransportDto>>(transports);  
        }

        public async Task<bool> CreateAsync(TransportDto newTransport)
        {
            var transportToDb = _mapper.Map<Transport>(newTransport);
            return await _transportsRepository.CreateAsync(transportToDb);
        }

        public async Task<bool> UpdateAsync(int id, TransportDto transportUpdate)
        {
            var transportDb = await _transportsRepository.GetTransportAsync(id);
            _mapper.Map(transportUpdate, transportDb);
            if(!await _transportsRepository.UpdateAsync(transportDb)) 
            {
                throw new ArgumentException("Failed to update transport");
            }
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _transportsRepository.DeleteAsync(id);
        }
    }
}