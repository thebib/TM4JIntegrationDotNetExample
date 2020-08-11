using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace TM4J_APIUsage
{
    [Binding]
    public class RejectionThenApproveSteps
    {
        [Given(@"the form is loaded")]
        public void GivenTheFormIsLoaded()
        {
        }
        
        [When(@"the amount requested is £(.*)")]
        public void WhenTheAmountRequestedIs(int p0)
        {
        }
        
        [When(@"the term is (.*) months")]
        public void WhenTheTermIsMonths(int p0)
        {
            Assert.Fail();
        }
        
        [When(@"the user completes the user information form")]
        public void WhenTheUserCompletesTheUserInformationForm()
        {
        }
        
        [When(@"the income minus expenditure is greater than £(.*) but less than £(.*)")]
        public void WhenTheIncomeMinusExpenditureIsGreaterThanButLessThan(int p0, int p1)
        {
        }
        
        [When(@"the user submits the from")]
        public void WhenTheUserSubmitsTheFrom()
        {
        }
        
        [Then(@"the user is shown the product request form with a capped maximum")]
        public void ThenTheUserIsShownTheProductRequestFormWithACappedMaximum()
        {
        }
        
        [Then(@"the loan is approved")]
        public void ThenTheLoanIsApproved()
        {
        }
    }
}
