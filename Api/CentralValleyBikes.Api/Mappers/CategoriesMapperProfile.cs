using AutoMapper;
using CentralValleyBikes.Domain.Entities;
using CentralValleyBikes.Api.DTOs;

namespace CentralValleyBikes.Api.Mappers
{
    public class CategoriesMapperProfile : Profile
    {
        public CategoriesMapperProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
        }
    }
}
