using System;

namespace Raymaker.FactoryDemo.Console
{
    public interface IUserCreditService
    {
        double GetCreditLimit(string firstName, DateTime dateOfBirth);
    }

    public class UserCreditService : IUserCreditService
    {
        public double GetCreditLimit(string firstName, DateTime dateOfBirth)
        {
            return 800;
        }
    }
}