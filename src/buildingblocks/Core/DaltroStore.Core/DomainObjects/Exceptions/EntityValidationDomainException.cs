namespace DaltroStore.Core.DomainObjects
{
    public class EntityValidationDomainException : DomainException 
    {
        public EntityValidationDomainException() { }

        public EntityValidationDomainException(string message) : base(message) { }

        public EntityValidationDomainException(string message, Exception innerException) : base(message, innerException) { }
    }
}
}
