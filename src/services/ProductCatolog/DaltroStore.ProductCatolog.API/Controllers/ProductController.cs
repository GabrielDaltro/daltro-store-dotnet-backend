﻿using Microsoft.AspNetCore.Mvc;
using DaltroStore.Core.Communication.Command;
using DaltroStore.ProductCatalog.Application.Commands;

namespace DaltroStore.ProductCatolog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ICommandBus commandBus;

        public ProductController(ICommandBus commandBus)
        {
            this.commandBus = commandBus;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterProduct(RegisterProductCommand registerProductCommand)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ValidationProblemDetails(ModelState));

            CommandResult<Guid> commandResult = await commandBus.Send<RegisterProductCommand,CommandResult<Guid>>(registerProductCommand);

            if(commandResult.Status == CmdResultStatus.Success)
                return CreatedAtAction(nameof(RegisterProduct), new { Id = commandResult.Value });
            
            return Problem(statusCode: StatusCodes.Status400BadRequest, title: commandResult.Error);
        }

        [HttpPost("unregister")]
        public async Task<ActionResult> UnregisterProduct(UnregisterProductCommand unregisterCommand)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ValidationProblemDetails(ModelState));

            CommandResult commandResult = await commandBus.Send<UnregisterProductCommand, CommandResult>(unregisterCommand);

            if (commandResult.Status == CmdResultStatus.Success)
                return Ok();

            return Problem(statusCode: StatusCodes.Status400BadRequest, title: commandResult.Error);
        }
    }
}