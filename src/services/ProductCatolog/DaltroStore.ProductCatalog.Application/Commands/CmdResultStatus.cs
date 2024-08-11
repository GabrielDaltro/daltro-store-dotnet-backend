namespace DaltroStore.ProductCatalog.Application.Commands
{
    public enum CmdResultStatus
    {
        Success,
        AggregateNotFound,
        InvalidDomainOperation,
    }
}