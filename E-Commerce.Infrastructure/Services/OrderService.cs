using E_Commerce.Core.Entities;
using E_Commerce.Core.Entities.OrdderAggregate;
using E_Commerce.Core.Interfaces;
using E_Commerce.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepo;

        public OrderService(IUnitOfWork unitOfWork,IBasketRepository basketRepo)
        {
            _unitOfWork = unitOfWork;
            _basketRepo = basketRepo;
        }
        public async Task<Order> CreateOrderAsync(string userEmail, Address shippingAddress, int deliveryMethodId, string basketId)
        {
            //get basket

            var basket = await _basketRepo.GetBasket(basketId);

            if (basket is null) return null;

            var ordereditems=new List<OrderItem>();

            foreach (var item in basket.Items)
            {
                var product =await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);

                var productItem=new ProductItemOrdered(product.Id, product.Name,product.ImageUrl);
                var orderitem = new OrderItem(productItem,item.Quantity,product.Price);
                ordereditems.Add(orderitem);
            }

            var deliveryMethod= await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);


            var subTotal = ordereditems.Sum(o => o.Price * o.Quantity);

            var order=new Order(userEmail,ordereditems
                ,shippingAddress,deliveryMethod,subTotal);

            _unitOfWork.Repository<Order>().Add(order);

            var result =await _unitOfWork.Complete();

            if(result<=0) return null;

            await _basketRepo.DeleteBasket(basketId);

            return  order;

        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id, string userEmail)
        {

            var spec = new OrderWithItemsSpecification(id, userEmail);
            return await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersAsync(string userEmail)
        {
            var spec=new OrderWithItemsSpecification(userEmail);

            return await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);
        }
    }
}
