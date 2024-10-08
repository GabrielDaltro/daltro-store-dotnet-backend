﻿using Microsoft.Extensions.Logging;
using DaltroStore.Core.Data;
using DaltroStore.ProductCatalog.Domain.Repositories;
using DaltroStore.ProductCatalog.Domain.Models;
using DaltroStore.Core.DomainObjects;
using DaltroStore.Core.Communication.Command;

namespace DaltroStore.ProductCatalog.Application.Commands
{
    public class EditProductCommandHandler : ICommandHandler<EditProductCommand, CommandResult>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly ILogger logger;

        public EditProductCommandHandler(IUnitOfWork unitOfWork,
                                           IProductRepository productRepository,
                                           ICategoryRepository categoryRepository,
                                           ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger(nameof(EditProductCommandHandler));
            this.unitOfWork = unitOfWork;
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
        }

        public async Task<CommandResult> Handle(EditProductCommand command, CancellationToken cancellationToken)
        {
            try
            {
                Product? product = await productRepository.GetById(command.AggregateId);
                if (product == null)
                    return new CommandResult(CmdResultStatus.AggregateNotFound, error: $"Product of Id {command.AggregateId} was not found.");

                Category? category = await categoryRepository.GetById(command.CategoryId);
                if (category == null)
                    return new CommandResult(CmdResultStatus.AggregateNotFound, error: $"Category of Id {command.CategoryId} was not found.");

                UpdateProductProperties(product, category, command);
                productRepository.Update(product);
                await unitOfWork.Commit(cancellationToken);
                return new CommandResult(CmdResultStatus.Success, error: string.Empty);
            }
            catch (DomainException e)
            {
                logger.LogError(e, "Error to update product of Id {Id}.", command.AggregateId);
                return new CommandResult(CmdResultStatus.InvalidDomainOperation, $"Error to update product of Id {command.AggregateId}. {e.Message}");
            }
        }

        private void UpdateProductProperties(Product product, Category category, EditProductCommand command)
        {
            product.ChangeName(command.Name);
            product.ChangeDescription(command.Description);
            product.ChangePrice(command.Price);
            product.ChangeCategory(category);
            product.ChangeWeight(command.Weight);
            product.ChangeImage(command.Image);
            product.ChangeDimession(new Dimension(command.Width, command.Height, command.Depth));

            if (command.Active)
                product.Activate();
            else product.Deactivate();
        }
    }
}