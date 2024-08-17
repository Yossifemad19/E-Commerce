using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Entities.OrdderAggregate
{
    public class Order:BaseEntity
    {
        public Order()
        {
        }

        public Order(string userEmail, IReadOnlyList<OrderItem> orderItems, Address shippingAddress,
            DeliveryMethod deliveryMethod,  decimal subTotal)
        {
            OrderItems = orderItems;
            ShippingAddress = shippingAddress;
            UserEmail = userEmail;
            DeliveryMethod = deliveryMethod;
            SubTotal = subTotal;
        }

        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public Address ShippingAddress { get; set; }
        public string UserEmail { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public OrderStatus OrderStatus { get; set; }=OrderStatus.Pending;

        public DateTimeOffset OrderDate { get; set; } = DateTime.UtcNow;
        public decimal SubTotal { get; set; }

        public decimal GetTotal (){
            return SubTotal+DeliveryMethod.Price;
        }

        public string GetAddress()
        {
            return $"{ShippingAddress.HouseNumber} {ShippingAddress.StreetName} - {ShippingAddress.State}";
        }
    }
}
