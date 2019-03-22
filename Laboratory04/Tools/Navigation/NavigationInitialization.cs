using System;
using Laboratory04.View;

namespace Laboratory04.Tools.Navigation
{
    internal class NavigationInitialization : NavigationModel
    {
        public NavigationInitialization(IContent content) : base(content)
        {

        }

        protected override void InitializeView(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Create:
                   ViewsDictionary.Add(viewType, new CreatePersonView());
                    break;
                case ViewType.MainPage:
                    ViewsDictionary.Add(viewType, new MainPageView());
                    break;
                case ViewType.Filter:
                    ViewsDictionary.Add(viewType, new FilterListView());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }
    }
}
