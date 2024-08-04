using E_Commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Data
{
    public  class StoreContextSeed
    {
        public static async Task SeedDataAsync(StoreContext context) {

            

            if (!context.ProductBrands.Any())
            {
                var brandsData = File.ReadAllText("../E-Commerce.Infrastructure/Data/SeedData/brands.json");

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                context.ProductBrands.AddRange(brands);
                await context.SaveChangesAsync();

            }

            if (!context.ProductTypes.Any())
            {
                var typesData = File.ReadAllText("../E-Commerce.Infrastructure/Data/SeedData/types.json");

                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                context.ProductTypes.AddRange(types);
                await context.SaveChangesAsync();

            }
            if (!context.Products.Any())
            {
                var productsData = File.ReadAllText("../E-Commerce.Infrastructure/Data/SeedData/products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                context.Products.AddRange(products);
                await context.SaveChangesAsync();

            }
            //if (context.ChangeTracker.HasChanges())
            //{
            //}
        }
    }
}
