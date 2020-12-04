using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using P42.Utils.Uno;

namespace P42.Uno.Controls
{
    public partial class Segment : ToggleButton
    {
        #region Properties
        /*
        #region Text Property
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(Segment),
            new PropertyMetadata(default(string))
        );
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        #endregion Text Property

        #region Icon Property
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            nameof(Icon),
            typeof(IconElement),
            typeof(Segment),
            new PropertyMetadata(default(IconElement))
        );
        public IconElement Icon
        {
            get => (IconElement)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }
        #endregion Icon Property
        */

        public int Index { get; internal set; } = -1;

        public bool IsSelected => IsChecked.HasValue && IsChecked.Value;
        #endregion


        #region Construction
        public Segment()
        {
            Click += OnClicked;
        }

        private void OnClicked(object sender, RoutedEventArgs e)
        {
            if (this.FindAncestor<SegmentedControl>() is SegmentedControl control)
                control.OnSegmentClicked(this);
        }
        #endregion


        #region Update Methods


        #endregion
    }
}
