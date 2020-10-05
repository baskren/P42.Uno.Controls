using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace P42.Uno.Popups
{
    public partial class ToastStyle : TargetedPopup
    {
        public ToastStyle()
        {
            var style = Application.Current.Resources["TargetedToastPopupTemplate"] as Style;
            Style = style;
        }
    }
}
