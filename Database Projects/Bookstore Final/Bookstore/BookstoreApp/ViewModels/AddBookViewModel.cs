using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BookstoreApp.Annotations;
using BookstoreApp.Dto;
using BookStoreDb;
using BookStoreDb.Core;
using FluentValidation;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookstoreApp.ViewModels
{
    public class AddBookViewModel : INotifyPropertyChanged
    {
        private string _bookTitleInput;
        private string _bookIsbnInput;
        private string _bookPriceInput;
        private DateTime _datePublishedInput;
        private string _bookPagesInput;
        private string _publisherSearchText;
        private string _authorSearchText;
        protected BookStoreLiteContext _context;
        private PublisherName _selectedPublisher;
        private BookAuthor _selectedAuthor;
        private BookAuthor _selectedAuthorOnList;

        public ObservableCollection<BookAuthor> Authors { get; set; } = new();
        public ObservableCollection<PublisherName> Publishers { get; set; } = new();
        public ObservableCollection<BookAuthor> AuthorList { get; set; } = new();

        public NewBookDto NewBookDto { get; set; } = new();

        public bool CanAdd
        {
            get 
            { 
                return ErrorsInText.Length == 0; 
            }
        }


        public string ErrorsInText { get; set; } = "";

        public PublisherName SelectedPublisher
        {
            get => _selectedPublisher;
            set
            {
                _selectedPublisher = value;
                NewBookDto.Publisher = value;
                OnPropertyChanged();
                Check();
            }
        }

        public BookAuthor SelectedAuthorOnList
        {
            get => _selectedAuthorOnList;
            set
            {
                _selectedAuthorOnList = value;
                OnPropertyChanged();
                Check();
            }
        }
        public BookAuthor SelectedAuthor
        {
            get => _selectedAuthor;
            set
            {
                _selectedAuthor = value;
                OnPropertyChanged();
                Check();
            }
        }

        public string BookTitleInput
        {
            get
            {
                return _bookTitleInput;
            }
            set
            {
                _bookTitleInput = value;
                NewBookDto.Title = value;
                OnPropertyChanged();
                Check();
            }
        }

        public string BookIsbnInput
        {
            get => _bookIsbnInput;
            set
            {
                _bookIsbnInput = value;
                NewBookDto.ISBN = _bookIsbnInput;
                OnPropertyChanged();
                Check();
            }
        }

        public string BookPriceInput
        {
            get => _bookPriceInput;
            set
            {
                _bookPriceInput = value;
                OnPropertyChanged();
                Check();
                NewBookDto.Price = float.Parse(value);
            }
        }

        public DateTime DatePublishedInput
        {
            get => _datePublishedInput;
            set
            {
                _datePublishedInput = value;
                NewBookDto.DatePublished = value;
                OnPropertyChanged();
                Check();
            }
        }

        public string BookPagesInput
        {
            get => _bookPagesInput;
            set
            {
                _bookPagesInput = value;
                OnPropertyChanged();
                Check();
                NewBookDto.Pages = int.Parse(_bookPagesInput, NumberStyles.AllowThousands);
            }
        }

        public string PublisherSearchText
        {
            get => _publisherSearchText;
            set
            {
                _publisherSearchText = value;
                FilterPublishers();
            }
        }

        public string AuthorSearchText
        {
            get => _authorSearchText;
            set
            {
                _authorSearchText = value; 

                FilterAuthors();
            }
        }

        public AddBookViewModel(BookStoreLiteContext context)
        {
            _context = context;
        }

        public void AddAuthorToList()
        {
            if (_selectedAuthor == null) return;
            var exists = AuthorList.FirstOrDefault(c => c.AuthorId == SelectedAuthor.AuthorId);
            if (exists != null) return;
            AuthorList.Add(_selectedAuthor); 
            NewBookDto.Authors.Add(_selectedAuthor);
            Check();
        }
        public void RemoveAuthorFromList()
        {
            if (_selectedAuthorOnList == null) return;

            AuthorList.Remove(_selectedAuthorOnList);
            NewBookDto.Authors.Add(_selectedAuthorOnList);
            Check();
        }
        public void IncreaseHierarchy()
        {
            if (_selectedAuthorOnList == null) return;

            var index = AuthorList.IndexOf(_selectedAuthorOnList);

            if (index == 0) return;
            
            AuthorList.Move(index, index - 1);
        }
        public void DecreaseHierarchy()
        {
            if (_selectedAuthorOnList == null) return;

            var index = AuthorList.IndexOf(_selectedAuthorOnList);

            if (index == AuthorList.Count-1) return;
            AuthorList.Move(index, index +1);
        }

        public void LoadPublishers()
        {
            FilterPublishers();
        }

        private void FilterPublishers()
        {
            string search = PublisherSearchText?.ToLowerInvariant() ?? string.Empty;

            var query = _context.Publishers
                .Where(c => c.Name.ToLower().Contains(search));

            var books = query
                .OrderBy(c => c.Name)
                .Select(c => new PublisherName(c.PublisherId, c.Name))
                .ToList();
            
            Publishers.Clear();

            foreach (var item in books) Publishers.Add(item);
        }

        public void LoadAuthors()
        {
            FilterAuthors();
        }

        private void FilterAuthors()
        {
            string search = AuthorSearchText?.ToLowerInvariant() ?? string.Empty;

            var query = _context.Authors
                .Include(c=>c.Wrotes.Take(1))
                .Where(c => c.Name.ToLower().Contains(search));
                
            
            var books = query
                .OrderBy(c => c.Name)
                .Select(c => new BookAuthor(c))
                .ToList();

            Authors.Clear();

            foreach (var item in books) Authors.Add(item);
        }



        /*
        public void Check()
        {
            NewBookDto.Check();

            ErrorsInText = NewBookDto.ErrorsInText;

            OnPropertyChanged(nameof(CanAdd));
            OnPropertyChanged(nameof(ErrorsInText));
        }
        */

        public void Check()
        {
            var validator = new NewBookDtoValidator();

            var result = validator.Validate(this);

            var sb = new StringBuilder();

            foreach (var error in result.Errors)
            {
                sb.AppendLine(error.ErrorMessage);
            }

            ErrorsInText = sb.ToString();

            OnPropertyChanged(nameof(CanAdd));
            OnPropertyChanged(nameof(ErrorsInText));
        }


        public virtual void SaveBook()
        {
            var book = new Book()
            {
                Title = _bookTitleInput,
                Isbn = _bookIsbnInput,
                NumPages = int.Parse(_bookPagesInput),
                DatePublished = _datePublishedInput,
                PublisherId = SelectedPublisher.PublisherId
            };
            book.Price = float.Parse(_bookPriceInput);

            book.Wrotes = new List<Wrote>();

            foreach (var author in AuthorList)
            {
                book.Wrotes.Add(new Wrote
                {
                    BookId = book.BookId,
                    AuthorId = author.AuthorId,
                    RoyaltyRate = 50
                }
                ) ;
            }

            _context.Add(book);
            try
            {
                _context.SaveChanges();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.InnerException.Message);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public class NewBookDtoValidator : AbstractValidator<AddBookViewModel>
        {
            public NewBookDtoValidator()
            {
                RuleFor(c => c.BookTitleInput).NotEmpty()
                    .WithMessage("Title is required.");
                RuleFor(c => c.BookIsbnInput).NotEmpty();
                RuleFor(c => c.BookPriceInput)
                    .Must(d =>
                    {
                        var canParse = float.TryParse(d, out float result);

                        return canParse && result>0;
                    }).WithMessage("Invalid price format and must be greater than 0");
                RuleFor(c => c.DatePublishedInput).Must(d => d.Date <= DateTime.Now);
                RuleFor(c => c.SelectedPublisher).NotNull();
                RuleFor(c => c.BookPagesInput).Must(d =>
                {
                    var canParse = int.TryParse(d, NumberStyles.AllowThousands , new CultureInfo(""), out int result);
                    
                    return canParse && result > 0;
                }).WithMessage("Pages input must be an integer and greater than 0");

                RuleForEach(c => c.Authors).NotNull().WithMessage("Error author at {CollectionIndex}")
                    .SetValidator(new BookAuthorValidator())
                    ;
                RuleFor(c => c.Authors).NotEmpty();
            }
        }

        public class BookAuthorValidator : AbstractValidator<BookAuthor>
        {
            public BookAuthorValidator()
            {
                RuleFor(c => c.AuthorId).NotEmpty();
                RuleFor(c => c.RoyaltyRate).GreaterThan(0);
            }
        }
    }
}
