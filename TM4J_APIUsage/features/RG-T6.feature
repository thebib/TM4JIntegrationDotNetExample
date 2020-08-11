Feature: Rejection then Approve
    @TestCaseKey=RG-T6
    Scenario: Rejection then Approve
        Given the form is loaded
        When the amount requested is £5000
        And the term is 3 months
        And the user completes the user information form
        When the income minus expenditure is greater than £400 but less than £500
        Then the user is shown the product request form with a capped maximum
        When the user submits the from
        Then the loan is approved