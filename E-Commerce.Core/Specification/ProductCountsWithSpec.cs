using E_Commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Specification
{
    public class ProductCountsWithSpec : BaseSpecification<Product>
    {
        public ProductCountsWithSpec(ProductSpecParams productParams) :
            base(p =>
            (!productParams.BrandId.HasValue || p.ProductBrandId == productParams.BrandId) &&
                (!productParams.TypeId.HasValue || p.ProductTypeId == productParams.TypeId)
                )
        {
        
        }

    }
}
