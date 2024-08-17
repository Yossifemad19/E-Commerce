using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Entities.OrdderAggregate
{
    public class ProductItemOrdered
    {
        public ProductItemOrdered()
        {
            
        }
        public ProductItemOrdered(int productItemId, string productName, string productImageUrl)
        {
            ProductItemId = productItemId;
            ProductName = productName;
            ProductImageUrl = productImageUrl;
        }

        public int ProductItemId { get; set; }
        public string ProductName { get; set; }
        public string ProductImageUrl { get; set; }
    }
}
