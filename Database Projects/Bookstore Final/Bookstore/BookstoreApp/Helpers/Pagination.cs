using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BookstoreApp.Annotations;

namespace BookstoreApp.Helpers
{
    public class Pagination : NotifyDataError, INotifyPropertyChanged
    {
        private readonly Action _hasUpdate;
        private int _totalPages;
        private string _enteredPage = "1";
        private int _currentPage = 1;

        public Pagination(Action hasUpdate)
        {
            _hasUpdate = hasUpdate;
        }

        public int TotalPages
        {
            get => _totalPages;
            set
            {
                _totalPages = value; 
                OnPropertyChanged(nameof(TotalPagesFormatted));
                OnPropertyChanged(nameof(EnteredPage));

            }
        }

        public string TotalPagesFormatted
        {
            get { return $"of {TotalPages:N0}"; }
        }
        
        public ObservableCollection<string> InputErrors { get; set; } = new ObservableCollection<string>();

        public string ToolTipError
        {
            get
            {
                var sb = new StringBuilder();

                foreach (var inputError in InputErrors)
                {
                    sb.AppendLine(inputError);
                }
                return sb.ToString();
            }
        }

        public string EnteredPage
        {
            get => _enteredPage;
            set
            {
                _enteredPage = value;
                var successParse = int.TryParse(_enteredPage, out int result);
                if (successParse)
                {
                    _hasUpdate();
                    ClearErrors(nameof(EnteredPage));
                    InputErrors.Clear();
                    CurrentPage = result;
                }
                else
                {
                    ClearErrors(nameof(EnteredPage));
                    InputErrors.Clear();
                    SetErrors(nameof(EnteredPage), new List<string>
                    {
                        "Invalid number format"
                    });
                    InputErrors.Add("Invalid number format");
                    InputErrors.Add($"Value must be between 1 and {TotalPages}");
                }
                OnPropertyChanged(nameof(CurrentPage));
                OnPropertyChanged(nameof(EnteredPage));
                OnPropertyChanged(nameof(ToolTipError));
            }
        }

        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        public int ItemsPerPage { get; set; } = 20;

        public void NextPage()
        {
            if (CurrentPage == TotalPages) return;
            CurrentPage++;
            EnteredPage = CurrentPage.ToString();
            OnPropertyChanged(nameof(EnteredPage));
        }

        public void PrevPage()
        {
            if (CurrentPage == 1) return;
            CurrentPage--;
            EnteredPage = CurrentPage.ToString();

            OnPropertyChanged(nameof(EnteredPage));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

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

    }
}
