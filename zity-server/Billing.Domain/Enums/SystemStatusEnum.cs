using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing.Domain.Enums
{
    public enum SystemStatusEnum
    {
        PREPAYMENT,
        PAYMENT,
        OVERDUE,
        DELINQUENT,
    }
}
