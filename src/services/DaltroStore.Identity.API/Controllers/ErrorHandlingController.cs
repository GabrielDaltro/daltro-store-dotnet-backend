using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;

namespace DaltroStore.Identity.API.Controllers
{
    [ApiController]
    public class ErrorHandlingController : ControllerBase
    {
        [Route("/error")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HandleError()
        {
            return Problem(statusCode: StatusCodes.Status500InternalServerError, title: "An unexpected error occurred");
        }

        [Route("/error-development")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HandleErrorDevelopment()
        {
            var exceptionsFeatures = HttpContext.Features.Get<IExceptionHandlerFeature>()!;
            return Problem(detail: exceptionsFeatures.Error.StackTrace, 
                           statusCode: StatusCodes.Status500InternalServerError,
                           title: exceptionsFeatures.Error.Message);
        }
    }
}