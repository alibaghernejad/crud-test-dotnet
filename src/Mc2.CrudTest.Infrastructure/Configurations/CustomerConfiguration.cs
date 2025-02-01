using Mc2.CrudTest.Domain.CustomerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mc2.CrudTest.Infrastructure.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            // Map to the "Customer" table
            builder.ToTable("Customer");

            // Primary key (assuming your Customer entity has a CustomerId property)
            builder.HasKey(c => c.Id);

            // FirstName: NVARCHAR(50) NOT NULL
            builder.Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            // LastName: NVARCHAR(50) NOT NULL
            builder.Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(50);

            // DateOfBirth: DATE NOT NULL
            builder.Property(c => c.DateOfBirth)
                .IsRequired()
                .HasColumnType("date");

            // PhoneNumber: VARCHAR(20) NOT NULL (E.164 format recommended)
            builder.Property(c => c.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false); // Use non-Unicode to map to VARCHAR

            // Email: NVARCHAR(320) NOT NULL
            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(254);

            // BankAccountNumber: VARCHAR(20) NOT NULL
            builder.Property(c => c.BankAccountNumber)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

            // Unique constraint on (FirstName, LastName, DateOfBirth)
            builder.HasIndex(c => new { c.FirstName, c.LastName, c.DateOfBirth })
                .IsUnique()
                .HasDatabaseName("UQ_Customer_NameDOB");

            // Unique constraint on Email
            builder.HasIndex(c => c.Email)
                .IsUnique()
                .HasDatabaseName("UQ_Customer_Email");
        }
    }
}
