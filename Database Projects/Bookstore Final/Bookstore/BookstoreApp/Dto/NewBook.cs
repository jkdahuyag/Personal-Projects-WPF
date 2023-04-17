using BookstoreApp.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BookStoreDb.Core;
using FluentValidation;

namespace BookstoreApp.Dto
{
    public class NewBookDto : INotifyPropertyChanged
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public float Price { get; set; }
        public DateTime DatePublished { get; set; }
        public int Pages { get; set; }

        public PublisherName Publisher { get; set; }

        public List<BookAuthor> Authors { get; set; } = new();

        public string ErrorsInText { get; set; }

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

            OnPropertyChanged(nameof(ErrorsInText));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class BookAuthor
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public float RoyaltyRate { get; set; }

        public BookAuthor(Author author)
        {
            AuthorId = author.AuthorId;
            Name = author.Name;
            Address = author.Address;
            RoyaltyRate = 50;
        }

        public BookAuthor(Author author, float royaltyRate)
        {
            RoyaltyRate = royaltyRate;
        }
    }

    public class PublisherValidator : AbstractValidator<Publisher>
    {
        public PublisherValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
        }
    }


    public class NewBookDtoValidator : AbstractValidator<NewBookDto>
    {
        public NewBookDtoValidator()
        {
            RuleFor(c => c.Title).NotEmpty()
                .WithMessage("Title is required.");
            RuleFor(c => c.ISBN).NotEmpty();
            RuleFor(c => c.Price).GreaterThan(0);
            RuleFor(c => c.DatePublished).Must(d => d.Date <= DateTime.Now);
            RuleFor(c => c.Publisher).NotNull();
            RuleFor(c => c.Pages).GreaterThan(0);


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

