using E_Commerce.Api.Errors;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{

    
    [Route("Erros/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController
    {
        public ActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}
