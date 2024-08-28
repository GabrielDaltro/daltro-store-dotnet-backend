using Microsoft.AspNetCore.Mvc;
using DaltroStore.Core.Communication.Command;
using DaltroStore.ProductCatalog.Application.Commands;
using DaltroStore.ProductCatalog.Application.Queries.ViewModels;
using DaltroStore.Core.Communication.Query;
using DaltroStore.ProductCatalog.Application.Queries;

namespace DaltroStore.ProductCatolog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ICommandBus commandBus;
        private readonly IQueryBus queryBus;

        public ProductController(ICommandBus commandBus, IQueryBus queryBus)
        {
            this.commandBus = commandBus;
            this.queryBus = queryBus;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterProduct(RegisterProductCommand registerProductCommand)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ValidationProblemDetails(ModelState));

            CommandResult<Guid> commandResult = await commandBus.Send<RegisterProductCommand, CommandResult<Guid>>(registerProductCommand);

            if (commandResult.Status == CmdResultStatus.Success)
                return CreatedAtAction(nameof(RegisterProduct), new { Id = commandResult.Value });

            return Problem(statusCode: StatusCodes.Status400BadRequest, title: commandResult.Error);
        }

        [HttpPost("unregister")]
        public async Task<ActionResult> UnregisterProduct(UnregisterProductCommand unregisterCommand)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ValidationProblemDetails(ModelState));

            CommandResult commandResult = await commandBus.Send<UnregisterProductCommand, CommandResult>(unregisterCommand);

            if (commandResult.Status == CmdResultStatus.Success || commandResult.Status == CmdResultStatus.AggregateNotFound)
                return Ok();

            return Problem(statusCode: StatusCodes.Status400BadRequest, title: commandResult.Error);
        }

        [HttpPost("increase-stock")]
        public async Task<ActionResult> IncreaseProductStock(IncreaseProductStockCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ValidationProblemDetails(ModelState));

            CommandResult commandResult = await commandBus.Send<IncreaseProductStockCommand, CommandResult>(command);

            if (commandResult.Status == CmdResultStatus.Success)
                return Ok();

            if (commandResult.Status == CmdResultStatus.AggregateNotFound)
                return NotFound(commandResult.Error);

            return Problem(statusCode: StatusCodes.Status400BadRequest, title: commandResult.Error);
        }

        [HttpPost("decrease-stock")]
        public async Task<ActionResult> DecreaseProductStock(DecreaseProductStockCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ValidationProblemDetails(ModelState));

            CommandResult commandResult = await commandBus.Send<DecreaseProductStockCommand, CommandResult>(command);

            if (commandResult.Status == CmdResultStatus.Success)
                return Ok();

            if (commandResult.Status == CmdResultStatus.AggregateNotFound)
                return NotFound(commandResult.Error);

            return Problem(statusCode: StatusCodes.Status400BadRequest, title: commandResult.Error);
        }

        [HttpPost("edit")]
        public async Task<ActionResult> EditProduct(EditProductCommand editProductCommand)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ValidationProblemDetails(ModelState));

            CommandResult commandResult = await commandBus.Send<EditProductCommand, CommandResult>(editProductCommand);

            if (commandResult.Status == CmdResultStatus.Success)
                return Ok();

            if (commandResult.Status == CmdResultStatus.AggregateNotFound)
                return NotFound(commandResult.Error);

            return Problem(statusCode: StatusCodes.Status400BadRequest, title: commandResult.Error);
        }

        [HttpGet]
        public async Task<IEnumerable<ProductViewModel>> GetAllProducts()
        {
            return await queryBus.Send<GetAllProductsQuery, IEnumerable<ProductViewModel>>(new GetAllProductsQuery());
        }

        [HttpGet("{id:Guid}")]
        public async Task<ProductViewModel> GetProductDetails(Guid id)
        {
            return await queryBus.Send<GetProductByIdQuery, ProductViewModel>(new GetProductByIdQuery { ProductId = id});
        }
    }
}