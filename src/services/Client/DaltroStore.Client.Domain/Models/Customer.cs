using DaltroStore.Core.DomainObjects;

namespace DaltroStore.Customers.Domain.Models
{
    public class Customer : Entity, IAggregateRoot
    {
        private readonly string name;
        private readonly Cpf cpf;
        private bool excluded = false;
        private Address address;
        private Email email;

        public string Name { get => name; }

        public Email Email { get => email; }

        public Cpf Cpf { get => cpf; }

        public bool Excluded { get => excluded; }

        public Address Address { get => address; }

        private Customer() { }

        public Customer(Guid id, string name, string email, string cpf)
        {
            Id = id;
            this.name = name;
            this.email = new Email(email);
            this.cpf = new Cpf(cpf);
        }

        public void ChangeEmail(string email)
        {
            this.email = new Email(email);
        }

        public void ChangeAddress(Address address) 
        {
            this.address = address;
        }
    }
}