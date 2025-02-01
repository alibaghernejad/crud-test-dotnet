Feature: Get Customer
As a user
I want to retrieve customer details
So that I can view their stored information in the system.

    Scenario: Successfully retrieve an existing customer
        Given a customer exists with email "john.doe@example.com"
        When I send a GET request to "/api/customers/1
        Then the response should be "200 OK"
        And the response should contain the customer's details

    Scenario: Customer not found
        Given no customer exists with email "nonexistent@example.com"
        When I send a GET request to "/api/customers/nonexistentcustomer"
        Then the response should be "404 Not Found"
