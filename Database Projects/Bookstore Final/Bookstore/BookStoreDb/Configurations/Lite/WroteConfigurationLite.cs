using Bogus;
using BookStoreDb.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStoreDb.Configurations.Lite;

public class WroteConfigurationLite : IEntityTypeConfiguration<Wrote>
{
    public void Configure(EntityTypeBuilder<Wrote> d)
    {
        d.ToTable("Wrote");

        d.HasIndex(c => new { c.AuthorId, c.BookId });

        var faker = new Faker();

        int numWrotes = 5000;

        for (int i = 0; i < numWrotes; i++)
        {
            var wrote = new Wrote
            {
                BookAuthorId = ++i,
                AuthorId = faker.Random.Number(1, 99),
                BookId = faker.Random.Number(1, 1000),
                RoyaltyRate = faker.Random.Float(10, 90000)
            };

            d.HasData(wrote);
        }
    }
}
