using System.Diagnostics;

namespace P42.Uno.Controls.AnimateBar;

public partial class Down : Base
{
    // ReSharper disable once MemberCanBeProtected.Global
    public Down()
    {
        Width = 30;
        Margin = new Thickness(5, 0, 5, 0);
        VerticalAlignment = VerticalAlignment.Top;

        Dir = 1;
        Height = 11;
        //StaticRect.Height = 
        DynamicRect.Height = 1;
        // StaticRect.VerticalAlignment = 
        DynamicRect.VerticalAlignment = VerticalAlignment.Top;
        //StaticRect.HorizontalAlignment = 
        DynamicRect.HorizontalAlignment = HorizontalAlignment.Stretch;
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
                    DynamicRect.RenderTransform = new TranslateTransform { Y = dir * (ActualHeight - 1) * x };
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
