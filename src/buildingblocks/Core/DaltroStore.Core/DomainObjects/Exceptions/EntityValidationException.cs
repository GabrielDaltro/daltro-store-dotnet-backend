namespace DaltroStore.Core.DomainObjects
{
    public class EntityValidationException : DomainException 
    {
        public EntityValidationException() { }

        public EntityValidationException(string message) : base(message) { }

        public EntityValidationException(string message, Exception innerException) : base(message, innerException) { }
    }
}