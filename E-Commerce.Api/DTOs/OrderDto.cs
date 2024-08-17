using E_Commerce.Core.Entities;
using E_Commerce.Core.Entities.OrdderAggregate;

namespace E_Commerce.Api.DTOs
{
    public class OrderDto
    {

        public AddressDto shippingAddress { get; set; }
        public int deliveryMethodId { get; set; }
        public string baskeId { get; set; }
    }
}
