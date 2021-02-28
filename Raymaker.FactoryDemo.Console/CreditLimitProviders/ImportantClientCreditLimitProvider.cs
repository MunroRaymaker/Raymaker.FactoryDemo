namespace Raymaker.FactoryDemo.Console.CreditLimitProviders
{
    public class ImportantClientCreditLimitProvider : ICreditLimitProvider
    {
        private readonly IUserCreditService userCreditService;

        public ImportantClientCreditLimitProvider(IUserCreditService userCreditService)
        {
            this.userCreditService = userCreditService;
        }

        public (bool HasCreditLimit, double CreditLimit) GetCreditLimit(User user)
        {
            var creditLimit = this.userCreditService.GetCreditLimit(user.FirstName, user.DateOfBirth);
            creditLimit = creditLimit * 2;
            return (true, creditLimit);
        }

        public string NameRequirement { get; } = "ImportantClient";
    }
}