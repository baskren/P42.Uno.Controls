using AppKit;

namespace P42.Uno.Controls.Demo.WinUI
{
	// This is the main entry point of the application.
	public class EntryPoint
	{
		static void Main(string[] args)
		{
			NSApplication.Init();
			NSApplication.SharedApplication.Delegate = new AppHead();
			NSApplication.Main(args);
		}
	}
}

