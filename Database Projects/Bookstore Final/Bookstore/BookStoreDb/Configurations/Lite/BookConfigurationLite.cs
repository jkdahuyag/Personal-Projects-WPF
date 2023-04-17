using System.Globalization;
using BookStoreDb.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStoreDb.Configurations.Lite;

public class BookConfigurationLite : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> d)
    {
        d.ToTable("Book");

        d.HasIndex(c => c.Isbn).IsUnique();

        d.Property(c => c.Price)
            .HasComputedColumnSql("[NumPages]*10")
            .IsRequired();

        var books = ExtractFromCsv();

        foreach (var book in books) d.HasData(book);
    }

    private List<Book> ExtractFromCsv()
    {
        var sr = new StreamReader(@"SeedData\Book.csv");
        var list = new List<Book>();

        var line = sr.ReadLine();
        var r = new Random();
        int bookId = 0;

        while (line != null)
        {
            var split = line.Split(',');

            var dateTimeFormatInfo = new DateTimeFormatInfo();

            var date = split[3].Split('/');

            var book = new Book
            {
                BookId = ++bookId,
                PublisherId = r.Next(1,101),
                Isbn = split[0],
                Title = split[1],
                Price = float.Parse(split[2]),
                DatePublished = DateTime.Parse(split[3], dateTimeFormatInfo),
                NumPages = int.Parse(split[4]),
                DaysOld = int.Parse(split[5])
            };

            list.Add(book);

            line = sr.ReadLine();
        }

        return list;
    }

}
