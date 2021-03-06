using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace P42.Uno.Controls
{
    internal partial class LoopingFlipViewItem : Grid
    {
        internal UIElement Child;

        AnimateBar.Left LeftBar = new AnimateBar.Left();

        AnimateBar.Right RightBar = new AnimateBar.Right();

        public LoopingFlipViewItem(UIElement child)
        {
            Child = child;
            Children.Add(child);
            Children.Add(RightBar);
            Children.Add(LeftBar);

            RightBar.Tapped += OnBarTapped;
            LeftBar.Tapped += OnBarTapped;
        }



        private void OnBarTapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (Parent is Grid _grid && _grid.Parent is LoopingFlipView flipView)
            {
                if (sender == LeftBar)
                    flipView.SelectedIndex--;
                else if (sender == RightBar)
                    flipView.SelectedIndex++;
            }
        }
    }
}
