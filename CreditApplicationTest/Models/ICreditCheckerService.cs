using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditApplicationTest.Models
{
    public interface ICreditCheckerService
    {
        CreditDecisionType GetTheCreditDecision(decimal appliedAmount);
        decimal GetInterestRate(decimal creditAmount);
        decimal GetTotalFutureDebt(decimal appliedCreditAmount, int termInMonths, decimal currentCreditAmount);
    }
}
