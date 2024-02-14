using P42.Utils.Uno;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using SkiaSharp.Views.Windows;
using P42.Uno.Markup;

namespace P42.Uno.Controls
{
    [Microsoft.UI.Xaml.Data.Bindable]
    //[System.ComponentModel.Bindable(System.ComponentModel.BindableSupport.Yes)]
    public partial class EmbeddedSvgImage : SKXamlCanvas
    {
        #region Properties

        #region Stretch Property
        public static readonly DependencyProperty StretchProperty = DependencyProperty.Register(
            nameof(Stretch),
            typeof(Stretch),
            typeof(EmbeddedSvgImage),
            new PropertyMetadata(Microsoft.UI.Xaml.Media.Stretch.Uniform)
        );
        public Stretch Stretch
        {
            get => (Stretch)GetValue(StretchProperty);
            set => SetValue(StretchProperty, value);
        }
        #endregion Stretch Property


        #endregion


        #region Fields

        SkiaSharp.Extended.Svg.SKSvg _skSvg;

        #endregion


        #region Construction
        public EmbeddedSvgImage()
        {
            Background = Colors.Transparent.ToBrush();
            PaintSurface += OnPaintSurface;
            MinHeight = 20;
            MinWidth = 20;
#if __IOS__
            Opaque = false;
#endif

        }

        public EmbeddedSvgImage(string resourceId, Assembly assembly = null) : this()
        {
            SetSource(resourceId, assembly);
        }
#endregion


        public void SetSource(string resourceId, Assembly assembly = null)
        {
            _skSvg = null;

            if (string.IsNullOrWhiteSpace(resourceId))
                return;

            assembly = assembly ?? P42.Utils.Uno.EmbeddedResourceExtensions.FindAssemblyForResourceId(resourceId);
            if (assembly == null)
                return;

            using (var stream = P42.Utils.Uno.EmbeddedResourceExtensions.FindStreamForResourceId(resourceId, assembly))
            {
                if (stream is null)
                {
                    var resources = assembly.GetManifestResourceNames();
                    Console.WriteLine($"ERROR: Cannot find embedded resource [{resourceId}] in assembly [{assembly}].");
                    Console.WriteLine($"       Resources found:");
                    foreach (var resource in resources)
                        Console.WriteLine($"       [{resource}]");
                }
                else
                {
                    _skSvg = new SkiaSharp.Extended.Svg.SKSvg();
                    _skSvg.Load(stream);
                    Invalidate();
                }
            }
        }

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            if (e?.Surface?.Canvas is not SKCanvas workingCanvas)
                return;

            workingCanvas.Clear();
            if (_skSvg?.Picture is not SKPicture picture)
                return;
            workingCanvas.Save();

            var fillRect = e.Info.Rect;
            var fillRectAspect = (float)fillRect.Width / (float)fillRect.Height;
            var imageAspect = (_skSvg.CanvasSize.Width < 1 || _skSvg.CanvasSize.Height < 1) 
                ? 1 
                : _skSvg.CanvasSize.Width / _skSvg.CanvasSize.Height;

            if (_skSvg.CanvasSize.Width <= 0 || _skSvg.CanvasSize.Height <= 0)
            {
                Console.WriteLine("Cannot tile, scale or justify an SVG image with zero or negative Width or Height. Verify, in the SVG source, that the x, y, width, height, and viewBox attributes of the <SVG> tag are present and set correctly.");
            }
            else if (Stretch == Stretch.UniformToFill)
            {
                var scale = imageAspect > fillRectAspect 
                    ? fillRect.Height / _skSvg.CanvasSize.Height 
                    : fillRect.Width / _skSvg.CanvasSize.Width;
                workingCanvas.Scale(scale, scale);
            }
            else if (Stretch == Stretch.Uniform)
            {
                var scale = imageAspect > fillRectAspect 
                    ? fillRect.Width / _skSvg.CanvasSize.Width 
                    : fillRect.Height / _skSvg.CanvasSize.Height;
                workingCanvas.Scale(scale, scale);
            }
            else if (Stretch == Stretch.Fill)
            {
                var scaleX = fillRect.Width / _skSvg.CanvasSize.Width;
                var scaleY = fillRect.Height / _skSvg.CanvasSize.Height;
                workingCanvas.Scale(scaleX, scaleY);
            }


            if (Opacity < 1.0 && Opacity >= 0)
            {
                var alpha = (byte)(Opacity * 255);
                var transparency = SKColors.White.WithAlpha(alpha); 
                var paint = new SKPaint { ColorFilter = SKColorFilter.CreateBlendMode(transparency, SKBlendMode.DstIn) };
                workingCanvas.DrawPicture(picture, paint);
            }
            else
                workingCanvas.DrawPicture(picture);

            workingCanvas.Restore();


        }

    }
}
