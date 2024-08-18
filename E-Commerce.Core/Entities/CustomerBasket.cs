using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Entities
{
    public class CustomerBasket
    {
        public CustomerBasket(string id)
        {
            Id = id;
        }
        public CustomerBasket()
        {
            
        }
        public string Id { get; set; }
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
        public int? DeliveryMethodId;
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
    }
}
