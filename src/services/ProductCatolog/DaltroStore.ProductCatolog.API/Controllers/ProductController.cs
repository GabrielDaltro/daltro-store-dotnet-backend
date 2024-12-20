﻿using Microsoft.AspNetCore.Mvc;
using DaltroStore.Core.Communication.Command;
using DaltroStore.ProductCatalog.Application.Commands;
using DaltroStore.ProductCatalog.Application.Queries.ViewModels;
using DaltroStore.Core.Communication.Query;
using DaltroStore.ProductCatalog.Application.Queries;
using DaltroStore.ProductCatolog.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using DaltroStore.Auth.Claims;

namespace DaltroStore.ProductCatolog.API.Controllers
{
    [ApiController]
    [Authorize]
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

        [ClaimsAuthorize("Catalog","create")]
        [HttpPost("register")]
        public async Task<ActionResult> RegisterProduct(ProductRegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ValidationProblemDetails(ModelState));

            var registerProductCommand = new RegisterProductCommand(dto.Name, dto.Price, dto.Description, dto.Active, dto.Image, dto.CategoryId,
                dto.RegistrationDate, dto.Weight, dto.Width, dto.Height, dto.Depth);

            CommandResult<Guid> commandResult = await commandBus.Send<RegisterProductCommand, CommandResult<Guid>>(registerProductCommand);

            if (commandResult.Status == CmdResultStatus.Success)
                return CreatedAtAction(nameof(RegisterProduct), new { Id = commandResult.Value });

            return Problem(statusCode: StatusCodes.Status400BadRequest, title: commandResult.Error);
        }

        [ClaimsAuthorize("Catalog", "delete")]
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

        [ClaimsAuthorize("Catalog", "update")]
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

        [ClaimsAuthorize("Catalog", "update")]
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

        [ClaimsAuthorize("Catalog", "update")]
        [HttpPost("edit")]
        public async Task<ActionResult> EditProduct(ProductEditDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ValidationProblemDetails(ModelState));

            var editProductCommand = new EditProductCommand(dto.Id, dto.Name, dto.Price, dto.Description, dto.Active, dto.Image, dto.CategoryId,
                dto.Weight, dto.Width, dto.Height, dto.Depth);

            CommandResult commandResult = await commandBus.Send<EditProductCommand, CommandResult>(editProductCommand);

            if (commandResult.Status == CmdResultStatus.Success)
                return Ok();

            if (commandResult.Status == CmdResultStatus.AggregateNotFound)
                return NotFound(commandResult.Error);

            return Problem(statusCode: StatusCodes.Status400BadRequest, title: commandResult.Error);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<ProductViewModel>> GetAllProducts()
        {
            return await queryBus.Send<GetAllProductsQuery, IEnumerable<ProductViewModel>>(new GetAllProductsQuery());
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<ProductViewModel> GetProductDetails(Guid id)
        {
            return await queryBus.Send<GetProductByIdQuery, ProductViewModel>(new GetProductByIdQuery { ProductId = id});
        }

        [AllowAnonymous]
        [HttpGet("category/{id:guid}")]
        public async Task<IEnumerable<ProductViewModel>> GetProductsByCategory(Guid id)
        {
            return await queryBus.Send<GetProductByCategoryQuery, IEnumerable<ProductViewModel>>(new GetProductByCategoryQuery() { CategoryId = id});
        }
    }
}