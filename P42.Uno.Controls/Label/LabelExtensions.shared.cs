using System.Text;
using Windows.Foundation;

namespace P42.Uno.Controls;

public static class LabelExtensions
{
    private static TextBlock SizingTextBlock = new();

    private static double _defaultFontSize = -1;
    internal static double DefaultFontSize
    {
        get
        {
            if (_defaultFontSize < 0)
            {
                var metaData = TextBlock.FontSizeProperty.GetMetadata(typeof(TextBlock));
                _defaultFontSize = (double)metaData.DefaultValue;
            }
            return _defaultFontSize;
        }
    }

    private static Size Infinite = new(double.PositiveInfinity, double.PositiveInfinity);

    internal static string LinesOfText(int lines)
    {
        var sb = new StringBuilder();
        for (var i = 0; i < lines; i++)
        {
            if (i < lines - 1)
                sb.Append("Hg\n");
            else
                sb.Append("Hg");
        }
        return sb.ToString();
    }

    extension(TextBlock textBlock)
    {
        internal double HeightForLinesAtFontSize(int lines, double fontSize)
        {
            lock (SizingTextBlock)
            {
                SizingTextBlock.SizePropertiesFrom(textBlock);
                SizingTextBlock.Text = LinesOfText(lines);
                SizingTextBlock.FontSize = fontSize;
                SizingTextBlock.Measure(Infinite);
                return SizingTextBlock.DesiredSize.Height;
            }
        }

        internal double FontSizeFromLinesInHeight(int lines, double targetHeight)
        {
            var currentHeight = 0.0;
            var currentFontSize = textBlock.FontSize;
            lock (SizingTextBlock)
            {
                SizingTextBlock.SizePropertiesFrom(textBlock);
                SizingTextBlock.Text = LinesOfText(lines);
                SizingTextBlock.Measure(Infinite);
                currentHeight = SizingTextBlock.DesiredSize.Height;
            }
            var fontSize = currentFontSize * targetHeight / currentHeight;
            return fontSize;
        }

        internal void SizePropertiesFrom(TextBlock source)
        {
            textBlock.CharacterSpacing = source.CharacterSpacing;
            textBlock.FontFamily = source.FontFamily;
            textBlock.FontSize = source.FontSize;
            textBlock.FontStretch = source.FontStretch;
            textBlock.FontStyle = source.FontStyle;
            textBlock.FontWeight = source.FontWeight;
            textBlock.LineHeight = source.LineHeight;
            textBlock.LineStackingStrategy = source.LineStackingStrategy;
            textBlock.MaxLines = source.MaxLines;
            textBlock.OpticalMarginAlignment = source.OpticalMarginAlignment;
            textBlock.Padding = source.Padding;
            textBlock.TextTrimming = source.TextTrimming;  //None, CharacterEllipsis, WordEllipsis, Clip
            textBlock.TextWrapping = source.TextWrapping;  //None, Wrap, WrapWholeWords
        }
    }
}
