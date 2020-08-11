Feature: Journey Resume
    @TestCaseKey=RG-T5
    Scenario: Journey Resume
        Given the user is on the basic information page
        When the user enters an email they have access to
        And submits the form with populated data
        Then the user receives an email
        When the user clicks on the link in the email
        Then the user is navigated to there previous journey