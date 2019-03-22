using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Laboratory04.Tools;
using Laboratory04.Tools.DataStorage;
using Laboratory04.Tools.Manager;
using Laboratory04.Tools.Navigation;

namespace Laboratory04.ViewModel
{
    internal class FilterListViewModel : BasicViewModel
    {
        #region Fields

        private string _name;
        private string _surname;
        private DateTime? _birthdayFrom;
        private DateTime? _birthdayTo;
        private string _email;
        private string _chineseSign;
        private string _sunSign;
        private bool _isAdult;
        private bool _notAdult;
        private bool _isBirthday;
        private bool _notBirthday;


        #endregion

        #region Commands

        private ICommand _filterCommand;
        private ICommand _cancelCommand;

        #endregion

        #region Properties

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                OnPropertyChanged();
            }
        }

        public DateTime? BirthdayFrom
        {
            get { return _birthdayFrom; }
            set
            {
                if (value == null)
                    _birthdayFrom = new DateTime(1800, 1, 1);
                else
                    _birthdayFrom = value;

                OnPropertyChanged();
            }
        }

        public DateTime? BirthdayTo
        {
            get { return _birthdayTo; }
            set
            {
                if (value == null)
                    _birthdayTo = DateTime.Today;
                else
                    _birthdayTo = value;

                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public string ChineseSign
        {
            get { return _chineseSign; }
            set
            {
                _chineseSign = value;
                OnPropertyChanged();
            }
        }

        public bool IsAdult
        {
            get { return _isAdult; }
            set
            {
                _isAdult = value;
                OnPropertyChanged();
            }
        }

        public bool NotAdult
        {
            get { return _notAdult; }
            set
            {
                _notAdult = value;
                OnPropertyChanged();
            }
        }

        public bool IsBirthday
        {
            get { return _isBirthday; }
            set
            {
                _isBirthday = value;
                OnPropertyChanged();
            }
        }

        public bool NotBirthday
        {
            get { return _notBirthday; }
            set
            {
                _notBirthday = value;
                OnPropertyChanged();
            }
        }

        public string SunSign
        {
            get { return _sunSign; }
            set
            {
                _sunSign = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        public ICommand FilterCommand
        {
            get
            {
                return _filterCommand ?? (_filterCommand =
                           new RelayCommand<object>(FilterImplementation));
            }
        }


        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new RelayCommand<object>(Cancel));
            }
        }

        #endregion

       private async void FilterImplementation(object obj)
       {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                StationManager.DataStorage.FilterList(Name, Surname, Email, BirthdayFrom, BirthdayTo, ChineseSign, SunSign, IsAdult, NotAdult, IsBirthday, NotBirthday);
            });

            LoaderManager.Instance.HideLoader();
            NavigationManager.Instance.Navigate(ViewType.MainPage);
       }

        public override void Refresh()
        {
            Name = "";
            Surname = "";
            BirthdayFrom = null;
            BirthdayTo = null;
            Email = "";
            ChineseSign = "";
            SunSign = "";
            IsAdult = true;
            IsBirthday = true;
            NotAdult = true;
            NotBirthday = true;

            StationManager.DataStorage = new SerializedDataStorage();
        }

        private void Cancel(object obj)
        {
            StationManager.DataStorage = new SerializedDataStorage();
            NavigationManager.Instance.Navigate(ViewType.MainPage);
        }

    }
}
