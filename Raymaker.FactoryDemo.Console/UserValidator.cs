using System;

namespace Raymaker.FactoryDemo.Console
{
    public class UserValidator
    {
        private readonly IDateTimeProvider dateTimeProvider;

        public UserValidator(IDateTimeProvider dateTimeProvider)
        {
            this.dateTimeProvider = dateTimeProvider;
        }

        public bool Is21OrOlder(DateTime dateOfBirth)
        {
            var now = this.dateTimeProvider.DateTimeNow;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            if (age < 21)
            {
                return false;
            }

            return true;
        }

        public bool HasCreditLimitLessThan500(User user)
        {
            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return true;
            }

            return false;
        }
        public bool HasValidEmail(string email)
        {
            if (!email.Contains("@") && !email.Contains("."))
            {
                return false;
            }

            return true;
        }

        public bool HasValidName(string firname, string surname)
        {
            if (string.IsNullOrEmpty(firname) || string.IsNullOrEmpty(surname))
            {
                return false;
            }

            return true;
        }
    }
}