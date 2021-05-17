using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Animation;

namespace P42.Uno.Controls
{
    static class AnimationExtensions
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
}
