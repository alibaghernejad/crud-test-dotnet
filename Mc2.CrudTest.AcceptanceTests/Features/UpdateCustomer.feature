Feature: Update Customer
As a user,
I want to update an existing customer,
So that their information remains accurate.

    Scenario: Successfully update an existing customer
        Given a customer exists in the database
        When I update the customer's details
        Then the customer's information should be updated and return.

    Scenario: Fail to update a non-existing customer
        Given a customer does not exist
        When I try to update the customer
        Then I should receive a "404 Not Found" error
