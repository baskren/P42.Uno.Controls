using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.UI.Xaml.Controls;
//using P42.Uno.Markup;
using System.Threading.Tasks;
using P42.Utils.Uno;
using P42.Utils;
using Windows.UI;
using Microsoft.UI;
using System.Linq;
using Microsoft.UI.Xaml.Media;
using System.Threading;
using Microsoft.UI.Xaml;

namespace P42.Uno.Controls.AnimateBar
{
    [Microsoft.UI.Xaml.Data.Bindable]
    public partial class Base : Grid, IDisposable
    {
        #region Properties

        #region Brush Property
        public static readonly DependencyProperty BrushProperty = DependencyProperty.Register(
            nameof(Brush),
            typeof(Brush),
            typeof(Base),
            new PropertyMetadata(default(Brush), OnBrushPropertyChanged)
        );

        private static void OnBrushPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Base a)
            {
                if (e.NewValue is Brush brush)
                    //a.StaticRect.Fill =
                    a.DynamicRect.Fill =  brush;
                else
                    //a.StaticRect.Fill = 
                    a.DynamicRect.Fill = null;
            }
        }

        public Brush Brush
        {
            get => (Brush)GetValue(BrushProperty);
            set => SetValue(BrushProperty, value);
        }
        #endregion Brush Property

        #endregion


        #region Fields
        protected Microsoft.UI.Xaml.Shapes.Rectangle DynamicRect = new Microsoft.UI.Xaml.Shapes.Rectangle
        {
            StrokeThickness = 0,
        };
        /*
        protected Microsoft.UI.Xaml.Shapes.Rectangle StaticRect = new Microsoft.UI.Xaml.Shapes.Rectangle
        {
            StrokeThickness = 0,
        };
        */

        protected int dir = 1;
        protected TimeSpan ActionTime = TimeSpan.FromSeconds(0.5);
        protected TimeSpan LullTime = TimeSpan.FromSeconds(1);
        protected bool looping;
        protected bool _disposed;
        #endregion


        #region Construction / Disposal
        public Base()
        {
            //StaticRect.Fill =
            DynamicRect.Fill = new SolidColorBrush(Colors.White); // SystemColors.BaseHigh.ToBrush();
            Children.Add(DynamicRect);
            //Children.Add(StaticRect);

            Loaded += Base_Loaded;
            Background = new SolidColorBrush(Color.FromArgb(1, 1, 1, 1));
        }


#if ANDROID
        protected override void Dispose(bool disposing)
        {
            _disposed |= disposing;
            base.Dispose(disposing);
        }


#elif WINDOWS
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                }

                _disposed = true;
            }
        }

        public virtual void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            System.GC.SuppressFinalize(this);
        }

#endif

        #endregion


        #region vitual methods
        private void Base_Loaded(object sender, RoutedEventArgs e)
        {
            if (looping)
                return;

            looping = true;
            Loop(dir).Forget();
        }


#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        protected virtual async Task Loop(int dir)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
