using Bogus;
using GlobalComputerSolutionsDb.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalComputerSolutionsDb.Configuration;

public class BillConfiguration : IEntityTypeConfiguration<Bill>
{
    public void Configure(EntityTypeBuilder<Bill> d)
    {
        d.ToTable("Bill");

        var bills = GenerateData();

        d.HasData(bills);

    }

    private List<Bill> GenerateData()
    {
        var list = new List<Bill>();

        var faker = new Faker();
        Randomizer.Seed = new Random(157649);

        for (int i = 1; i <= 200; i++)
        {
            var bill = new Bill();
            bill.BillId = i;
            bill.CustomerId = (i % 200) + 1;
            bill.DateSent = faker.Date.Past(3);
                
            list.Add(bill);
        }

        return list;
    }
}