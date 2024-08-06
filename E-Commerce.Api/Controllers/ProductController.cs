using E_Commerce.Core.Entities;
using E_Commerce.Core.Interfaces;
using E_Commerce.Core.Specification;
using E_Commerce.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductController
    {
        private readonly IGenericRepository<Product> _repo;

        public ProductController(IGenericRepository<Product> repo)
        {
            _repo = repo;
        }

        [HttpGet("get-all-data")]
        public async Task<ActionResult<List<Product>>> GetAllAsync()
        {
            var spec = new ProductWithTypeAndBrandSpecification();
            return await _repo.GetAllWithSpecAsync(spec);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetByIdAsync(int id)
        {
            var spec = new ProductWithTypeAndBrandSpecification(id);
            return await _repo.GetByIdWithSpecAsync(spec);
        }
    }
}
