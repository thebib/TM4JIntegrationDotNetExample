Feature: Edit Form
    @TestCaseKey=RG-T4
    Scenario: Edit Form
        Given the user has completed the product request form
        When the user clicks to edit the product request form
        Then the product request form is displayed
        When the user completes the form
        And the user clicks enter
        Then the product request is resubmitted and shown to the user