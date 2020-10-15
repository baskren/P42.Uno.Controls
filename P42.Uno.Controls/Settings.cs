using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace P42.Uno.Controls
{
    public static class Settings
    {
        public static List<Assembly> AssembliesToInclude => P42.Utils.Uno.Settings.AssembliesToInclude;

        public static void Init(Windows.UI.Xaml.Application application)
        {
            P42.Utils.Uno.Settings.Init(application);
            AssembliesToInclude.Add(typeof(Settings).Assembly);
        }
    }
}
