namespace DaltroStore.ProductCatalog.Application.Commands
{
    public class CommandResult<T> : CommandResult
    {
        public T Value { get; init; }

        public CommandResult(CmdResultStatus status, T value, string error) : base(status, error)
        {
            this.Value = value;
        }
    }
}
