using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("Errors/{code}")]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorsController : BaseApiController
    {
        public IActionResult Get(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}