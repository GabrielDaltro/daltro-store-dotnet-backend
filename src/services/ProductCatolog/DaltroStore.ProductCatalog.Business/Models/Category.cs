using DaltroStore.Core.DomainObjects;

namespace DaltroStore.ProductCatalog.Domain.Models
{
    public class Category : Entity, IAggregateRoot
    {
        private string name;
        private int code;

        public string Name { get => name; }

        public int Code { get => code; }

        public Category(string name, int code)
        {
            this.name = name;
            this.code = code;
        }

        public void ChangeName(string newName) 
        {
            AssertionConcern.AssertIsNotNull(newName, "category name can not be null");
            AssertionConcern.AssertIsNotEmpty(newName, "category name can not be empty");
            name = newName;
        }

        public void ChangeCode(int newCode) 
        {
            code = newCode;
        }

        public void Validate()
        {
            AssertionConcern.AssertIsNotEquals(Id, Guid.Empty, "the product id can not be empty");
            AssertionConcern.AssertIsNotNull(name, "category name can not be null");
            AssertionConcern.AssertIsNotEmpty(Name, "category name can not be empty");
        }
    }
}