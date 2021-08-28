using CreditApplicationTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditApplicationTest.BLL
{
    public class CreditCheckerService : ICreditCheckerService
    {
        public CreditDecisionType GetTheCreditDecision(decimal appliedAmount)
        {
            if (appliedAmount >= 2000 && appliedAmount <= 69000)
            {
                return CreditDecisionType.Yes;
            }
            else
            {
                return CreditDecisionType.No;
            }
        }

        public decimal GetInterestRate(decimal creditAmount)
        {
            //because of Interest rate rules were not covering cases between 39000-40000 and 59000-60000, we rounded the credit amount to thousands
            creditAmount = Math.Round(creditAmount / 1000) * 1000;
            if (creditAmount < 20000)
            {
                return 3;
            }
            else if (creditAmount >= 20000 && creditAmount <= 39000)
            {
                return 4;
            }
            else if (creditAmount >= 40000 && creditAmount <= 59000)
            {
                return 5;
            }
            else if (creditAmount >= 60000)
            {
                return 6;
            }
            return -1;
        }

        public decimal GetTotalFutureDebt(decimal appliedCreditAmount, int termInMonths, decimal currentCreditAmount)
        {
            var anualInterestRatePercent = GetInterestRate(appliedCreditAmount);
            var monthlyInterestRate = anualInterestRatePercent / 12;
            var appliedCreditInterest = Math.Round((appliedCreditAmount * (monthlyInterestRate * (decimal)0.01)) * termInMonths);
            var totalfutureDebt = (appliedCreditAmount + appliedCreditInterest) + currentCreditAmount;

            return totalfutureDebt;
        }
    }
}
