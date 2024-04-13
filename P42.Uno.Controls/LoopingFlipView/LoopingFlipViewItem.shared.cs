using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace P42.Uno.Controls
{
    [Microsoft.UI.Xaml.Data.Bindable]
    internal partial class LoopingFlipViewItem : Grid, IEventSubscriber
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



        private void OnBarTapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (Parent is Grid _grid && _grid.Parent is LoopingFlipView flipView)
            {
                if (sender is P42.Uno.Controls.AnimateBar.Base bar)
                {
                    if (bar == LeftBar)
                        flipView.SelectedIndex--;
                    else if (bar == RightBar)
                        flipView.SelectedIndex++;
                }
            }
        }

        public void EnableEvents()
        {
            if (Child is IEventSubscriber eventSubscriber)
                eventSubscriber.EnableEvents();
        }

        public void DisableEvents()
        {
            if (Child is IEventSubscriber eventSubscriber)
                eventSubscriber.DisableEvents();
        }

        public bool AreEventsEnabled
        {
            get
            {
                if (Child is IEventSubscriber subscriber)
                    return subscriber.AreEventsEnabled;
                return false;
            }
        }
    }
}
