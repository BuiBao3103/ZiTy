using Billing.Domain.Core.Models;

namespace Billing.Domain.Core.Repositories;

public interface IStatisticRepository
{
    public Task<List<MonthlyRevenueStatistics>> GetStatisticsRevenue(string startMonth, string endMonth);
}
