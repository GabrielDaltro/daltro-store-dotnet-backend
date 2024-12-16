using DaltroStore.Core.DomainObjects;

namespace DaltroStore.Customers.Domain.Models
{
    public class Cpf
    {
        public const int CpfMaxLength = 11;

        private readonly string value;

        public Cpf(string value)
        {
            this.value = CleanCpf(value);
            if (!IsValid())
                throw new EntityValidationException($"{value} is not a valid CPF");
        }

        public string Value => value;

        public bool IsValid()
        {
            if (value.Length != 11 || value.Distinct().Count() == 1)
                return false;

            var digits = value.Select(c => int.Parse(c.ToString())).ToArray();
            var firstCheck = CalculateDigit(digits, 9);
            var secondCheck = CalculateDigit(digits, 10);

            return digits[9] == firstCheck && digits[10] == secondCheck;
        }

        private static int CalculateDigit(int[] digits, int length)
        {
            var sum = 0;
            for (var i = 0; i < length; i++)
            {
                sum += digits[i] * (length + 1 - i);
            }
            var remainder = sum % 11;
            return remainder < 2 ? 0 : 11 - remainder;
        }

        private static string CleanCpf(string cpf)
        {
            return new string(cpf.Where(char.IsDigit).ToArray());
        }

        public override string ToString()
        {
            return $"{value.Substring(0, 3)}.{value.Substring(3, 3)}.{value.Substring(6, 3)}-{value.Substring(9, 2)}";
        }
    }

}
