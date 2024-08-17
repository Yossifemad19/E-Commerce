using E_Commerce.Core.Entities.OrdderAggregate;

namespace E_Commerce.Api.DTOs
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public DateTimeOffset OrderDate { get;set; }
        public string Address { get; set; }
        public IReadOnlyList<OrderItemDto> OrderItems { get; set; }

        public string DeliveryMethod { get; set; }
        public string DeliveryMethodPrice { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public string OrderStatus { get; set; }

    }
}
