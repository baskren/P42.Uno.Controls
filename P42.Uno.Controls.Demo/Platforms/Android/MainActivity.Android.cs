using Android.App;
using Android.Views;

namespace P42.Uno.Controls.Demo.Droid;

[Activity(
    MainLauncher = true,
    ConfigurationChanges = ActivityHelper.AllConfigChanges,
    WindowSoftInputMode = SoftInput.AdjustNothing | SoftInput.StateHidden
)]
public class MainActivity : ApplicationActivity
{
}
