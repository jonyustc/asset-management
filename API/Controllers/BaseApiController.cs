using API.Errors;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        public ActionResult HandleResult<T>(Result<T> result)
        {
            //return Ok(result);
            if (result.IsSuccess && result.Values != null)
            {
                return Ok(result);
            }


            if (!result.IsSuccess && result.Values == null)
            {
                return NotFound(result);
            }

            if (!result.IsSuccess && result.Values == null)
            {
                return BadRequest(result);
            }

            return BadRequest(result);
        }
    }
}