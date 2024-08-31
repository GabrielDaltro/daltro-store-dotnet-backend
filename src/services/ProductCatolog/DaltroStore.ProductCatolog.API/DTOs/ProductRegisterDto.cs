namespace DaltroStore.ProductCatolog.API.DTOs
{
    public record class ProductRegisterDto
    {
        public string Name { get; init; } = string.Empty;

        public string Description { get; init; } = string.Empty;

        public bool Active { get; init; }

        public decimal Price { get; init; }

        public string Image { get; init; } = string.Empty;

        public Guid CategoryId { get; init; }

        public decimal Weight { get; init; }

        public decimal Width { get; init; }

        public decimal Height { get; init; }

        public decimal Depth { get; init; }

        public DateTime RegistrationDate { get; init; }
    }
}
