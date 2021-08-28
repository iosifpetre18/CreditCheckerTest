using CreditApplicationTest.BLL;
using System;
using Xunit;

namespace CreditCheckerTests
{
    public class CreditCheckerServiceTests
    {
        private CreditCheckerService _creditCheckerService = null;
        public CreditCheckerServiceTests()
        {
            if (_creditCheckerService == null)
            {
                _creditCheckerService = new CreditCheckerService();
            }
        }

        //naming convention: MethodName_StateUnderTest_ExpectedBehavior
        [Fact]
        public void GetTheCreditDecision_CorrectCreditAmounts_Yes()
        {
            var decision1 = _creditCheckerService.GetTheCreditDecision(2000);
            Assert.Equal(CreditApplicationTest.CreditDecisionType.Yes, decision1);

            var decision2 = _creditCheckerService.GetTheCreditDecision(50000);
            Assert.Equal(CreditApplicationTest.CreditDecisionType.Yes, decision1);

            var decision3 = _creditCheckerService.GetTheCreditDecision(69000);
            Assert.Equal(CreditApplicationTest.CreditDecisionType.Yes, decision3);
        }

        [Fact]
        public void GetTheCreditDecision_IncorrectCreditAmounts_No()
        {
            var decision1 = _creditCheckerService.GetTheCreditDecision(-15000);
            Assert.Equal(CreditApplicationTest.CreditDecisionType.No, decision1);

            var decision2 = _creditCheckerService.GetTheCreditDecision(1000);
            Assert.Equal(CreditApplicationTest.CreditDecisionType.No, decision1);

            var decision3 = _creditCheckerService.GetTheCreditDecision(70000);
            Assert.Equal(CreditApplicationTest.CreditDecisionType.No, decision3);
        }

        [Fact]
        public void GetInterestRate_LessThen20000_3percent()
        {
            var interestRate = _creditCheckerService.GetInterestRate(-15000);
            Assert.Equal(3, interestRate);

            interestRate = _creditCheckerService.GetInterestRate(19000);
            Assert.Equal(3, interestRate);
        }

        [Fact]
        public void GetInterestRate_SecondRangeDebt_4percent()
        {
            var interestRate = _creditCheckerService.GetInterestRate(20000);
            Assert.Equal(4, interestRate);

            interestRate = _creditCheckerService.GetInterestRate(39000);
            Assert.Equal(4, interestRate);
        }

        [Fact]
        public void GetInterestRate_ThirdRangeDebt_5percent()
        {
            var interestRate = _creditCheckerService.GetInterestRate(40000);
            Assert.Equal(5, interestRate);

            interestRate = _creditCheckerService.GetInterestRate(45000);
            Assert.Equal(5, interestRate);

            interestRate = _creditCheckerService.GetInterestRate(59000);
            Assert.Equal(5, interestRate);
        }

        [Fact]
        public void GetInterestRate_FourthRangeDebt_6percent()
        {
            var interestRate = _creditCheckerService.GetInterestRate(59600);
            Assert.Equal(6, interestRate);

            interestRate = _creditCheckerService.GetInterestRate(60000);
            Assert.Equal(6, interestRate);

            interestRate = _creditCheckerService.GetInterestRate(100000);
            Assert.Equal(6, interestRate);
        }

        [Fact]
        public void GetTotalFutureDebt_Input40000And6Months_Expect_41000()
        {
            var debt = _creditCheckerService.GetTotalFutureDebt(40000, 6, 0);
            Assert.Equal(41000, debt);
        }

        [Fact]
        public void GetTotalFutureDebt_Input45000And12MonthsAndCurrent2500_Expect_49500()
        {
            var debt = _creditCheckerService.GetTotalFutureDebt(45000, 12, 2500);
            Assert.Equal(49750, debt);
        }

        [Fact]
        public void GetTotalFutureDebt_Input70000And12Months_Expect_74200()
        {
            var debt = _creditCheckerService.GetTotalFutureDebt(70000, 12, 0);
            Assert.Equal(74200, debt);
        }

        [Fact]
        public void GetTotalFutureDebt_Input10000And6Months_Expect_10150()
        {
            var debt = _creditCheckerService.GetTotalFutureDebt(10000, 6, 0);
            Assert.Equal(10150, debt);
        }

        [Fact]
        public void GetTotalFutureDebt_Input10000And6MonthsAndCurrent150_Expect_10300()
        {
            var debt = _creditCheckerService.GetTotalFutureDebt(10000, 6, 150);
            Assert.Equal(10300, debt);
        }
    }
}
