using P42.Utils.Uno;
using SkiaSharp;
using SkiaSharp.Views.UWP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace P42.Uno.Controls
{
    public partial class EmbeddedSvgImage : SKXamlCanvas
    {
        #region Properties

        #region Stretch Property
        public static readonly DependencyProperty StretchProperty = DependencyProperty.Register(
            nameof(Stretch),
            typeof(Stretch),
            typeof(EmbeddedSvgImage),
            new PropertyMetadata(Windows.UI.Xaml.Media.Stretch.Uniform)
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
            PaintSurface += OnPaintSurface;
        }

        public EmbeddedSvgImage(Assembly assembly, string resourceId) : this()
        {
            SetSource(assembly, resourceId);
        }
        #endregion


        public void SetSource(Assembly assembly, string resourceId)
        {
            using (var stream = assembly.GetManifestResourceStream(resourceId))
            {
                _skSvg = new SkiaSharp.Extended.Svg.SKSvg();
                _skSvg.Load(stream);  
            }
        }

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {

            var canvas = e.Surface?.Canvas;
            if (canvas == null)
                return;

            var clipBounds = canvas.LocalClipBounds;

            var workingCanvas = canvas;
            var fillRect = e.Info.Rect;

            workingCanvas.Save();

            var fillRectAspect = (float)fillRect.Width / (float)fillRect.Height;
            var imageAspect = (_skSvg.CanvasSize.Width < 1 || _skSvg.CanvasSize.Height < 1) ? 1 : _skSvg.CanvasSize.Width / _skSvg.CanvasSize.Height;
            double scaleX = 1.0;
            double scaleY = 1.0;
            if (_skSvg.CanvasSize.Width <= 0 || _skSvg.CanvasSize.Height <= 0)
            {
                scaleX = scaleY = 1;
                Console.WriteLine("Cannot tile, scale or justify an SVG image with zero or negative Width or Height. Verify, in the SVG source, that the x, y, width, height, and viewBox attributes of the <SVG> tag are present and set correctly.");
            }
            else if (Stretch == Stretch.UniformToFill)
            {
                scaleX = imageAspect > fillRectAspect ? fillRect.Height / _skSvg.CanvasSize.Height : fillRect.Width / _skSvg.CanvasSize.Width;
                scaleY = scaleX;
            }
            else if (Stretch == Stretch.Uniform)
            {
                scaleX = imageAspect > fillRectAspect ? fillRect.Width / _skSvg.CanvasSize.Width : fillRect.Height / _skSvg.CanvasSize.Height;
                scaleY = scaleX;
            }
            else if (Stretch == Stretch.Fill)
            {
                scaleX = fillRect.Width / _skSvg.CanvasSize.Width;
                scaleY = fillRect.Height / _skSvg.CanvasSize.Height;
            }

            var scaledWidth = _skSvg.CanvasSize.Width * scaleX;
            var scaledHeight = _skSvg.CanvasSize.Height * scaleY;

            float left = 0;
            float top = 0;
            if (_skSvg.CanvasSize.Width > 0 && _skSvg.CanvasSize.Height > 0)
            {
                switch (HorizontalAlignment)
                {
                    case HorizontalAlignment.Left:
                        left = 0;
                        break;
                    case HorizontalAlignment.Right:
                        left = (float)(fillRect.Width - scaledWidth);
                        break;
                    default:
                        left = (float)(fillRect.Width - scaledWidth) / 2.0f;
                        break;
                }
                switch (VerticalAlignment)
                {
                    case VerticalAlignment.Top:
                        top = 0;
                        break;
                    case VerticalAlignment.Bottom:
                        top = (float)(fillRect.Height - scaledHeight);
                        break;
                    default:
                        top = (float)(fillRect.Height - scaledHeight) / 2.0f;
                        break;
                }
            }
            //var shadowPadding = ShapeBase.ShadowPadding(this, true);
            //workingCanvas.Translate(left + (float)shadowPadding.Left, top + (float)shadowPadding.Top);
            workingCanvas.Scale((float)scaleX, (float)scaleY);
            SKPaint paint = null;
            /*
            if (shadowPaint == null && TintColor != Color.Default && TintColor != Color.Transparent)
            {
                var color = new SKColor(TintColor.ByteR(), TintColor.ByteG(), TintColor.ByteB(), TintColor.ByteA());
                var cf = SKColorFilter.CreateBlendMode(color, SKBlendMode.SrcIn);

                paint = new SKPaint
                {
                    ColorFilter = cf,
                    IsAntialias = true
                };
            }
            else 
            */
            if (Opacity < 1.0)
            {
                var transparency = SKColors.White.WithAlpha((byte)(Opacity * 255)); // 127 => 50%
                paint = new SKPaint { ColorFilter = SKColorFilter.CreateBlendMode(transparency, SKBlendMode.DstIn) };
            }
            workingCanvas.DrawPicture(_skSvg.Picture, paint);
            workingCanvas.Restore();


        }

    }
}
