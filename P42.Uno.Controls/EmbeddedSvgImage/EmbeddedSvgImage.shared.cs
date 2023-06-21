﻿using P42.Utils.Uno;
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
                }
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

            workingCanvas.Clear();
            if (_skSvg?.Picture is null)
                return;
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
            //workingCanvas.Clear();
            workingCanvas.DrawPicture(_skSvg.Picture, paint);
            workingCanvas.Restore();


        }

    }
}
