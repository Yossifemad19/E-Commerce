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
        private readonly IGenericRepository<Product> _repo;
        private readonly IMapper _mapper;

        public ProductController(IGenericRepository<Product> repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("get-all-data")]
        //[Authorize]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetAllAsync([FromQuery] ProductSpecParams productParams)
        {
            var spec = new ProductWithTypeAndBrandSpecification(productParams);

            var products= await _repo.GetAllWithSpecAsync(spec);
            var countSpec = new ProductCountsWithSpec(productParams);
            var totalItems =await _repo.CountAsync(countSpec);

            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, totalItems,
                _mapper.Map<List<Product>, List<ProductToReturnDto>>(products)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetByIdAsync(int id)
        {
            var spec = new ProductWithTypeAndBrandSpecification(id);
            return _mapper.Map<Product,ProductToReturnDto>(await _repo.GetByIdWithSpecAsync(spec));
        }
    }
}
