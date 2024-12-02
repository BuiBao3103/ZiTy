using Billing.Domain.Core.Models;
using Billing.Domain.Core.Repositories;
using Billing.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Billing.Infrastructure.Repositories
{
    public class StatisticRepository(BillingDbContext dbContext) : IStatisticRepository
    {
        private readonly BillingDbContext _dbContext = dbContext;

        public async Task<List<MonthlyRevenueStatistics>> GetStatisticsRevenue(string startMonth, string endMonth)
        {
            return await _dbContext.Bills
              .Where(b => b.Monthly.CompareTo(startMonth) >= 0 && b.Monthly.CompareTo(endMonth) <= 0 && b.Status == "PAID")
              .GroupBy(b => b.Monthly)
              .Select(g => new MonthlyRevenueStatistics
              {
                  Month = g.Key,
                  TotalRevenue = g.Sum(b => (decimal)b.TotalPrice)
              })
              .OrderBy(mr => mr.Month)
              .ToListAsync();
        }
    }
}
