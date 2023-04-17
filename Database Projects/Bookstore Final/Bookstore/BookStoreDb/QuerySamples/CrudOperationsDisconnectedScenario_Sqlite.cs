using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Bogus;
using BookStoreDb.Core;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace BookStoreDb.QuerySamples;

// ===================================================
// NOTE: Since the Sqlite file is copied to the output folder for every unit test run
// The database file being modified is the one in the output folder and NOT the originally
// generated file.
// ===================================================
internal class CrudOperationsDisconnectedScenario_Sqlite
{

    [Test]
    public void Add_NewAuthor()
    {
        using var context = GetContext();
        // create the author you want to add
        var faker = new Faker("ja");

        var author = new Author();

        // Adding new entities in the disconnected state is the same as in the connected state.
        // Simply add a new entity to the Set<> class then call SaveChanges.

        // note that there is no need to provide the AuthorId
        // since it is auto generated
        // This author has no related entities e.g. Wrotes entities.
        author.Name = faker.Name.FullName();
        author.Address = faker.Address.SecondaryAddress();

        context.Set<Author>().Add(author);

        // alternatively
        // context.Authors.Add(author);

        context.SaveChanges();

        // after context.SaveChanges(), all auto-generated columns will be generated
        Console.WriteLine($"generated author id {author.AuthorId}");
    }

    [Test]
    public void Remove_ExistingAuthor()
    {
        // simulate a disconnected state, i.e. fetch an author outside of this method
        // using a different instance of a DbContext
        var authorToRemove = new Author();
        authorToRemove.AuthorId = 7;

        // NOTE: we use a dummy so that we don't actually remove records from the database
        using var context = new DummySqlLiteContext();

        // check if authorId == 6 does exists in the database
        var doesExists = context.Authors.AsNoTracking()
            .First(c => c.AuthorId == authorToRemove.AuthorId);

        // You can remove any author (even newly instantiated authors)
        // as long as the primary key still exists in the database.
        context.Set<Author>().Remove(authorToRemove);
        context.SaveChanges();
    }

    [Test]
    public void Update_ExistingAuthor()
    {

        // NOTE: at this point you have no idea of the details of authorId = 7
        // update records of authorId = 7
        var authorToUpdate = new Author();
        authorToUpdate.AuthorId = 7;

        authorToUpdate.Name = "XXX";
        authorToUpdate.Address = "Jacinto";

        using var context = GetContext();

        context.Set<Author>().Update(authorToUpdate);
        context.SaveChanges();

        Console.WriteLine("After Edit details:");
        Console.WriteLine($"{authorToUpdate.Name}   {authorToUpdate.Address}");
    }

    private static BookStoreLiteContext GetContext()
    {
        return new BookStoreLiteContext();
    }
}

public class DummySqlLiteContext : BookStoreLiteContext
{
    public override int SaveChanges()
    {
        return 0;
    }
}