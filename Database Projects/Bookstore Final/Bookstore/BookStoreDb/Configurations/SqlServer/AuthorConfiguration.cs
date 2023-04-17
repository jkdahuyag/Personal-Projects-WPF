using Bogus;
using BookStoreDb.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStoreDb.Configurations.SqlServer;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> d)
    {
        d.ToTable("Author");
        var authors = GenerateData();

        foreach (var author in authors) d.HasData(author);

    }
    private List<Author> GenerateData()
    {
        var list = new List<Author>();

        var faker = new Faker();

        for (int i = 1; i <= 100; i++)
        {
            var author = new Author();
            author.AuthorId = i;
            author.Name = faker.Name.FullName();
            author.Address = faker.Address.FullAddress();

            list.Add(author);
        }

        return list;
    }
}
