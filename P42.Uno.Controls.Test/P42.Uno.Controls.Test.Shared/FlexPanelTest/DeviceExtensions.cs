using System;
using Windows.UI.Xaml;
#if __IOS__
using UIKit;
#endif

namespace FlexPanelTest
{
    public static class DeviceExtensions
    {

        public static Thickness GetSafeAreaInsets()
        {

#if __IOS__
            UIEdgeInsets sai;

			if (!UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
				sai = new UIEdgeInsets(UIApplication.SharedApplication.StatusBarFrame.Size.Height, 0, 0, 0);
			else if (UIApplication.SharedApplication.KeyWindow != null)
				sai = UIApplication.SharedApplication.KeyWindow.SafeAreaInsets;
			else if (UIApplication.SharedApplication.Windows.Length > 0)
				sai = UIApplication.SharedApplication.Windows[0].SafeAreaInsets;
			else
				sai = UIEdgeInsets.Zero;

            return new Thickness(sai.Left, sai.Top, sai.Right, sai.Bottom);

#else
            return new Thickness();
#endif
        }


    }
}
