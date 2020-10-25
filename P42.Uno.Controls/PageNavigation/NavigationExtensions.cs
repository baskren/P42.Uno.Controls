using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace P42.Uno.Controls
{
    public static class NavigationExtensions
    {
        #region Attached Properties 

        #region Title
        #region PageTitle Property
        public static readonly DependencyProperty PageTitleProperty = DependencyProperty.RegisterAttached(
            "PageTitle",
            typeof(string),
            typeof(NavigationExtensions),
            new PropertyMetadata(default(string), OnPageTitleChanged)
        );
        static void OnPageTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Page page)
            {
                //throw new NotImplementedException();
            }
        }
        public static string GetPageTitle(Page element)
            => (string)element.GetValue(PageTitleProperty);
        public static void SetPageTitle(Page element, string value)
            => element.SetValue(PageTitleProperty, value);
        #endregion PageTitle Property


        #region PageTitleBrush Property
        public static readonly DependencyProperty PageTitleBrushProperty = DependencyProperty.RegisterAttached(
            "PageTitleBrush",
            typeof(Brush),
            typeof(NavigationExtensions),
            new PropertyMetadata(default(Brush), OnPageTitleBrushChanged)
        );
        static void OnPageTitleBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement element)
            {
                throw new NotImplementedException();
            }
        }
        public static Brush GetPageTitleBrush(FrameworkElement element)
            => (Brush)element.GetValue(PageTitleBrushProperty);
        public static void SetPageTitleBrush(FrameworkElement element, Brush value)
            => element.SetValue(PageTitleBrushProperty, value);
        #endregion PageTitleBrush Property


        

        #endregion

        #region BackButtonTitle Property
        public static readonly DependencyProperty BackButtonTitleProperty = DependencyProperty.RegisterAttached(
            "BackButtonTitle",
            typeof(string),
            typeof(NavigationExtensions),
            new PropertyMetadata(default(string), OnBackButtonTitleChanged)
        );
        static void OnBackButtonTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement element)
            {
                throw new NotImplementedException();
            }
        }
        public static string GetBackButtonTitle(FrameworkElement element)
            => (string)element.GetValue(BackButtonTitleProperty);
        public static void SetBackButtonTitle(FrameworkElement element, string value)
            => element.SetValue(BackButtonTitleProperty, value);
        #endregion BackButtonTitle Property


        #endregion

        /// <summary>
        /// Navigate to a pre-instantiated page
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static async Task Navigate(this Frame frame, Page page)
        {

        }

    }
}
