using AutoMapper;
using E_Commerce.Api.DTOs;
using E_Commerce.Api.Helpers;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Interfaces;
using E_Commerce.Core.Specification;
using E_Commerce.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{
 
    public class ProductController:BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public ProductController(IUnitOfWork UnitOfWork,IMapper mapper,IWebHostEnvironment env)
        {
            _unitOfWork = UnitOfWork;
            _mapper = mapper;
            _env = env;
        }

        [Cached(10000)]
        [HttpGet("get-all-data")]
        //[Authorize]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetAllAsync([FromQuery] ProductSpecParams productParams)
        {
            var spec = new ProductWithTypeAndBrandSpecification(productParams);

            var products= await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);
            var countSpec = new ProductCountsWithSpec(productParams);
            var totalItems =await _unitOfWork.Repository<Product>().CountAsync(countSpec);

            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, totalItems,
                _mapper.Map<List<Product>, List<ProductToReturnDto>>(products)));
        }

        [Cached(10000)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithTypeAndBrandSpecification(id);
            return Ok(_mapper.Map<Product,ProductToReturnDto>(await _unitOfWork.Repository<Product>().GetByIdWithSpecAsync(spec)));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductAsync(int id)
        {
            var product= await _unitOfWork.Repository<Product>().GetByIdAsync(id);
            var imagePath = Path.Combine(_env.WebRootPath, product.ImageUrl);

            _unitOfWork.Repository<Product>().Delete(product);
            var result =await _unitOfWork.Complete();
            if(result<=0)
                return BadRequest();
            
            if(System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);   

            return Ok();
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddProductAsync(ProductDto productForm)
        {
            var imagesFolder = Path.Combine(_env.WebRootPath,"images/products");
            if (!Directory.Exists(imagesFolder)) 
                Directory.CreateDirectory(imagesFolder);

            var imageFile = $"{Guid.NewGuid()}-{productForm.Picture.FileName}";
            var relativePath = Path.Combine("images/products", imageFile);
            var filePath = Path.Combine(imagesFolder, imageFile);

            using(var fileStream =new FileStream(filePath, FileMode.Create))
            {
                await productForm.Picture.CopyToAsync(fileStream);
            }

            var product = new Product
            {
                Name = productForm.Name,
                Description = productForm.Description,
                ImageUrl=relativePath,
                Price = productForm.Price,
                ProductBrandId = productForm.BrandID,
                ProductTypeId = productForm.TypeID,
            };

            _unitOfWork.Repository<Product>().Add(product);
             var result =await _unitOfWork.Complete();
            if (result<=0) return BadRequest(); 

            return Ok(_mapper.Map<Product,ProductToReturnDto>(product));
        }


    }
}
