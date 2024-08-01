using DaltroStore.Core.DomainObjects;

namespace DaltroStore.ProductCatalog.Business.Models
{
    public class Dimesion
    {
        public decimal Width { get; init; }

        public decimal Height { get; init; }

        public decimal Depth { get; init; }

        public Dimesion(decimal width, decimal height, decimal depth)
        {
            Width = width; 
            Height = height; 
            Depth = depth;

            Validate();
        }

        public void Validate()
        {
            int minValue = 0;
            AssertionConcern.AssertIsGreaterOrEqualsThan(Width, minValue, $"The dimesion width must be greater than {minValue}");
            AssertionConcern.AssertIsGreaterOrEqualsThan(Height, minValue, $"The dimesion height must be greater than {minValue}");
            AssertionConcern.AssertIsGreaterOrEqualsThan(Depth, minValue, $"The dimesion depth must be greater than {minValue}");
        }
    }
}