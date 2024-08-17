using E_Commerce.Core.Entities.OrdderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string userEmail, Address shippingAddress,
            int deliveryMethodId, string baskeId);

        Task<IReadOnlyList<Order>> GetOrdersAsync(string userEmail);
        Task<Order> GetOrderByIdAsync(int id, string userEmail);

        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
    }
}
