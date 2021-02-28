using System;

namespace Raymaker.FactoryDemo.Console
{
    public interface IDateTimeProvider
    {
        DateTime DateTimeNow { get; }
    }

    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime DateTimeNow => DateTime.Now;
    }
}