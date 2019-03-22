using System.Windows.Controls;
using Laboratory04.Tools;
using Laboratory04.Tools.DataStorage;
using Laboratory04.Tools.Manager;
using Laboratory04.Tools.Navigation;
using Laboratory04.ViewModel;

namespace Laboratory04.View
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPageView : UserControl, INavigatable
    {
        private readonly BasicViewModel _mainPage;
        public MainPageView()
        {
            InitializeComponent();
            _mainPage = new MainPageViewModel();
            DataContext = _mainPage;
        }

        INavigatable INavigatable.Refresh()
        {
            _mainPage.Refresh();
            return this;
        }
    }
}
