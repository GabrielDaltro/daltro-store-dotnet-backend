using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;

namespace DaltroStore.Identity.API.Controllers
{
    [ApiController]
    public class ErrorHandlingController : MainController
    {
        [Route("/error")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HandleError()
        {
            return CustomInternalServerError(title: "Um erro inesperado ocorreu");
        }

        [Route("/error-development")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HandleErrorDevelopment()
        {
            var exceptionsFeatures = HttpContext.Features.Get<IExceptionHandlerFeature>()!;
            return Problem(detail: exceptionsFeatures.Error.StackTrace, 
                           statusCode: StatusCodes.Status500InternalServerError,
                           title: "Um erro inesperado ocorreu");
        }
    }
}