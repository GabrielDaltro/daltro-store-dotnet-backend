namespace DaltroStore.ProductCatalog.Application.Commands
{
    public class CommandResult
    {
        public CmdResultStatus Status { get; init; }

        public string Error { get; init; }

        public CommandResult(CmdResultStatus status, string error)
        {
            Status = status;
            Error = error;
        }
    }
}