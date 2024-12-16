using DaltroStore.Core.DomainObjects;

namespace DaltroStore.Customers.Domain.Models
{
    public class Address : Entity
    {
        private readonly string street;
        private readonly string number;
        private readonly string apartment;
        private readonly string neighborhood;
        private readonly string zipCode;
        private readonly string city;
        private readonly string state;
        private readonly Guid customerId;

        public string Street
        {
            get => street; 
        }

        public string Number
        {
            get => number; 
        }

        public string Apartment
        {
            get => apartment; 
        }

        public string Neighborhood
        {
            get => neighborhood; 
        }

        public string ZipCode
        {
            get => zipCode; 
        }

        public string City
        {
            get => city; 
        }

        public string State
        {
            get => state; 
        }

        public Guid CustomerId
        {
            get => customerId; 
        }

        // EF Constructor
        private Address() { }

        public Address(string street, string number, string apartment, string neighborhood, string zipCode, string city, string state, Guid customerId)
        {
            this.street = street;
            this.number = number;
            this.apartment = apartment;
            this.neighborhood = neighborhood;
            this.zipCode = zipCode;
            this.city = city;
            this.state = state;
            this.customerId = customerId;
        }
    }
}