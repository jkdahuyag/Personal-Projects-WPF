using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using BookstoreApp.Dto;
using BookstoreApp.Helpers;
using BookStoreDb;
using BookStoreDb.Core;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftAntimalwareAMFilter;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApp.ViewModels;

public class BookListViewModel : INotifyPropertyChanged
{
    private readonly BookStoreLiteContext _context;

    #region Properties
    public ObservableCollection<BookTitle> Books { get; } = new();

    public BookTitle SelectedBook
    {
        get => _selectedBook;
        set
        {
            _selectedBook = value;
            OnPropertyChanged();
            LoadBookDetails();
        }
    }

    public Pagination PageDetails { get; private set; }

    public BookDetails SelectedBookDetails { get; set; }

    public string SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            OnPropertyChanged();

            FilterBooks();
        }
    }

    public BookListViewModel(BookStoreLiteContext context) : this()
    {
        _context = context;
    }
    #endregion

    public void CreateNewBook()
    {
        var newBook = new AddBookViewModel(_context);

        var window = new AddBook();

        window.Owner = Application.Current.MainWindow;

        window.DataContext = newBook;

        window.Show();
    }

    public void EditBook()
    {
        if (_selectedBook is null) return;

        var newBook = new EditBookViewModel(_context, SelectedBookDetails);

        var window = new EditBook();

        window.Owner = Application.Current.MainWindow;

        window.DataContext = newBook;

        window.Show();
    }


    #region INotifyPropertyChanged
    public event PropertyChangedEventHandler? PropertyChanged;
    #endregion


    #region Public Methods
    public void LoadBooks()
    {
        FilterBooks();
    }
    #endregion


    #region Protected Methods
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
    #endregion


    #region Private Methods
    private void LoadBookDetails()
    {
        if (_selectedBook == null) return;

        //using var context = new BookStoreLiteContext();

        int bookId = _selectedBook.BookId;

        var book = _context.Books
            .Include(c => c.PublisherLink)
            .Include(c => c.Wrotes)
            .ThenInclude(c => c.AuthorLink)
            .Single(c => c.BookId == bookId);

        SelectedBookDetails = new BookDetails(book);

        // Inform the UI that the data for SelectedBookDetails
        // have been updated.
        OnPropertyChanged(nameof(SelectedBookDetails));
    }

    private void FilterBooks()
    {
        //using var context = new BookStoreLiteContext();

        string search = SearchText?.ToLowerInvariant()?? string.Empty;

        var query = _context.Books
            .Include(c => c.PublisherLink)
            .Where(c => c.Title.ToLower().Contains(search) ||
                        c.Isbn.ToLower().Contains(search) ||
                        c.PublisherLink.Name.ToLower().Contains(search));

        int totalCount = query.Count();
        UpdateTotalPages(totalCount);

        var books = query
            .OrderBy(c => c.Title)
            .Select(c => new BookTitle(c.BookId, c.Title,c.PublisherLink.Name))
            .Skip(PageDetails.ItemsPerPage * (PageDetails.CurrentPage - 1))
            .Take(PageDetails.ItemsPerPage)
            .ToList();

        Books.Clear();

        foreach (var item in books) Books.Add(item);
    }

    private void UpdateTotalPages(int totalCount)
    {
        PageDetails.TotalPages = (int)Math.Ceiling((float)totalCount/PageDetails.ItemsPerPage);

        OnPropertyChanged(nameof(PageDetails));
    }
    #endregion


    #region Fields
    private Visibility _bookDetailsVisibility = Visibility.Collapsed;
    private string _searchText;
    private BookTitle _selectedBook;

    private BookListViewModel()
    {
        PageDetails = new Pagination(FilterBooks);
    }
    #endregion

    public void RemoveBook()
    {
        if (SelectedBook is null) return;

        var book = _context.Books.First(c => c.BookId == SelectedBook.BookId);

        book.Wrotes.Clear();
        try
        {
            _context.Remove(book);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            MessageBox.Show(e.InnerException.Message);
        }


        Books.Remove(SelectedBook);
        SelectedBook = null;
        SelectedBookDetails = null;
    }
}