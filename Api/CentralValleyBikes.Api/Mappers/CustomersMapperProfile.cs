using AutoMapper;
using CentralValleyBikes.Api.DTOs;
using CentralValleyBikes.Domain.Entities;

namespace CentralValleyBikes.Api.Mappers
{
    public class CustomersMapperProfile : Profile
    {
        public CustomersMapperProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();
        }
    }
}
