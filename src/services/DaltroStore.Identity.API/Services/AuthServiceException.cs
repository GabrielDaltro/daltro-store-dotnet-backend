namespace DaltroStore.Identity.API.Services
{
    internal class AuthServiceException : Exception
    {
        public AuthServiceException() { }

        public AuthServiceException(string message) : base (message) { }

        public AuthServiceException(string message, Exception innerException) : base (message, innerException) { }
    }
}