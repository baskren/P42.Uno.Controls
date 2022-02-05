using P42.Uno.Markup;
using P42.Utils.Uno;
using P42.Utils;
using SkiaSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Shapes;
using Element = P42.Uno.Controls.NewSegmentedControl;

namespace P42.Uno.Controls
{
    public static class NewSegmentedControlExtensions
    {
        public static Element Padding(this Element element, Thickness padding)
        { element.Padding = padding; return element;}

        public static Element Padding(this Element element, double hz, double vt)
        { element.Padding = new Thickness(hz, vt, hz, vt); return element; }

        public static Element Padding(this Element element, double padding)
        { element.Padding = new Thickness(padding); return element; }

        public static Element Labels(this Element element, params string[] labels)
        { element.Labels = labels; return element; }

        public static Element BorderWidth(this Element element, double width)
        { element.BorderWidth = width; return element; }

        public static Element SelectedIndex(this Element element, int index)
        { element.SelectedIndex = index; return element; }

        public static Element SelectedLabel(this Element element, string label)
        { element.SelectedLabel = label; return element; }

        public static Element AllowUnselectAll(this Element element, bool allow = true)
        { element.AllowUnselectAll = allow; return element; }

        public static Element SelectedIndexes(this Element element, params int[] indexes)
        { element.SelectedIndexes = indexes.ToList(); return element; }

        public static Element SelectedItems(this Element element, params string[] items)
        { element.SelectedItems = items.ToList(); return element; }

        public static Element SelectionMode(this Element element, SelectionMode mode)
        { element.SelectionMode = mode; return element; }

        public static Element RadioSelect(this Element element)
        { element.SelectionMode = Controls.SelectionMode.Radio; return element; }

        public static Element MultiSelect(this Element element)
        { element.SelectionMode = Controls.SelectionMode.Multi; return element; }

        public static Element NoneSelect(this Element element)
        { element.SelectionMode= Controls.SelectionMode.None; return element; }

        public static Element AddOnIsOverflowedChanged(this Element element, EventHandler<bool> handler)
        { element.IsOverflowedChanged += handler; return element; }

        public static Element AddOnSelectionChanged(this Element element, EventHandler<(int SelectedIndex, string SelectedLabel)> handler)
        { element.SelectionChanged += handler; return element; }
    }
}