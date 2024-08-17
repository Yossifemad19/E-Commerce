using E_Commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Interfaces
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasket(string id);
        Task<CustomerBasket> UpdateBasket(CustomerBasket basket);
        Task<bool> DeleteBasket(string id);
    }
}
