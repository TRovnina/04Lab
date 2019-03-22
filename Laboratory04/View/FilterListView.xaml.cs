using Laboratory04.Tools;
using Laboratory04.Tools.Navigation;
using Laboratory04.ViewModel;

namespace Laboratory04.View
{
    /// <summary>
    /// Interaction logic for FilterList.xaml
    /// </summary>
    public partial class FilterListView : INavigatable
    {
        private readonly BasicViewModel _filter;

        public FilterListView()
        {
            InitializeComponent();
            _filter = new FilterListViewModel();
            DataContext = _filter;
        }

        INavigatable INavigatable.Refresh()
        {
            _filter.Refresh();
            return this;
        }
    }
}
