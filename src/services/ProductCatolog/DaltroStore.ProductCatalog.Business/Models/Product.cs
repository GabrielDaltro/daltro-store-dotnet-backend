﻿using DaltroStore.Core.DomainObjects;

namespace DaltroStore.ProductCatalog.Domain.Models
{
    public class Product : Entity, IAggregateRoot
    {
        private string name; 
        private string description; 
        private bool active; 
        private string image;
        private Guid categoryId; 
        private uint stockQuantity; 
        private decimal weight; 
        private Dimension dimension; 
        private decimal price; 
        private readonly DateTime registrationDate;

        private static readonly int minPriceValue = 0;
        private static readonly int minWeightValue = 0;

        public decimal Price
        {
            get => price;
        }

        public string Name
        {
            get => name;
        }

        public string Description
        {
            get => description;
        }

        public bool Active
        {
            get => active;
        }

        public string Image
        {
            get => image;
        }

        public Guid CategoryId
        {
            get => categoryId;
        }

        public uint StockQuantity
        {
            get => stockQuantity;
        }

        public DateTime RegistrationDate
        {
            get => registrationDate;
        }

        public decimal Weight
        {
            get => weight;
        }

        public Dimension Dimension
        {
            get => dimension;
        }

        public Product(Guid id, string name, decimal price, string description, bool active, string image, Guid categoryId, DateTime registrationDate, decimal weight, Dimension dimension)
        {
            Id = id;
            this.name = name;
            this.price = price;
            this.description = description;
            this.active = active;
            this.image = image;
            this.categoryId = categoryId;
            this.registrationDate = registrationDate;
            this.weight = weight;
            this.dimension = dimension;

            Validate();
        }

        private Product() { }

        public void Activate() => active = true;

        public void Deactivate() => active = false;

        public void ChangeCategory(Category category)
        {
            categoryId = category.Id;
        }

        public void IncreaseStockQuantity(uint quantity)
        {
            stockQuantity += quantity;
        }

        public void DecreaseStockQuantity(uint quantity)
        {
            if (!HasStockQuantity(quantity)) throw new EntityValidationException("Insufficient stock");
            stockQuantity -= quantity;
        }

        public bool HasStockQuantity(uint quantity)
        {
            return quantity <= stockQuantity;
        }
        
        public void ChangeImage(string newImage)
        {
            AssertionConcern.AssertIsNotNull(newImage, "product image can not be null");
            AssertionConcern.AssertIsNotEmpty(newImage, "product image can not be empty");
            image = newImage;
        }
    
        public void ChangeName(string newName) 
        {
            AssertionConcern.AssertIsNotNull(newName, "product name can not be null");
            AssertionConcern.AssertIsNotEmpty(newName, "product name can not be empty");
            name = newName;
        }

        public void ChangeDescription(string newDescription) 
        {
            AssertionConcern.AssertIsNotNull(newDescription, "product description can not be null");
            AssertionConcern.AssertIsNotEmpty(newDescription, "product description can not be empty");
            description = newDescription;
        }
    
        public void ChangePrice(decimal newPrice)
        {
            AssertionConcern.AssertIsGreaterThan(newPrice, expcted: minPriceValue, $"The product price must be greater than {minPriceValue}");
            price = newPrice;
        }

        public void ChangeWeight(decimal newWeight)
        {
            AssertionConcern.AssertIsGreaterThan(newWeight, expcted: minWeightValue, $"The product weight must be greater than {minWeightValue}");
            weight = newWeight;
        }

        public void ChangeDimession(Dimension newDimession)
        {
            dimension = newDimession;
        }

        public void Validate()
        {
            AssertionConcern.AssertIsNotEquals(Id, Guid.Empty, "the product id can not be empty");
            AssertionConcern.AssertIsNotNull(name, "product name can not be null");
            AssertionConcern.AssertIsNotEmpty(name, "product name can not be empty");
            AssertionConcern.AssertIsNotNull(description, "product description can not be null");
            AssertionConcern.AssertIsNotEmpty(description, "product description can not be empty");
            AssertionConcern.AssertIsNotNull(image, "product image can not be null");
            AssertionConcern.AssertIsNotEmpty(image, "product image can not be empty");
            AssertionConcern.AssertIsGreaterThan(price, expcted: minPriceValue, $"The product price must be greater than {minPriceValue}");
            AssertionConcern.AssertIsGreaterThan(weight, expcted: minWeightValue, $"The product weight must be greater than {minWeightValue}");
            dimension.Validate();
        }
    }
}