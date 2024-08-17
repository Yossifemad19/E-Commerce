using AutoMapper;
using E_Commerce.Api.DTOs;
using E_Commerce.Core.Entities.OrdderAggregate;

namespace E_Commerce.Api.Helpers
{
    public class OrderImagesUrlRedolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _config;

        public OrderImagesUrlRedolver(IConfiguration config)
        {
            _config = config;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ProductItem.ProductImageUrl))
            {
                return _config["ApiUrl"] + source.ProductItem.ProductImageUrl;
            }

            return null;
        }
    }
}
