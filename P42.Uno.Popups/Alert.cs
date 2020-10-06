using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace P42.Uno.Popups
{
    public partial class Alert : Toast
    {
        #region Properties
        #region OkText Property
        public static readonly DependencyProperty OkButtonTextProperty = DependencyProperty.Register(
            nameof(OkButtonText),
            typeof(string),
            typeof(Alert),
            new PropertyMetadata(default(string))
        );
        public string OkButtonText
        {
            get => (string)GetValue(OkButtonTextProperty);
            set => SetValue(OkButtonTextProperty, value);
        }
        #endregion OkText Property

        #region OkButtonForeground Property
        public static readonly DependencyProperty OkButtonForegroundProperty = DependencyProperty.Register(
            nameof(OkButtonForeground),
            typeof(Brush),
            typeof(Alert),
            new PropertyMetadata(default(Brush))
        );
        public Brush OkButtonForeground
        {
            get => (Brush)GetValue(OkButtonForegroundProperty);
            set => SetValue(OkButtonForegroundProperty, value);
        }
        #endregion OkButtonForeground Property


        #region OkButtonBackground Property
        public static readonly DependencyProperty OkButtonBackgroundProperty = DependencyProperty.Register(
            nameof(OkButtonBackground),
            typeof(Brush),
            typeof(Alert),
            new PropertyMetadata(default(Brush))
        );
        public Brush OkButtonBackground
        {
            get => (Brush)GetValue(OkButtonBackgroundProperty);
            set => SetValue(OkButtonBackgroundProperty, value);
        }
        #endregion OkButtonBackgroundBrush Property


        #endregion

        public Alert() : base()
        {
            this.DefaultStyleKey = typeof(Alert);

            /*
            var style = Style;
            var template = Application.Current.Resources["AlertPopupTemplate"] as ControlTemplate;
            System.Diagnostics.Debug.WriteLine(GetType() + ".Alert ctr template:" + template + "  style:"+style);
            Template = template;
            */
        }
    }
}
