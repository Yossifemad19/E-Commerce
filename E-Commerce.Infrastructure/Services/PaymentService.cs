using E_Commerce.Core.Entities;
using E_Commerce.Core.Entities.OrdderAggregate;
using E_Commerce.Core.Interfaces;
using E_Commerce.Core.Specification;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = E_Commerce.Core.Entities.Product;

namespace E_Commerce.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basket;
        private readonly IConfiguration _config;

        public PaymentService(IUnitOfWork unitOfWork,IBasketRepository _basket,IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            this._basket = _basket;
            _config = config;
        }
        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _config["Stripe:SecretKey"];

            var basket = await _basket.GetBasket(basketId);

            if (basket == null)
                return null;

            var shippingPrice = 0m;

            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod=await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value);
                shippingPrice = deliveryMethod.Price;
            }

            foreach(var item in basket.Items)
            {
                var prd = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                
                if (prd.Price != item.Price)
                    item.Price = prd.Price;
            }

            PaymentIntent paymentIntent;
             var service = new PaymentIntentService();

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)(basket.Items.Sum(i => i.Quantity * (i.Price * 100))+(shippingPrice*100)),
                    Currency = "USD",
                    PaymentMethodTypes=new List<string> { "card"}
                };

                paymentIntent = await service.CreateAsync(options);
                basket.PaymentIntentId=paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)(basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + (shippingPrice * 100))
                };
                await service.UpdateAsync(basket.PaymentIntentId, options);
            }

            await _basket.UpdateBasket(basket);

            return basket;
        }

        public async Task<Order> UpdateOrderPaymentFailed(string paymentIntentId)
        {
            var spec = new OrderByPaymentIntentIdSpecification(paymentIntentId);
            var order = await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);

            if (order == null)
                return null;

            order.OrderStatus = OrderStatus.PaymentFailed;

            await _unitOfWork.Complete();
            return order;
        }

        public async Task<Order> UpdateOrderPaymentSucceeded(string paymentIntentId)
        {

            var spec = new OrderByPaymentIntentIdSpecification(paymentIntentId);
            var order =await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);

            if (order == null)
                return null;

            order.OrderStatus = OrderStatus.PaymentReceived;

            await _unitOfWork.Complete();

            throw new NotImplementedException();
        }
    }
}
