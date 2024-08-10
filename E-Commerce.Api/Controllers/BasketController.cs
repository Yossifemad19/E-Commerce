using E_Commerce.Core.Entities;
using E_Commerce.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{
    public class BasketController:BaseApiController
    {
        private readonly IBasketService _basket;

        public BasketController(IBasketService basket)
        {
            _basket = basket;
        }

        [HttpGet("GetBasket")]
        public async Task<ActionResult> GetBasket(string id)
        {
            var Basket=await _basket.GetBasket(id);
            
            return Ok(Basket?? new CustomerBasket(id));
        }

        [HttpPost("UpdateBasket")]

        public async Task<ActionResult> UpdatBasket(CustomerBasket basket)
        {
            var Basket = await _basket.UpdateBasket(basket);
            if (Basket == null)
                return BadRequest();

            return Ok(Basket);
        }

        [HttpDelete("DeleteBasket")]
        public async Task<ActionResult> DeleteBasket(string id)
        {
            return Ok(await _basket.DeleteBasket(id));
        }

    }
}
