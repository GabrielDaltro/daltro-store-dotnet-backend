namespace DaltroStore.ProductCatalog.Application.Commands
{
    public class CommandResult
    {
        public bool Success { get; init; }

        public string Error { get; init; }

        public CommandResult(bool success, string error)
        {
            Success = success;
            Error = error;
        }
    }
}