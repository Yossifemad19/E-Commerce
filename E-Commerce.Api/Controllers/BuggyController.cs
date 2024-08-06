using E_Commerce.Api.Errors;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Interfaces;
using E_Commerce.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly StoreContext _context;

        public BuggyController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet("not-found")]
        public async Task<ActionResult> GetNotFoundAsync() {
            
            var product = await _context.Products.FindAsync(55);
            if(product == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok();
        }

        [HttpGet("ServerError")]
        public async Task<ActionResult<Product>> GetServerErrorAsync()
        {
            var product = await _context.Products.FindAsync(55);

            var product2=product.ToString();

            return product;
        }

        [HttpGet("BadRequest")]
        public async Task<ActionResult<Product>> GetBadRequestAsync()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("BadRequest/{id}")]
        public async Task<ActionResult<Product>> GetNotFoundRequest(int id)
        {
            return Ok();
        }



    }
}
