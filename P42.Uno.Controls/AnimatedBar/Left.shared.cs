using System.Diagnostics;

namespace P42.Uno.Controls.AnimateBar;

public partial class Left : Base
{
    // ReSharper disable once MemberCanBeProtected.Global
    public Left()
    {
        //Height = 30;
        VerticalAlignment = VerticalAlignment.Stretch;
        Margin = new Thickness(0, 5, 0, 5);
        HorizontalAlignment = HorizontalAlignment.Left;

        Width = 11;
        Dir = 1;
        //StaticRect.Width =
        DynamicRect.Width = 1;
        //StaticRect.VerticalAlignment = 
        DynamicRect.VerticalAlignment = VerticalAlignment.Stretch;
        //StaticRect.HorizontalAlignment = 
        DynamicRect.HorizontalAlignment = HorizontalAlignment.Left;
    }

    protected override async Task Loop(int dir)
    {
        do
        {
            var animator = new NormalizedActionAnimator(ActionTime, x =>
            {
                try
                {
                    if (Failed || Disposed || !DynamicRect.IsLoaded) return;

                    DynamicRect.Opacity = 1 - x;
                    DynamicRect.RenderTransform = new TranslateTransform { X = dir * (ActualWidth - 1) * x };
                }
                catch (Exception ex)
                {
                    Failed = true;
                    Debug.WriteLine(ex.ToString());
                }

            }); //, new Microsoft.UI.Xaml.Media.Animation.ExponentialEase());
            await animator.RunAsync();
            await Task.Delay(LullTime);
        } while (!Disposed);
    }

}
