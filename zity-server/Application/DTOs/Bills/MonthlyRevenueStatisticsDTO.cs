using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Bills
{
    public class MonthlyRevenueStatisticsDTO
    {
            public string Month { get; set; }
            public decimal TotalRevenue { get; set; }
    }
}
