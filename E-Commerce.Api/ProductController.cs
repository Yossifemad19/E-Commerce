using E_Commerce.Core.Entities;
using E_Commerce.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductController
    {
        private readonly StoreContext _context;

        public ProductController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet("get-all-data")]
        public ActionResult<List<Product>> GetAll() {
            return _context.Products.ToList();
        }
    }
}
