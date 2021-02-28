using System;

namespace Raymaker.FactoryDemo.Console
{
    public class User
    {
        public User()
        {
            CreditLimit = 600.0d;
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool HasCreditLimit { get; set; }
        public double CreditLimit { get; set; }
    }
}