using Mc2.CrudTest.Domain.CustomerAggregate;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Infrastructure.Data;

public static class SeedData
{
    public static readonly Customer Customer1 = new Customer() {FirstName = "Ali", LastName = "Baghernezhad", Email = "alibaghernezhad@gmail.com", DateOfBirth = DateTime.Parse("1989-01-17T15:04:05Z"),BankAccountNumber = "GB29NWBK60161331926819", PhoneNumber = "+989155323816"};
    public static readonly Customer Customer2 = new Customer() {FirstName = "John", LastName = "Doe", Email = "johndoe@gmail.com", DateOfBirth = DateTime.Parse("1965-01-17T15:04:05Z"),BankAccountNumber = "TK29NWBK60161331926819", PhoneNumber = "+1 415-555-2671"};

    public static async Task InitializeAsync(AppDbContext dbContext)
    {
        if (await dbContext.Customers.AnyAsync()) return; // DB has been seeded

        await PopulateTestDataAsync(dbContext);
    }

    public static async Task PopulateTestDataAsync(AppDbContext dbContext)
    {
        dbContext.Customers.AddRange([Customer1, Customer2]);
        await dbContext.SaveChangesAsync();
    }
}