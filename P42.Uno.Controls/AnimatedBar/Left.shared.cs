using P42.Utils.Uno;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace P42.Uno.Controls.AnimateBar
{
    public partial class Left : Base
    {
        public Left()
        {
            Width = 11;
            dir = 1;
            //StaticRect.Width =
            DynamicRect.Width = 1;
            //StaticRect.VerticalAlignment = 
            DynamicRect.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Stretch;
            //StaticRect.HorizontalAlignment = 
            DynamicRect.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
        }

        protected override async Task Loop(int dir)
        {
            do
            {
                var animator = new NormalizedActionAnimator(ActionTime, x =>
                {
                    if (!_disposed)
                    {
                        DynamicRect.Opacity = 1 - x;
                        DynamicRect.RenderTransform = new TranslateTransform { X = dir * (ActualWidth - 1) * x };
                    }

                }); //, new Windows.UI.Xaml.Media.Animation.ExponentialEase());
                await animator.RunAsync();
                await Task.Delay(LullTime);
            } while (!_disposed);
        }

    }
}
