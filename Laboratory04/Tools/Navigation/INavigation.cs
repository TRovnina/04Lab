namespace Laboratory04.Tools.Navigation
{
    internal enum ViewType
    {
        Create,
        MainPage,
        Filter
    }

    interface INavigation
    {
        void Navigate(ViewType viewType);
    }
}
