using Microsoft.UI.Xaml.Media.Animation;

namespace P42.Uno.Controls;

internal static class AnimationExtensions
{
    public static Task BeginAsync(this Storyboard storyboard)
    {
        var taskSource = new TaskCompletionSource<object>();
        EventHandler<object> completed = null;
        completed += (s, e) =>
        {
            storyboard.Completed -= completed;
            taskSource.SetResult(null);
        };

        storyboard.Completed += completed;
        storyboard.Begin();

        return taskSource.Task;
    }

}
