using AutoMapper;
using CentralValleyBikes.Api.DTOs;
using CentralValleyBikes.Domain.Entities;
using Microsoft.AspNetCore.Routing.Constraints;

namespace CentralValleyBikes.Api.Mappers
{
    public class ProductsMapperProfile : Profile
    {
        public ProductsMapperProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>()
                .ForMember(m => m.BrandId, opt => opt.MapFrom(src => src.Brand.BrandId))
                .ForMember(m => m.CategoryId, opt => opt.MapFrom(src => src.Category.CategoryId));
        }
    }
}
