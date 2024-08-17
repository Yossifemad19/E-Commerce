using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Entities.OrdderAggregate
{
    public class OrderItem:BaseEntity
    {
        public OrderItem()
        {
            
        }
        public OrderItem(ProductItemOrdered productItem, int quantity, decimal price)
        {
            ProductItem = productItem;
            Quantity = quantity;
            Price = price;
        }

        public ProductItemOrdered ProductItem { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
