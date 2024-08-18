using E_Commerce.Api.Errors;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace E_Commerce.Api.Controllers
{
    public class PaymentController : BaseApiController
    {
        private readonly IPaymentService _paymentService;
        private readonly IConfiguration _config;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IPaymentService paymentService,IConfiguration config,ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _config = config;
            _logger = logger;
        }

        [HttpPost("payment")]

        public async Task<ActionResult<CustomerBasket>> CreatePayment(string basketId)
        {
            var basketResult = await _paymentService.CreateOrUpdatePaymentIntent(basketId);

            if (basketResult == null)
                return BadRequest(new ApiResponse(400, "Problem with basket"));

            return Ok(basketResult);
        }


        [HttpPost("webhook")]
        public async Task<ActionResult> StripeWebhook()
        {
            var json = new StreamReader(HttpContext.Request.Body).ReadToEnd();

            var stripeEvent = EventUtility.ConstructEvent(json, HttpContext.Request.Headers["Stripe-Signature"], _config["Stripe:WebHook"]);

            var intent =(PaymentIntent) stripeEvent.Data.Object;
            if(stripeEvent.Type == Events.PaymentIntentSucceeded)
            {
               var order = await _paymentService.UpdateOrderPaymentSucceeded(intent.Id);

                _logger.LogInformation("Payment Succeeded");
                return Ok(order);

            }
            else if (stripeEvent.Type==Events.PaymentIntentPaymentFailed)
            {
                var order =await _paymentService.UpdateOrderPaymentFailed(intent.Id);
                _logger.LogInformation("Payment Failed");
                return Ok(order);

            }
            else {
                _logger.LogInformation($"unhandeld event {stripeEvent.Type}");

            }

            return Ok();
        }
    }
}
