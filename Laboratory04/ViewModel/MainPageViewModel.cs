using System.Collections.ObjectModel;
using Laboratory04.Tools;
using System.Windows.Input;
using Laboratory04.Models;
using Laboratory04.Tools.Manager;
using Laboratory04.Tools.Navigation;

namespace Laboratory04.ViewModel
{
    internal class MainPageViewModel: BasicViewModel
    {
        private ObservableCollection<Person> _people;
        private Person _selectedPerson;

        private ICommand _sortCommand;
        private ICommand _filterCommand;
        private ICommand _deleteCommand;
        private ICommand _addCommand;
        private ICommand _editCommand;
        private ICommand _saveCommand;

        public ObservableCollection<Person> People
        {
            get { return _people; }
            set
            {
                _people = value;
                OnPropertyChanged();
            }
        }

        public Person SelectedPerson
        {
            get { return _selectedPerson; }

            set
            {
                _selectedPerson = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new RelayCommand<object>(SaveImplementation)); }
        }

        private void SaveImplementation(object obj)
        {
            StationManager.DataStorage.SaveChanges();
        }

        public ICommand FilterCommand
        {
            get { return _filterCommand ?? (_filterCommand = new RelayCommand<object>(FilterImplementation)); }
        }

        private void FilterImplementation(object obj)
        {
           NavigationManager.Instance.Navigate(ViewType.Filter);
        }


        public ICommand SortCommand
        {
            get { return _sortCommand ?? (_sortCommand = new RelayCommand<object>(SortImplementation)); }
        }
        
        private void SortImplementation(object obj)
        {
            StationManager.DataStorage.SortList(obj.ToString());
            Refresh();
        }

        public ICommand AddCommand
        {
            get { return _addCommand ?? (_addCommand = new RelayCommand<object>(AddImplementation)); }
        }

        private void AddImplementation(object obj)
        {
            NavigationManager.Instance.Navigate(ViewType.Create);
        }

        public ICommand DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new RelayCommand<object>(DeleteImplementation)); }
        }

        private void DeleteImplementation(object obj)
        {
           StationManager.DataStorage.DeletePerson(SelectedPerson);
           Refresh();
        }

        public ICommand EditCommand
        {
            get { return _editCommand ?? (_editCommand = new RelayCommand<object>(EditImplementation)); }
        }

        private void EditImplementation(object obj)
        {
            StationManager.CurrentPerson = SelectedPerson;
            NavigationManager.Instance.Navigate(ViewType.Create);
            Refresh();
        }

        internal MainPageViewModel()
        {
            _people = new ObservableCollection<Person>(StationManager.DataStorage.PeopleList);
        }

        public override void Refresh()
        {
            People = new ObservableCollection<Person>(StationManager.DataStorage.PeopleList);
        }
        
    }
}
