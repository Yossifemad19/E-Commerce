using E_Commerce.Core.Entities;
using E_Commerce.Core.Entities.OrdderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Interfaces
{
    public interface IPaymentService
    {
        Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId);
        Task<Order> UpdateOrderPaymentSucceeded(string paymentIntent);
        Task<Order> UpdateOrderPaymentFailed(string paymentIntent);

    }
}
