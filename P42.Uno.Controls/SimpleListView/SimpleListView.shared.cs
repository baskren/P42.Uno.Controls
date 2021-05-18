using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace P42.Uno.Controls
{
    public partial class SimpleListView : ContentControl
    {

        #region Properties

        #region ListViewBase Properties

        #region IsItemClickEnabled Property
        public static readonly DependencyProperty IsItemClickEnabledProperty = DependencyProperty.Register(
            nameof(IsItemClickEnabled),
            typeof(bool),
            typeof(SimpleListView),
            new PropertyMetadata(default(bool))
        );
        public bool IsItemClickEnabled
        {
            get => (bool)GetValue(IsItemClickEnabledProperty);
            set => SetValue(IsItemClickEnabledProperty, value);
        }
        #endregion IsItemClickEnabled Property

        #region SelectedItems Property
        public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.Register(
            nameof(SelectedItems),
            typeof(IList<object>),
            typeof(SimpleListView),
            new PropertyMetadata(default(IList<object>))
        );
        public IList<object> SelectedItems
        {
            get => (IList<object>)GetValue(SelectedItemsProperty);
            private set => SetValue(SelectedItemsProperty, value);
        }
        #endregion SelectedItems Property

        #region SelectionMode Property
        public static readonly DependencyProperty SelectionModeProperty = DependencyProperty.Register(
            nameof(SelectionMode),
            typeof(ListViewSelectionMode),
            typeof(SimpleListView),
            new PropertyMetadata(default(ListViewSelectionMode))
        );
        public ListViewSelectionMode SelectionMode
        {
            get => (ListViewSelectionMode)GetValue(SelectionModeProperty);
            set => SetValue(SelectionModeProperty, value);
        }
        #endregion SelectionMode Property

        #region Seletor Properties

        #region SelectedIndex Property
        public static readonly DependencyProperty SelectedIndexProperty = DependencyProperty.Register(
            nameof(SelectedIndex),
            typeof(int),
            typeof(SimpleListView),
            new PropertyMetadata(-1)
        );
        public int SelectedIndex
        {
            get => (int)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }
        #endregion SelectedIndex Property

        #region SelectedItem Property
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            nameof(SelectedItem),
            typeof(object),
            typeof(SimpleListView),
            new PropertyMetadata(default(object))
        );
        public object SelectedItem
        {
            get => (object)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }
        #endregion SelectedItem Property

        #region ItemsControl Properties


        #region ItemsSource Property
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            nameof(ItemsSource),
            typeof(IEnumerable),
            typeof(SimpleListView),
            new PropertyMetadata(default(IEnumerable), OnItemsSourceChanged)
        );

        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }
        #endregion ItemsSource Property

        #region ItemTemplate Property
        public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register(
            nameof(ItemTemplate),
            typeof(DataTemplate),
            typeof(SimpleListView),
            new PropertyMetadata(default(DataTemplate), OnItemTemplateChanged)
        );

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }
        #endregion ItemTemplate Property

        #region ItemTemplateSelector Property
        public static readonly DependencyProperty ItemTemplateSelectorProperty = DependencyProperty.Register(
            nameof(ItemTemplateSelector),
            typeof(DataTemplateSelector),
            typeof(SimpleListView),
            new PropertyMetadata(default(DataTemplateSelector), OnItemTemplateSelectorChanged)
        );

        public DataTemplateSelector ItemTemplateSelector
        {
            get => (DataTemplateSelector)GetValue(ItemTemplateSelectorProperty);
            set => SetValue(ItemTemplateSelectorProperty, value);
        }
        #endregion ItemTemplateSelector Property


        #endregion

        #endregion Selector Properties

        #endregion ListViewBase Properties

        #endregion



        #region Events

        #region ListViewBase Events

        public event ItemClickEventHandler ItemClick;

        #region Selector Events

        public event SelectionChangedEventHandler SelectionChanged;

        #endregion

        #endregion

        #endregion


        #region Constructors
        public SimpleListView()
            => PlatformBuild();
        #endregion


        #region Methods


        #endregion
    }
}
