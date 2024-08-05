namespace App1;

    [Microsoft.UI.Xaml.Data.Bindable]
    public partial class SimplePage : Page
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
                        Foreground = new SolidColorBrush(Microsoft.UI.Colors.Red)
                    }
                }
            };
        }
    }

