namespace DaltroStore.Core.DomainObjects
{
    public class EntityNotFoundDomainException : DomainException 
    {
        public EntityNotFoundDomainException() { }

        public EntityNotFoundDomainException(string message) : base(message) { }

        public EntityNotFoundDomainException(string message, Exception innerException) : base(message, innerException) { }
    }
}