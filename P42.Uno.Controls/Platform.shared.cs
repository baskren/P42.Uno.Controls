using System.Collections.Generic;
using System.Reflection;

namespace P42.Uno.Controls
{
    public static class Platform
    {
        public static List<Assembly> AssembliesToInclude => P42.Utils.Uno.Platform.AssembliesToInclude;

        public static void Init(Microsoft.UI.Xaml.Application application, Microsoft.UI.Xaml.Window window)
        {
            P42.Utils.Uno.Platform.Init(application, window);
            AssembliesToInclude.Add(typeof(Platform).Assembly);
        }
    }
}
