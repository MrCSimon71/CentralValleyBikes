using AutoMapper;
using CentralValleyBikes.Domain.Entities;
using CentralValleyBikes.Api.DTOs;

namespace CentralValleyBikes.Api.Mappers
{
    public class BrandsMapperProfile : Profile
    {
        public BrandsMapperProfile()
        {
            CreateMap<Brand, BrandDto>();
            CreateMap<BrandDto, Brand>();
        }
    }
}
