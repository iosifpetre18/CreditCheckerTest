using System;
using System.Collections.Generic;
using System.Linq;
using CreditApplicationTest.BLL;
using CreditApplicationTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CreditApplicationTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreditCheckerController : ControllerBase
    {
        private ICreditCheckerService _creditService;
        public CreditCheckerController(ICreditCheckerService creditService)
        {
            _creditService = creditService;
        }

        [HttpGet]
        public ActionResult<CreditDecisionInfo> Get(decimal appliedCreditAmount, int term, decimal currentCreditAmount)
        {
            if (appliedCreditAmount <= 0)
                return BadRequest("Credit Amount is not valid");

            if (term <= 0)
                return BadRequest("The Term must be bigger then 0 months");

            if (currentCreditAmount < 0)
                return BadRequest("The Current Credit Amount is not valid");

            var decisionInfo = new CreditDecisionInfo();
            var creditDecision = _creditService.GetTheCreditDecision(appliedCreditAmount);
            decisionInfo.Decision = creditDecision.ToString();

            if (creditDecision == CreditDecisionType.Yes)//there is no reason to calculate the Interest Rate if the Decision is No
            {
                var totalfutureDebt = _creditService.GetTotalFutureDebt(appliedCreditAmount, term, currentCreditAmount);
                var interestRate = _creditService.GetInterestRate(totalfutureDebt);
                decisionInfo.InterestRate = interestRate;
            }

            return Ok(decisionInfo);
        }
    }
}
