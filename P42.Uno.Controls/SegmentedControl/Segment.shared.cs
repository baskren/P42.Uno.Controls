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
