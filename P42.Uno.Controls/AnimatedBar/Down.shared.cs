using P42.Utils.Uno;
using System;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Media;

namespace P42.Uno.Controls.AnimateBar
{
    public partial class Down : Base
    {
        public Down()
        {
            Width = 30;
            Margin = new Microsoft.UI.Xaml.Thickness(5, 0, 5, 0);
            VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Top;

            dir = 1;
            Height = 11;
            //StaticRect.Height = 
            DynamicRect.Height = 1;
            // StaticRect.VerticalAlignment = 
            DynamicRect.VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Top;
            //StaticRect.HorizontalAlignment = 
            DynamicRect.HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Stretch;
        }

        protected override async Task Loop(int dir)
        {
            do
            {
                var animator = new NormalizedActionAnimator(ActionTime, x =>
                {
                    try
                    {
                        if (_failed || _disposed || !DynamicRect.IsLoaded) return;

                        DynamicRect.Opacity = 1 - x;
                        DynamicRect.RenderTransform = new TranslateTransform { Y = dir * (ActualHeight - 1) * x };
                    }
                    catch (Exception ex)
                    {
                        _failed = true;
                        System.Diagnostics.Debug.WriteLine(ex.ToString());
                    }

                }); //, new Microsoft.UI.Xaml.Media.Animation.ExponentialEase());
                await animator.RunAsync();
                await Task.Delay(LullTime);
            } while (!_disposed);
        }

    }
}
