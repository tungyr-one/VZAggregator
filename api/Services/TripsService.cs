using AutoMapper;
using VZAggregator.DTOs;
using VZAggregator.Entities;
using VZAggregator.Interfaces;
using VZAggregator.Interfaces.Repositories;

namespace VZAggregator.Services
{
    public class TripsService:ITripsService
    {
        private readonly ITripsRepository _tripsRepository;
        private readonly IMapper _mapper;

        public TripsService(ITripsRepository tripsRepository,
            IMapper mapper)
        {
            _tripsRepository = tripsRepository;
            _mapper = mapper;
        }

        public async Task<TripDto> GetTripAsync(int id)
        {
            var trip = await _tripsRepository.GetTripAsync(id);
            return _mapper.Map<TripDto>(trip);
        }

        public async Task<IEnumerable<TripDto>> GetTripsAsync()
        {
            var trips = await _tripsRepository.GetTripsAsync();
            return _mapper.Map<IEnumerable<TripDto>>(trips);  
        }

        public async Task<bool> CreateAsync(TripDto newTrip)
        {
            var tripToDb = _mapper.Map<Trip>(newTrip);
            return await _tripsRepository.CreateAsync(tripToDb);
        }

        public async Task<bool> UpdateAsync(int id, TripDto tripUpdate)
        {
            var tripDb = await _tripsRepository.GetTripAsync(id);
            _mapper.Map(tripUpdate, tripDb);
            if(!await _tripsRepository.UpdateAsync(tripDb)) 
            {
                throw new ArgumentException("Failed to update trip");
            }
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _tripsRepository.DeleteAsync(id);
        }
    }
}