using System;
using Raymaker.FactoryDemo.Console.CreditLimitProviders;

namespace Raymaker.FactoryDemo.Console
{
    public class UserService
    {
        private readonly UserValidator userValidator;
        private readonly CreditLimitProviderFactory creditLimitProviderFactory;

        public UserService() : this(
            new DateTimeProvider(),
            new UserCreditService())
        {
        }

        public UserService(
            IDateTimeProvider dateTimeProvider,
            IUserCreditService userCreditService)
        {
            this.userValidator = new UserValidator(dateTimeProvider);
            this.creditLimitProviderFactory = new CreditLimitProviderFactory(userCreditService);
        }

        public bool AddUser(string firname, string surname, string email, DateTime dateOfBirth, string clientName)
        {
            if (!this.userValidator.HasValidName(firname, surname)) return false;
            if (!this.userValidator.HasValidEmail(email)) return false;
            if (!this.userValidator.Is21OrOlder(dateOfBirth)) return false;

            var user = new User
            {
                Email = email,
                FirstName = firname,
                LastName = surname,
                DateOfBirth = dateOfBirth
            };

            ICreditLimitProvider provider = creditLimitProviderFactory.GetProviderByClientName(clientName);
            var (hasCreditLimit, creditLimit) = provider.GetCreditLimit(user);

            user.HasCreditLimit = hasCreditLimit;
            user.CreditLimit = creditLimit;

            if(this.userValidator.HasCreditLimitLessThan500(user)) return false;

            // Add user to database here
            
            return true;
        }
    }
}
