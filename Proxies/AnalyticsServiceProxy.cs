using System;
using System.IO;
using HseBank.Services;
using Newtonsoft.Json;

namespace HseBank.Proxies
{
    public class AnalyticsServiceProxy : IAnalyticsService
    {
        private readonly IAnalyticsService _realService;
        private readonly string _logFilePath;

        public AnalyticsServiceProxy(IAnalyticsService realService, string logFilePath)
        {
            _realService = realService;
            _logFilePath = logFilePath;
        }

        private void Log(string message)
        {
            string logMessage = $"{DateTime.Now}: {message}";
            File.AppendAllText(_logFilePath, logMessage + Environment.NewLine);
        }

        public int CalculateNetIncome(DateTime start, DateTime end)
        {
            Log($"Вызов CalculateNetIncome с датами: {start} - {end}");
            int result = _realService.CalculateNetIncome(start, end);
            Log($"Результат CalculateNetIncome: {result}");
            return result;
        }
    }
}
