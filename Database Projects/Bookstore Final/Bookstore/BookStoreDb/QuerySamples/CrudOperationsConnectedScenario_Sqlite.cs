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

namespace BookStoreDb.QuerySamples
{
    // ===================================================
    // NOTE: Since the Sqlite file is copied to the output folder for every unit test run
    // The database file being modified is the one in the output folder and NOT the originally
    // generated file.
    // ===================================================
    internal class CrudOperationsConnectedScenario_Sqlite
    {

        [Test]
        public void Add_NewAuthor()
        {
            using var context = GetContext();
            // create the author you want to add
            var faker = new Faker("ja");

            var author = new Author();

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
            using var context = GetContext();

            // get the author you want to remove
            var authorToRemove = context.Authors.First(c=>c.AuthorId == 2000);

            context.Set<Author>().Remove(authorToRemove);
            // context.SaveChanges();
        }

        [Test]
        public void Update_ExistingAuthor()
        {
            using var context = GetContext();

            // get the author you want to remove
            var authorToUpdate = context.Authors.First(c=>c.AuthorId == 1);

            Console.WriteLine("Previous details:");
            Console.WriteLine($"{authorToUpdate.Name}   {authorToUpdate.Address}");

            // simply modify the properties
            authorToUpdate.Name = "Dirk";
            authorToUpdate.Address = "Roxas Ave.";

            context.SaveChanges();

            Console.WriteLine("After Edit details:");
            Console.WriteLine($"{authorToUpdate.Name}   {authorToUpdate.Address}");
        }

        private static BookStoreLiteContext GetContext()
        {
            return new BookStoreLiteContext();
        }
    }
}
