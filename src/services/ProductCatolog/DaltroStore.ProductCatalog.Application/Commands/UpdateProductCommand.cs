using DaltroStore.Core.Messages;

namespace DaltroStore.ProductCatalog.Application.Commands
{
    public class UpdateProductCommand : Command<CommandResult>
    {
        public string Name { get; init; }

        public string Description { get; init; }

        public bool Active { get; init; }

        public decimal Price { get; init; }

        public string Image { get; init; }

        public Guid CategoryId { get; init; }

        public decimal Weight { get; init; }

        public decimal Width { get; init; }

        public decimal Height { get; init; }

        public decimal Depth { get; init; }

        public UpdateProductCommand(string name, decimal price, string description, bool active, string image,
                    Guid categoryId, decimal weight, decimal width, decimal height, decimal depth)
        {
            Name = name;
            Price = price;
            Description = description;
            Active = active;
            Image = image;
            CategoryId = categoryId;
            Weight = weight;
            Width = width;
            Height = height;
            Depth = depth;
        }
    }
}