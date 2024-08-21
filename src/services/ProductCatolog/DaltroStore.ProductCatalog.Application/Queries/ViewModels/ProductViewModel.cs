namespace DaltroStore.ProductCatalog.Application.Queries.ViewModels
{
    public class ProductViewModel 
    {
        public string Name { get; init; }

        public decimal Price { get; init; }

        public string Description { get; init; }

        public string Category { get; init; }

        public string Image { get; init; }

        public decimal Weight { get; init; }

        public decimal Width { get; init; }

        public decimal Height { get; init; }

        public decimal Depth { get; init; }

        public ProductViewModel(string name, decimal price, string description, 
                                string image, decimal weight, decimal width,
                                decimal height, decimal depth, string category)
        {
            Name = name;
            Price = price;
            Description = description;
            Image = image;
            Weight = weight;
            Width = width;
            Height = height;
            Depth = depth;
            Category = category;
        }
    }
}