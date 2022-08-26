using System;
using Windows.Foundation;
using Microsoft.UI.Xaml;

namespace P42.Uno.Controls.Test.Wasm
{
    public class Program
    {
        private static App _app;

        static int Main(string[] args)
        {
            Microsoft.UI.Xaml.Application.Start(_ => _app = new App());

            return 0;
        }

    }
}
