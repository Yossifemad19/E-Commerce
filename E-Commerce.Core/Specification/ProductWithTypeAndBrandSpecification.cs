using E_Commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Specification
{
    public class ProductWithTypeAndBrandSpecification : BaseSpecification<Product>
    {
        public ProductWithTypeAndBrandSpecification(ProductSpecParams productParams):
            base(p=>
            (!productParams.BrandId.HasValue || p.ProductBrandId== productParams.BrandId) &&
                (!productParams.TypeId.HasValue || p.ProductTypeId== productParams.TypeId)
                )
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);

            ApplyPagination(productParams.PageSize*(productParams.PageIndex-1), productParams.PageSize);

            if(!string.IsNullOrEmpty(productParams.Sort)){
                switch (productParams.Sort)
                {
                    case "PriceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
        }

        public ProductWithTypeAndBrandSpecification(int id) : base(x=>x.Id==id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }




    }
}
