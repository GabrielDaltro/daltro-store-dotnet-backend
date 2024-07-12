using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DaltroStore.Identity.API.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        protected IDictionary<string, string[]> errors = new Dictionary<string, string[]>();
        
        protected BadRequestObjectResult CustomBadRequest()
        {
            return BadRequest(new ValidationProblemDetails(errors));
        }

        protected BadRequestObjectResult CustomBadRequest(ModelStateDictionary modelState)
        {           
            return BadRequest(new ValidationProblemDetails(modelState));
        }

        protected ObjectResult CustomInternalServerError(string title)
        {
            return Problem(statusCode: StatusCodes.Status500InternalServerError, title: title);
        }

        protected ObjectResult CustomTooManyRequests(string title)
        {
            return Problem(statusCode: StatusCodes.Status429TooManyRequests, title: title);
        }

        protected ObjectResult CustomUnauthorized(string title)
        {
            return Problem(statusCode: StatusCodes.Status401Unauthorized, title: title);
        }

        protected void AddProcessingError(string key, string[] errors) => this.errors.Add(key, errors);

        protected void CleanProcessingErrors() => errors.Clear();
    }
}