using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreDb.Configurations.SqlServer;
using BookStoreDb.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookStoreDb;

public partial class BookStoreContext : DbContext
{
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Wrote> Wrotes { get; set; }
    public DbSet<Author> Authors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder op)
    {
        if (!op.IsConfigured)
            op.UseSqlServer(@"Server=JF607A-013\MAIN;Initial Catalog=BookstoreDb;Trusted_Connection=True");
    }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.ApplyConfiguration(new AuthorConfiguration());
        mb.ApplyConfiguration(new WroteConfiguration());
        mb.ApplyConfiguration(new PublisherConfiguration());
        mb.ApplyConfiguration(new BookConfiguration());
    }
}