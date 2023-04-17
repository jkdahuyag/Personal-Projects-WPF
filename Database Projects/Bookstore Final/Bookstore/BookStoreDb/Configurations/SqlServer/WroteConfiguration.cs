using Bogus;
using BookStoreDb.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStoreDb.Configurations.SqlServer;

public class WroteConfiguration : IEntityTypeConfiguration<Wrote>
{
    public void Configure(EntityTypeBuilder<Wrote> d)
    {
        d.ToTable("Wrote");

        d.HasIndex(c => new { c.AuthorId, c.BookId }).IsUnique();
        
        var faker = new Faker();

        int numWrotes = 5000;

        for (int i = 0; i < numWrotes; i++)
        {
            var wrote = new Wrote
            {
                BookAuthorId = i+1,
                AuthorId = i%100 + 1,
                BookId = (i  + (int)Math.Floor(i / 1000f) )% 1000 + 1,
                RoyaltyRate = faker.Random.Float(10, 90000)
            };

            d.HasData(wrote);
        }

    }
}
