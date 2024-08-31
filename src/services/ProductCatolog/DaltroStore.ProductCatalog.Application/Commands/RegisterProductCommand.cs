using DaltroStore.Core.Communication.Command;

namespace DaltroStore.ProductCatalog.Application.Commands
{
    public class RegisterProductCommand : ICommand<CommandResult<Guid>>
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

        public DateTime RegistrationDate { get; init; }

        public RegisterProductCommand(string name, decimal price, string description, bool active, string image, 
            Guid categoryId, DateTime registrationDate, decimal weight, decimal width, decimal height, decimal depth)
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
            RegistrationDate = registrationDate;
        }
    }
}