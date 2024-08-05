namespace P42.Uno.Controls.Demo;

public class Program
{
    private static App? _app;

    public static int Main(string[] args)
    {
        Application.Start(_ => _app = new App());

        return 0;
    }
}
