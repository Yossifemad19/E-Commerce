using AutoMapper;
using E_Commerce.Api.DTOs;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Entities.Identity;

namespace E_Commerce.Api.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Address, AddressDto>();
            CreateMap<BasketItemDto,BasketItem>();

            CreateMap<Product,ProductToReturnDto>()
                .ForMember(p=>p.ProductBrand,o=>o.MapFrom(p=>p.ProductBrand.Name))
                .ForMember(p=>p.ProductType,o=>o.MapFrom(p=>p.ProductType.Name))
                .ForMember(p=>p.ImageUrl,o=>o.MapFrom<ProductUrlResolver>());
            ;
        }
    }
}
