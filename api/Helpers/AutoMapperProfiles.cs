using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using VZAggregator.DTOs;
using VZAggregator.Entities;

namespace VZAggregator.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Carrier, CarrierDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Transport, TransportDto>().ReverseMap();
            CreateMap<Trip, TripDto>().ReverseMap();
        }
    }
}