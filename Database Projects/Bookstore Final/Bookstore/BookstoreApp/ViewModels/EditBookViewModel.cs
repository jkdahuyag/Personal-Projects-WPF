using BookstoreApp.Dto;
using BookStoreDb;
using BookStoreDb.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookstoreApp.ViewModels
{
    public class EditBookViewModel : AddBookViewModel
    {
        private readonly BookDetails _book;

        public EditBookViewModel(BookStoreLiteContext context, BookDetails book) : base(context)
        {
            _book = book;
            BookTitleInput = book.Title;
            BookPagesInput = book.Pages;
            BookIsbnInput = book.Isbn;
            DatePublishedInput = DateTime.Parse(book.DatePublished);
            BookPriceInput = book.Price.Data;

            var publisher = new PublisherName(book.PublisherId, book.PublisherName);

            PublisherSearchText = "";
            AuthorSearchText = "";
            SelectedPublisher = publisher;
            Publishers.Insert(0, publisher);
            foreach (var bookAuthor in book.AuthorList)
            {
                AuthorList.Add(bookAuthor);
            }
        }

        public override void SaveBook()
        {
            // fetch the original book entity
            var book = _context.Books.First(c => c.BookId == _book.BookId);

            // update columns

            book.Title = BookTitleInput;
            book.Isbn = BookIsbnInput;
            book.NumPages = int.Parse(BookPagesInput, NumberStyles.AllowThousands);
            book.DatePublished = DatePublishedInput;
            book.PublisherId = SelectedPublisher.PublisherId;
            book.Price = book.NumPages * 10;

            // update the publisher (if changed)
            if (book.PublisherId != SelectedPublisher.PublisherId)
                book.PublisherId = SelectedPublisher.PublisherId;

            // update the wrotes

            // 1. clear the wrotes collection
            book.Wrotes.Clear();

            // 2. set up the foreign keys for the new authors

            foreach (var i in AuthorList)
            {
                book.Wrotes.Add(new Wrote
                {
                    BookId = book.BookId,
                    AuthorId = i.AuthorId,
                    RoyaltyRate = i.RoyaltyRate
                });
            }

            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.InnerException.Message);
            }

        }
    }
}
