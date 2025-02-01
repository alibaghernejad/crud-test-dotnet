Feature: Delete Customer
As a user
I want to Delete an exists Customer 
So that I can view their stored information in the system.

    Scenario: Successfully Delete an existing customer
        Given a customer exists with id 1
        When I send a DELETE request to "/api/customers/1
        Then the response should be "204 NO Content"

    Scenario: Customer not found
        Given no customer exists with id 0
        When I send a DELETE request to "/api/customers/0"
        Then the response should be "404 Not Found"