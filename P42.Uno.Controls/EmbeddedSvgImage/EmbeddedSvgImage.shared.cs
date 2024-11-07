using SkiaSharp;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using Windows.Foundation;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using SkiaSharp.Views.Windows;
using P42.Uno.Markup;
using Svg;

namespace P42.Uno.Controls;

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

    private Svg.Skia.SKSvg _skSvg;
    //private SkiaSharp.Extended.Svg.SKSvg _skSvg;
    private double _canvasAspect = 1.0;
    private float _canvasWidth = 0;
    private float _canvasHeight = 0;
    #endregion


    #region Construction
    public EmbeddedSvgImage()
    {
        Background = Colors.Transparent.ToBrush();
        PaintSurface += OnPaintSurface;
        //MinHeight = 20;
        //MinWidth = 20;
#if __IOS__
        Opaque = false;
#endif

    }

    public EmbeddedSvgImage(string resourceId, Assembly assembly = null) : this()
    {
        SetSource(resourceId, assembly);
    }
    #endregion


    #region SetSource
    public void SetSource(string resourceId, Assembly assembly = null)
    {
        _skSvg = null;

        if (string.IsNullOrWhiteSpace(resourceId))
            return;

        if (!resourceId.EndsWith(".svg", StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException($"Resource ID must end with '.svg'.");

        if (resourceId.StartsWith("http://") || resourceId.StartsWith("https://") || resourceId.StartsWith("file://") || resourceId.StartsWith("ms-appx://"))
        {
            var uri = new Uri(resourceId);
            SetSource(uri);
            return;
        }
            
        assembly = assembly ?? P42.Utils.Uno.EmbeddedResourceExtensions.FindAssemblyForResourceId(resourceId);
        if (assembly == null)
            return;

        using var stream = P42.Utils.Uno.EmbeddedResourceExtensions.FindStreamForResourceId(resourceId, assembly);
        if (stream is null)
        {
            var resources = assembly.GetManifestResourceNames();
            Console.WriteLine($"ERROR: Cannot find embedded resource [{resourceId}] in assembly [{assembly}].");
            Console.WriteLine($"       Resources found:");
            foreach (var resource in resources)
                Console.WriteLine($"       [{resource}]");
        }
        else
            SetSource(stream);
    }

    public void SetSource(Uri uri)
    {
        if (uri.IsFile)
        {
            SetSourceFromPath(uri.LocalPath);
            return;
        }
        var aRequest = (HttpWebRequest)WebRequest.Create(uri);
        using var aResponse = (HttpWebResponse)aRequest.GetResponse();
        using var stream = aResponse.GetResponseStream();
        SetSource(stream);
    }

    public void SetSourceFromPath(string path)
    {
        using var stream = File.OpenRead(path);
        SetSource(stream);
    }

    public void SetSource(Stream stream)
    {
        if (stream is null)
            return;

        
        _skSvg = new Svg.Skia.SKSvg();
        //_skSvg = new SkiaSharp.Extended.Svg.SKSvg();
        _skSvg.Load(stream);
        /*
        _imageAspect = _skSvg.CanvasSize.Width < 1 || _skSvg.CanvasSize.Height < 1 
            ? 1 
            : _skSvg.CanvasSize.Width / _skSvg.CanvasSize.Height;
        */
        /*
        System.Diagnostics.Debug.WriteLine($"SKSvg loaded:");
        System.Diagnostics.Debug.WriteLine($"\t Bounds: {_skSvg.Drawable.Bounds.Width}x{_skSvg.Drawable.Bounds.Height}");
        System.Diagnostics.Debug.WriteLine($"\t Model.CullRect: {_skSvg.Model?.CullRect}");
        System.Diagnostics.Debug.WriteLine($"\t Picture.CullRect: {_skSvg.Picture?.CullRect}");

        var p = _skSvg.Parameters?.Entities;
        if (p is not null)
        {
            foreach (var kvp in p)
            {
                System.Diagnostics.Debug.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
        }

        System.Diagnostics.Debug.WriteLine($"\t {_skSvg.Drawable.Bounds.Width}x{_skSvg.Drawable.Bounds.Height}");
        */
        
        _canvasWidth = _skSvg.Picture?.CullRect.Width ?? _skSvg.Model?.CullRect.Width ?? 10;
        _canvasHeight =_skSvg.Picture?.CullRect.Height ??  _skSvg.Model?.CullRect.Height ?? 10;
        _canvasAspect = _canvasWidth < 1 || _canvasHeight < 1
            ? 1
            : _canvasWidth / _canvasHeight;
        
        //Invalidate();
        InvalidateMeasure();
    }
    #endregion
    
    
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
        
        // System.Diagnostics.Debug.WriteLine($"{nameof(EmbeddedSvgImage)}.OnPaintSurface");
        // System.Diagnostics.Debug.WriteLine($"\t fillRect:[{fillRect.Width}x{fillRect.Height}] aspect: {fillRectAspect}  Stretch:[{Stretch}]");

        //if (_skSvg.CanvasSize.Width <= 0 || _skSvg.CanvasSize.Height <= 0)
        if (_skSvg.Drawable.Bounds.Width <= 0 || _skSvg.Drawable.Bounds.Height <= 0)
        {
            Console.WriteLine("Cannot tile, scale or justify an SVG image with zero or negative Width or Height. Verify, in the SVG source, that the x, y, width, height, and viewBox attributes of the <SVG> tag are present and set correctly.");
        }
        else if (Stretch == Stretch.UniformToFill)
        {
            var scale = _canvasAspect > fillRectAspect
                // ? fillRect.Height / _skSvg.CanvasSize.Height 
                // : fillRect.Width / _skSvg.CanvasSize.Width;
                ? fillRect.Height / _canvasHeight
                : fillRect.Width / _canvasWidth;
            System.Diagnostics.Debug.WriteLine($"\t scale:[{scale}]");
            workingCanvas.Scale(scale, scale);
        }
        else if (Stretch == Stretch.Uniform)
        {
            var scale = _canvasAspect > fillRectAspect
                // ? fillRect.Width / _skSvg.CanvasSize.Width 
                // : fillRect.Height / _skSvg.CanvasSize.Height;
                ? fillRect.Width / _canvasWidth
                : fillRect.Height / _canvasHeight;
            System.Diagnostics.Debug.WriteLine($"\t scale:[{scale}]");
            workingCanvas.Scale(scale, scale);
        }
        else if (Stretch == Stretch.Fill)
        {
            // var scaleX = fillRect.Width / _skSvg.CanvasSize.Width;
            // var scaleY = fillRect.Height / _skSvg.CanvasSize.Height;
            var scaleX = fillRect.Width / _canvasWidth;
            var scaleY = fillRect.Height / _canvasHeight;
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

    
    protected override Size MeasureOverride(Size availableSize)
    {
        if (_skSvg?.Picture is not SKPicture)
            return new Size(MinWidth, MinHeight);
        
        
        // System.Diagnostics.Debug.WriteLine($"{nameof(EmbeddedSvgImage)}.MeasureOverride({availableSize.Width}, {availableSize.Height}) isInfinity:[{double.IsInfinity(availableSize.Width)}, {double.IsInfinity(availableSize.Height)}] _canvasWidth:{_canvasWidth} _canvasHeight:{_canvasHeight}");
        if (double.IsInfinity(availableSize.Width) == double.IsInfinity(availableSize.Height))
            return base.MeasureOverride(availableSize);
            
        var availableWidth = Math.Max(availableSize.Width, MinWidth);
        var availableHeight = Math.Max(availableSize.Height, MinHeight);
        // System.Diagnostics.Debug.WriteLine($"\t availableSize: [{availableWidth}, {availableHeight}] isInfinity:[{double.IsInfinity(availableWidth)}, {double.IsInfinity(availableHeight)}] ");
            
        if (availableWidth < 1 || availableHeight < 1)
            return base.MeasureOverride(availableSize);
            
        var result = double.IsInfinity(availableSize.Width) 
            ? new Size(availableSize.Height  * _canvasAspect, availableSize.Height) 
            : new Size(availableSize.Width, availableSize.Width / _canvasAspect);

        // System.Diagnostics.Debug.WriteLine($"\t result: [{result.Width}, {result.Height}] aspect:[{result.Width/result.Height}] \n");
        return result;
    }

}
