namespace DaltroStore.Core.DomainObjects
{
    public static class AssertionConcern
    {
        public static void AssertIsNotEmpty(string str, string message)
        {
            if (str.Trim().Length == 0)
                throw new DomainException(message);
        }

        public static void AssertIsNotNull(object obj, string message)
        {
            if (obj == null)
                throw new DomainException(message);
        }

        public static void AssertIsGreaterThan<T>(T actual, T expcted, string message) where T : IComparable
        {
            if (actual.CompareTo(expcted) <= 0)
                throw new DomainException(message);
        }

        public static void AssertIsGreaterOrEqualsThan<T>(T actual, T expcted, string message) where T : IComparable
        {
            if (actual.CompareTo(expcted) < 0)
                throw new DomainException(message);
        }
    }
}