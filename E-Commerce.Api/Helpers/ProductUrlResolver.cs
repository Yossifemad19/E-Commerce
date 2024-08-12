using AutoMapper;
using E_Commerce.Api.DTOs;
using E_Commerce.Core.Entities;

namespace E_Commerce.Api.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _config;

        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!String.IsNullOrEmpty(source.ImageUrl))
            {
                return _config["ApiUrl"]+source.ImageUrl;
            }

            return null;
        }
    }
}
