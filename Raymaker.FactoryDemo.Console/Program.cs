using System;

namespace Raymaker.FactoryDemo.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Hello World!");

            var userService = new UserService();

            var result = userService.AddUser("Tim", "Corey", "tim.corey@pluralsight.com", new DateTime(1980, 1, 1),
                "");

            System.Console.WriteLine($"Added Tim: {result}");
        }
    }
}
