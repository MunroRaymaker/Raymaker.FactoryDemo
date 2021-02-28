namespace Raymaker.FactoryDemo.Console.CreditLimitProviders
{
    public interface ICreditLimitProvider
    {
        (bool HasCreditLimit, double CreditLimit) GetCreditLimit(User user);

        public string NameRequirement { get; }
    }
}
