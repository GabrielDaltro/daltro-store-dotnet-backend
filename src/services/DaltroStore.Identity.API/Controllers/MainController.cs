using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DaltroStore.Identity.API.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        protected ICollection<string> errors = new List<string>();
        
        protected BadRequestObjectResult CustomBadRequest()
        {
            return BadRequest(new ValidationProblemDetails(
                new Dictionary<string, string[]>()
                {
                    {"Message", errors.ToArray() }
                })
            );
        }

        protected BadRequestObjectResult CustomBadRequest(ModelStateDictionary modelState)
        {
            IEnumerable<ModelError> errors = modelState.Values.SelectMany(x => x.Errors);
            foreach (ModelError error in errors) 
            {
                this.errors.Add(error.ErrorMessage);
            }
            return CustomBadRequest();
        }

        protected void AddProcessingError(string error) => errors.Add(error);

        protected void CleanProcessingErrors() => errors.Clear();
    }
}
