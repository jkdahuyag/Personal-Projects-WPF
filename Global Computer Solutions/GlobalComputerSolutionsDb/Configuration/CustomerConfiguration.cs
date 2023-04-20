using Bogus;
using GlobalComputerSolutionsDb.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalComputerSolutionsDb.Configuration;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> d)
    {
        d.ToTable("Customer");

        d.Property(c => c.FirstName).HasMaxLength(100);
        d.Property(c => c.MiddleName).HasMaxLength(100);
        d.Property(c => c.LastName).HasMaxLength(100);
        d.Property(c => c.Telephone).HasMaxLength(50);

        var customers = GenerateData();

        d.HasData(customers);

    }

    private List<Customer> GenerateData()
    {
        var list = new List<Customer>();

        var faker = new Faker();
        Randomizer.Seed = new Random(629062);

        for (int i = 1; i <= 200; i++)
        {
            var customer = new Customer();
            customer.CustomerId = i;
            customer.LastName = faker.Name.LastName();
            customer.FirstName = faker.Name.FirstName();
            customer.MiddleName = faker.Name.LastName();
            customer.Telephone = faker.Phone.PhoneNumber();
            customer.RegionId = faker.Random.Number(1, 6);
            list.Add(customer);
        }

        return list;
    }
}