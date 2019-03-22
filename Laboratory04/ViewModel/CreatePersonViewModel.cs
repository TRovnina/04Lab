using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Laboratory04.Exceptions;
using Laboratory04.Models;
using Laboratory04.Tools;
using Laboratory04.Tools.Manager;
using Laboratory04.Tools.Navigation;

namespace Laboratory04.ViewModel
{
   internal class CreatePersonViewModel: BasicViewModel
   {
        #region Fields

        private string _name;
        private string _surname;
        private DateTime? _birthday;
        private string _email;

        #endregion

        #region Commands

        private ICommand _createCommand;
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

        public DateTime? Birthday
        {
            get { return _birthday; }
            set
            {
               _birthday = value;
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

        #endregion

        #region Commands

        public ICommand CreateCommand
        {
            get
            {
                return _createCommand ?? (_createCommand =
                           new RelayCommand<object>(CreateImplementation, CanLogInExecute));
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

        private bool CanLogInExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(_name) && !string.IsNullOrWhiteSpace(_surname) &&
                   !string.IsNullOrWhiteSpace(_email) && _birthday != null;
        }

        private async void CreateImplementation(object obj)
        {
            var stop = false;
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                Thread.Sleep(1000);

                try
                {
                    var person = new Person(Name, Surname, Convert.ToDateTime(_birthday), Email);
                    StationManager.DataStorage.AddPerson(person);
                }
                catch (EmailException ex)
                {
                    MessageBox.Show(ex.Message);
                    stop = true;
                }
                catch (AgeException ex)
                {
                    MessageBox.Show(ex.Message);
                    stop = true;
                }
            });

            LoaderManager.Instance.HideLoader();

            if (!stop)
            {
                StationManager.DataStorage.DeletePerson(StationManager.CurrentPerson);
                StationManager.CurrentPerson = null;
                NavigationManager.Instance.Navigate(ViewType.MainPage);
            }
        }

        public override void Refresh()
        {
            if (StationManager.CurrentPerson != null)
            {
                Name = StationManager.CurrentPerson.Name;
                Surname = StationManager.CurrentPerson.Surname;
                Birthday = StationManager.CurrentPerson.Birthday;
                Email = StationManager.CurrentPerson.Email;
            }
            else
            {
                Name = "";
                Surname = "";
                Birthday = null;
                Email = "";
            }
        }

        private void Cancel(object obj)
        {
            StationManager.CurrentPerson = null;
            NavigationManager.Instance.Navigate(ViewType.MainPage);
        }

    }
}
