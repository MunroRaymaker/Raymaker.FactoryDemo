namespace Raymaker.FactoryDemo.Console.CreditLimitProviders
{
    public class DefaultCreditLimitProvider : ICreditLimitProvider
    {
        private readonly IUserCreditService userCreditService;

        public DefaultCreditLimitProvider(IUserCreditService userCreditService)
        {
            this.userCreditService = userCreditService;
        }

        public (bool HasCreditLimit, double CreditLimit) GetCreditLimit(User user)
        {
            user.HasCreditLimit = true;
            var creditLimit = this.userCreditService.GetCreditLimit(user.FirstName, user.DateOfBirth);
            return (true, creditLimit);
        }

        public string NameRequirement { get; } = string.Empty;
    }
}