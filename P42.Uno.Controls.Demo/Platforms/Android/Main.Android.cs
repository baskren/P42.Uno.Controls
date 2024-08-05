using Android.App;
using Android.Runtime;
using Com.Nostra13.Universalimageloader.Core;

namespace P42.Uno.Controls.Demo.Droid;

[ApplicationAttribute(
    Label = "@string/ApplicationName",
    Icon = "@mipmap/icon",
    LargeHeap = true,
    HardwareAccelerated = true,
    Theme = "@style/AppTheme"
)]
public class Application : NativeApplication
{
    static Application()
    {
        App.InitializeLogging();
    }

    public Application(IntPtr javaReference, JniHandleOwnership transfer)
        : base(() => new App(), javaReference, transfer)
    {
        ConfigureUniversalImageLoader();
    }

    private static void ConfigureUniversalImageLoader()
    {
        // Create global configuration and initialize ImageLoader with this config
        ImageLoaderConfiguration config = new ImageLoaderConfiguration.Builder(Context)
            .Build();

        ImageLoader.Instance.Init(config);

        ImageSource.DefaultImageLoader = ImageLoader.Instance.LoadImageAsync;
    }
}

