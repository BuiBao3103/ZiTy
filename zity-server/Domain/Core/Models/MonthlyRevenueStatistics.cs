using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Models
{
    public class MonthlyRevenueStatistics
    {
        public string Month { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
