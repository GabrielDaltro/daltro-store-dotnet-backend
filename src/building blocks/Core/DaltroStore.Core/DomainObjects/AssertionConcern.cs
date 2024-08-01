namespace DaltroStore.Core.DomainObjects
{
    public static class AssertionConcern
    {
        public static void AssertNotEmpty(string str, string message)
        {
            if (str.Trim().Length == 0)
                throw new DomainException(message);
        }

        public static void AssertNotNull(object obj, string message)
        {
            if (obj == null)
                throw new DomainException(message);
        }
    }
}