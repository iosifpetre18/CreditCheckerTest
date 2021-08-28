using CreditApplicationTest;
using CreditApplicationTest.BLL;
using CreditApplicationTest.Controllers;
using CreditApplicationTest.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;

namespace CreditCheckerTests
{
    public class CreditCheckerControllerTests
    {
        private Mock<ICreditCheckerService> _creditCheckerService;
        private CreditCheckerController _creditCheckerController;
        private decimal appliedCreditAmount = 0;
        private int term = 0;
        private decimal currentCreditAmount = 0;
        public CreditCheckerControllerTests()
        {
            _creditCheckerService = new Mock<ICreditCheckerService>();
            _creditCheckerService.Setup(s => s.GetTotalFutureDebt(appliedCreditAmount, term, currentCreditAmount)).Verifiable();
            _creditCheckerService.Setup(s => s.GetTheCreditDecision(appliedCreditAmount)).Verifiable();
            _creditCheckerService.Setup(s => s.GetInterestRate(appliedCreditAmount)).Verifiable();
            _creditCheckerController =
                new CreditCheckerController(this._creditCheckerService.Object);
        }

        //naming convention: MethodName_StateUnderTest_ExpectedBehavior
        [Fact]
        public void GetTheCreditDecision_ValidateCreditAmountInput_BadRequest()
        {
            var result = _creditCheckerController.Get(appliedCreditAmount, term, currentCreditAmount).Result;
            _creditCheckerService.Verify(s => s.GetTheCreditDecision(appliedCreditAmount), Times.Never);
            _creditCheckerService.Verify(s => s.GetTotalFutureDebt(appliedCreditAmount, term, currentCreditAmount), Times.Never);
            _creditCheckerService.Verify(s => s.GetInterestRate(appliedCreditAmount), Times.Never);

            // Assert
            var notFoundObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Credit Amount is not valid", notFoundObjectResult.Value);
        }

        [Fact]
        public void GetTheCreditDecision_ValidateTermInput_BadRequest()
        {
            appliedCreditAmount = 1000;
            var result = _creditCheckerController.Get(appliedCreditAmount, term, currentCreditAmount).Result;
            _creditCheckerService.Verify(s => s.GetTheCreditDecision(appliedCreditAmount), Times.Never);
            _creditCheckerService.Verify(s => s.GetTotalFutureDebt(appliedCreditAmount, term, currentCreditAmount), Times.Never);
            _creditCheckerService.Verify(s => s.GetInterestRate(appliedCreditAmount), Times.Never);

            // Assert
            var notFoundObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("The Term must be bigger then 0 months", notFoundObjectResult.Value);
        }

        [Fact]
        public void GetTheCreditDecision_ValidateCurrentCreditAmountInput_BadRequest()
        {
            appliedCreditAmount = 1000;
            term = 12;
            currentCreditAmount = -10;
            var result = _creditCheckerController.Get(appliedCreditAmount, term, currentCreditAmount).Result;
            _creditCheckerService.Verify(s => s.GetTheCreditDecision(appliedCreditAmount), Times.Never);
            _creditCheckerService.Verify(s => s.GetTotalFutureDebt(appliedCreditAmount, term, currentCreditAmount), Times.Never);
            _creditCheckerService.Verify(s => s.GetInterestRate(appliedCreditAmount), Times.Never);

            // Assert
            var notFoundObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("The Current Credit Amount is not valid", notFoundObjectResult.Value);
        }

        [Fact]
        public void GetTheCreditDecision_ValidInputParams_OkRequest()
        {
            appliedCreditAmount = 1000;
            term = 12;
            currentCreditAmount = 0;
            var result = _creditCheckerController.Get(appliedCreditAmount, term, currentCreditAmount).Result;
            _creditCheckerService.Verify(s => s.GetTheCreditDecision(appliedCreditAmount), Times.Once);
            _creditCheckerService.Verify(s => s.GetTotalFutureDebt(appliedCreditAmount, term, currentCreditAmount), Times.Once);
            _creditCheckerService.Verify(s => s.GetInterestRate(It.IsAny<decimal>()), Times.Once);

            var okObjectResult = Assert.IsType<OkObjectResult>(result);
        }
    }
}
