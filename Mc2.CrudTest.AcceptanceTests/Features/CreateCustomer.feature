Feature: Create Customer
  As a user,
  I want to create a new customer
  So that the customer can be stored in the database.

  Scenario: Successfully create a valid customer
    Given I have a valid customer with a unique name, date of birth, email, phone number, and bank account number
    When I submit the customer creation request
    Then the customer should be stored in the database
    And I should receive a success message.

  Scenario: Reject duplicate customer
    Given a customer already exists with the same first name, last name, and date of birth
    When I submit another customer with the same details
    Then the creation should be rejected
    And I should receive a "Customer already exists" error message.

  Scenario: Reject invalid phone number
    Given I provide an invalid phone number
    When I submit the customer creation request
    Then the request should be rejected
    And I should receive an "Invalid phone number" error message.

  Scenario: Reject invalid email address
    Given I provide an invalid email address
    When I submit the customer creation request
    Then the request should be rejected
    And I should receive an "Invalid email address" error message.

  Scenario: Reject duplicate email
    Given a customer already exists with the same email address
    When I submit another customer with the same email
    Then the request should be rejected
    And I should receive a "Duplicate email address" error message.

  Scenario: Reject invalid bank account number
    Given I provide an invalid bank account number
    When I submit the customer creation request
    Then the request should be rejected
    And I should receive an "Invalid bank account number" error message.
