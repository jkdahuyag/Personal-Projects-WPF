using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreDb.Core;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace BookStoreDb.QuerySamples
{
    internal class ReadQueries
    {
        [Test]
        public void GetRelatedDataBookAcrossGraph()
        {
            using var context = GetContext();

            var books = context.Books
                .Include(c => c.PublisherLink)
                .Include(c => c.Wrotes)
                .ThenInclude(c => c.AuthorLink)
                .ToList();
        }

        private static BookStoreContext GetContext()
        {
            return new BookStoreContext();
        }

        [Test]
        public void GetRelatedDataPublishersAcrossGraph()
        {
            using var context = GetContext();

            var query = context.Publishers
                .Include(c => c.Books)
                .ThenInclude(c => c.Wrotes)
                .ThenInclude(c => c.AuthorLink);

            var publishers = query
                .ToList();

            Console.WriteLine(query.ToQueryString());

            foreach (var item in publishers)
            {
                Console.WriteLine($"{item.Name}");
                Console.WriteLine($"    {"ISBN",20}   {"Title",-50}");
                foreach (var book in item.Books)
                {
                    Console.WriteLine($"    {book.Isbn,20}   {book.Title.PadRight(100).Substring(0, 50),50}");
                    Console.WriteLine($"     {"Authors:",20}");
                    Console.WriteLine($"       {"Name",20}  {"Royalty Rate",20}");
                    // each book has wrotes related data
                    foreach (var wrote in book.Wrotes)
                    {
                        var author = wrote.AuthorLink;

                        Console.WriteLine($"       {author.Name.PadRight(20).Substring(0, 20),20}  {wrote.RoyaltyRate}");
                        // each wrote has an associated author
                    }
                }
            }

        }
        [Test]
        public void GetPublisherBooks()
        {
            using var context = GetContext();

            var publishers = context.Publishers
                .Include(c => c.Books.Take(3))
                .ToList();

            foreach (var item in publishers)
            {
                Console.WriteLine($"{item.Name}");
                Console.WriteLine($"    {"ISBN",20}   {"Title",-50}");
                foreach (var book in item.Books)
                {
                    Console.WriteLine($"    {book.Isbn,20}   {book.Title.PadRight(100).Substring(0, 50),50}");
                }
            }
        }

        [Test]
        public void GetPublisherContainsK_FilterBooksContainingTh_Get3Books()
        {
            using var context = GetContext();

            var publishers = context.Publishers
                .Include(c => c.Books
                    .Where(d => d.Title.Contains("th"))
                    .Take(3)
                    .OrderBy(e => e.Isbn))
                .Where(c => c.Name.Contains("k"))
                .OrderByDescending(c => c.Name)
                .ToList();

            foreach (var item in publishers)
            {
                Console.WriteLine($"{item.Name}");
                Console.WriteLine($"    {"ISBN",20}   {"Title",-50}");
                foreach (var book in item.Books)
                {
                    Console.WriteLine($"    {book.Isbn,20}   {book.Title.PadRight(100).Substring(0, 50),50}");
                }
            }
        }


        [Test]
        public void QueryALlPublishers()
        {
            using var context = GetContext();

            IQueryable<Publisher> publishersFromDb = context.Publishers;
            List<Publisher> publishersInMemory = publishersFromDb.ToList();

            Console.WriteLine($"{"Publisher ID",12}   {"Name",-50}");
            foreach (var item in publishersInMemory)
            {
                Console.WriteLine($"{item.PublisherId.ToString().PadRight(12).Substring(0, 12),-12}   " +
                                  $"{item.Name.PadRight(50).Substring(0, 50),-50}");
            }
        }

        [Test]
        public void GetPublishers_NameStartsWithB()
        {
            using var context = GetContext();

            var startsWithB = context.Publishers
                .Where(c => c.Name.StartsWith("B"))
                .OrderBy(c => c.PublisherId)
                .ThenBy(c => c.Name)
                .ToList();

            Console.WriteLine($"{"Publisher ID",12}   {"Name",-50}");
            foreach (var item in startsWithB)
            {
                Console.WriteLine($"{item.PublisherId.ToString().PadRight(12).Substring(0, 12),-12}   " +
                                  $"{item.Name.PadRight(50).Substring(0, 50),-50}");
            }
        }

        [Test]
        public void GetPublishers_NameContainsB()
        {
            using var context = GetContext();

            var startsWithB = context.Publishers
                .Where(c => c.Name.Contains("B"))
                .OrderBy(c => c.PublisherId)
                .ThenBy(c => c.Name)
                .ToList();

            Console.WriteLine($"{"Publisher ID",12}   {"Name",-50}");
            foreach (var item in startsWithB)
            {
                Console.WriteLine($"{item.PublisherId.ToString().PadRight(12).Substring(0, 12),-12}   " +
                                  $"{item.Name.PadRight(50).Substring(0, 50),-50}");
            }
        }

        [Test]
        public void CountPublishers()
        {
            using var context = GetContext();

            var count = context.Publishers
                .Count();

            Console.WriteLine(count);
        }
    }
}
