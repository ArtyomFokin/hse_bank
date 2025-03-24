using System;
using System.Diagnostics;

namespace HseBank.Commands
{
    public class TimedCommandDecorator : IFinancialCommand
    {
        private readonly IFinancialCommand _innerCommand;

        public TimedCommandDecorator(IFinancialCommand innerCommand)
        {
            _innerCommand = innerCommand;
        }

        public object Execute()
        {
            var stopwatch = Stopwatch.StartNew();
            var result = _innerCommand.Execute();
            stopwatch.Stop();
            Console.WriteLine($"Команда выполнена за {stopwatch.ElapsedMilliseconds} мс.");
            return result;
        }
    }
}
