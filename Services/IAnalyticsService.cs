using System;
using System.Collections.Generic;

namespace HseBank.Services
{
    public interface IAnalyticsService
    {
        int CalculateNetIncome(DateTime start, DateTime end);
    }
}
