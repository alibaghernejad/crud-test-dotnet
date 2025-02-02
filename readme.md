# DotNet Crud Test: Clean Architecture
I have completed the **Crud-Test-Dotnet** project as part of the grouped talk process. The project follows **Clean Architecture** principles and incorporates **REPR, CQRS, Event Sourcing**, along with **BDD** and **TDD** in its design and implementation in combine to _PostgreSQL_, _Martendb_ and _Sqlite_. Below are the key details:

- **Architecture:** Clean Architecture, REPR, CQRS, Event Sourcing, BDD, and TDD
- **Containerization Support:** Docker & Docker Compose (with PostgreSQL and Marten for Event Sourcing)
- **Framework:** .NET 9.0
- **Programming Language:** C# 13
- **Vertical Slicing Architecture** with **DDD Domain Services** (e.g., Customer Service)
- **ORM:** Entity Framework
- **Documentation:** HTTP Request Files + Swagger UI ([Swagger UI Link](http://localhost:5236/index.html))
- **Authentication/Authorization:** Omitted for Simplicity
- **Delete Mechanism:** Hard Delete + Event Sourcing
- **Validation:** Fluent Validator
- **Mapper:** AutoMapper
- **Testing Frameworks:** xUnit, NUnit
- **Support for multiple Backstores:** SQL Server, SQLite
- **Minimal API Support**

To retrieve event sources after running the Docker Compose file and connecting to the postgres database with this connection string:
```bash
cd crud-test-dotnet/
docker-compose up
# Host=localhost;Database=eventstore;Username=postgres;Password=YOUR_PASSWORD
```
Use the following SQL script:
```sql
SELECT * FROM mt_events ORDER BY seq_id DESC LIMIT 5;

```

For phone number storage, the **E.164 format** (e.g., `+14155552671`) and stored it as `VARCHAR(15)` for strict validation. Although that  **ulong** for the data type as it uses less space , but e.164  still adhering to the standard. This method ensures: (Storing the Country code and information and flags can be persisted into the separated tables )

- **Retains Formatting:** Allows `+`, `()`, `-`, and spaces
- **Prevents Loss of Leading Zeros**
- **Scalability:** Supports future changes in numbering formats
___

Please read each note very carefully!
Feel free to add/change the project structure to a clean architecture to your view.
and if you are not able to work on the FrontEnd project, you can add a Swagger UI
in a new Front project.

Create a simple CRUD application with .NET that implements the below model:
```
Customer {
	FirstName
	LastName
	DateOfBirth
	PhoneNumber
	Email
	BankAccountNumber
}
```
## Practices and patterns:

- [TDD](https://docs.microsoft.com/en-us/visualstudio/test/quick-start-test-driven-development-with-test-explorer?view=vs-2022)
- [BDD](https://en.wikipedia.org/wiki/Behavior-driven_development)
- [DDD](https://en.wikipedia.org/wiki/Domain-driven_design)
- [Clean architecture](https://github.com/jasontaylordev/CleanArchitecture)
- [CQRS](https://en.wikipedia.org/wiki/Command%E2%80%93query_separation#Command_query_responsibility_separation) pattern ([Event sourcing](https://en.wikipedia.org/wiki/Domain-driven_design#Event_sourcing)).
- Clean git commits that show your work progress, each commit must provide your decision making process for each change or selection.

### Validations

- During Create; validate the phone number to be a valid *mobile* number only (Please use [Google LibPhoneNumber](https://github.com/google/libphonenumber) to validate number at the backend).

- A Valid email and a valid bank account number must be checked before submitting the form.

- Customers must be unique in the database: By `Firstname`, `Lastname`, and `DateOfBirth`.

- Email must be unique in the database.

### Storage

- Store the phone number in a database with minimized space storage (choose `varchar`/`string`, or `ulong` whichever store less space).

### Delivery
- Please clone this repository in a new GitHub repository in private mode and share with ID: `mason-chase` in private mode on github.com, make sure you do not erase my commits and then create a [pull request](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/proposing-changes-to-your-work-with-pull-requests/about-pull-requests) (code review).

## Nice to do:
- Blazor Web.
- Docker-compose project that loads database service automatically which `docker-compose up`

