using AutoMapper;
using E_Commerce.Api.DTOs;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Entities.OrdderAggregate;



namespace E_Commerce.Api.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<E_Commerce.Core.Entities.Identity.Address, AddressDto>();

            CreateMap<BasketItemDto,BasketItem>();

            CreateMap<Product,ProductToReturnDto>()
                .ForMember(p=>p.ProductBrand,o=>o.MapFrom(p=>p.ProductBrand.Name))
                .ForMember(p=>p.ProductType,o=>o.MapFrom(p=>p.ProductType.Name))
                .ForMember(p=>p.ImageUrl,o=>o.MapFrom<ProductUrlResolver>());

            CreateMap<AddressDto, E_Commerce.Core.Entities.OrdderAggregate.Address>();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(o => o.OrderDate, o => o.MapFrom(o => o.OrderDate))
                .ForMember(o => o.DeliveryMethod, o => o.MapFrom(o => o.DeliveryMethod.ShortName))
                .ForMember(o => o.DeliveryMethodPrice, o => o.MapFrom(o => o.DeliveryMethod.Price));

           CreateMap<OrderItem,OrderItemDto>()
                .ForMember(o=>o.ProductItemId,o=>o.MapFrom(o=>o.ProductItem.ProductItemId))
                .ForMember(o=>o.ProductName,o=>o.MapFrom(o=>o.ProductItem.ProductName))
                .ForMember(o=>o.ProductImageUrl,o=>o.MapFrom<OrderImagesUrlRedolver>());

        }

        
    }
}
