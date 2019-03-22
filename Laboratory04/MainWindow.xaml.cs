using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Laboratory04.Tools.DataStorage;
using Laboratory04.Tools.Manager;
using Laboratory04.Tools.Navigation;
using Laboratory04.ViewModel;

namespace Laboratory04
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IContent
    {
        public ContentControl ContentControl
        {
            get { return _contentControl; }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            InitializeApplication();
        }

        private void InitializeApplication()
        {
            StationManager.Initialize(new SerializedDataStorage());
            NavigationManager.Instance.Initialize(new NavigationInitialization(this));
            NavigationManager.Instance.Navigate(ViewType.MainPage);
        }
    }
}
