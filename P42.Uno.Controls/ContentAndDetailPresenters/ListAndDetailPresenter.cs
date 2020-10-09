using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace P42.Uno.Controls
{
    [TemplatePart(Name = ListViewName, Type = typeof(ListView))]
    public partial class ListAndDetailPresenter : ContentAndDetailPresenter, IDisposable
    {
        #region Properties
        #endregion


        #region Fields
        const string ListViewName = "_listView";
        ListView _listView;
        #endregion


        #region Construction / Initialization / Disposal
        public ListAndDetailPresenter()
        {
            DefaultStyleKey = typeof(ListAndDetailPresenter);
        }


        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _listView = (ListView)GetTemplateChild(ListViewName);
            _listView.ContainerContentChanging += OnListViewContainerContentChanging;
            _listView.ChoosingItemContainer += OnListViewChoosingItemContainer;
        }

        private void OnListViewContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.ItemContainer?.ContentTemplateRoot is IListAndDetailDataTemplate listAndDetailDataTemplate)
                listAndDetailDataTemplate.ListAndDetailPresenter = this;
        }

        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _disposed = true;
                _listView.ContainerContentChanging -= OnListViewContainerContentChanging;
                _listView.ChoosingItemContainer -= OnListViewChoosingItemContainer;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion


        #region Cell Matching
        protected virtual void OnListViewChoosingItemContainer(ListViewBase sender, ChoosingItemContainerEventArgs args)
        {
        }


        #endregion
    }
}