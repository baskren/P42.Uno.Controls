using P42.Utils.Uno;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace P42.Uno.Controls.AnimateBar
{
    public partial class Down : Base
    {
        public Down()
        {
            dir = 1;
            Height = 11;
            //StaticRect.Height = 
            DynamicRect.Height = 1;
            // StaticRect.VerticalAlignment = 
            DynamicRect.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
            //StaticRect.HorizontalAlignment = 
            DynamicRect.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch;
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
                        DynamicRect.RenderTransform = new TranslateTransform { Y = dir * (ActualHeight - 1) * x };
                    }

                }); //, new Windows.UI.Xaml.Media.Animation.ExponentialEase());
                await animator.RunAsync();
                await Task.Delay(LullTime);
            } while (!_disposed);
        }

    }
}
