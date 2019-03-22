using Laboratory04.Tools;
using Laboratory04.ViewModel;
using Laboratory04.Tools.Navigation;

namespace Laboratory04.View
{
    /// <summary>
    /// Interaction logic for LoginPageControl.xaml
    /// </summary>
    public partial class CreatePersonView : INavigatable
    {
        private readonly BasicViewModel _createPerson;

        public CreatePersonView()
        {
            InitializeComponent();
            _createPerson = new CreatePersonViewModel();
            DataContext = _createPerson;
        }

        INavigatable INavigatable.Refresh()
        {
            _createPerson.Refresh();
            return this;
        }
    }
}