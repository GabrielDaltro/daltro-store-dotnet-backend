namespace DaltroStore.Core.DomainObjects
{
    public class EntityNotFoundException : DomainException 
    {
        public EntityNotFoundException() { }

        public EntityNotFoundException(string message) : base(message) { }

        public EntityNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}