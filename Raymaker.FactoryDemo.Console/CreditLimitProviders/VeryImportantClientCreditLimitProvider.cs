namespace Raymaker.FactoryDemo.Console.CreditLimitProviders
{
    public class VeryImportantClientCreditLimitProvider : ICreditLimitProvider
    {
        public (bool HasCreditLimit, double CreditLimit) GetCreditLimit(User user)
        {
            return (false, 0);
        }

        public string NameRequirement { get; } = "VeryImportantClient";
    }
}