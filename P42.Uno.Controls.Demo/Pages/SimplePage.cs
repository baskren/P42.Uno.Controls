namespace App1;

    [Bindable]
    public class SimplePage : Page
    {
        public SimplePage()
        {
            Content = new Grid
            {
                Children =
                { 
                    new TextBlock
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Text = "SIMPLE PAGE",
                        Foreground = new SolidColorBrush(Colors.Red)
                    }
                }
            };
        }
    }

