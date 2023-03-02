using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using P42.Uno.Markup;
using P42.Utils.Uno;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace P42.Uno.Controls.Demo
{
    public partial class PageMenu : Page
    {
        
        ListView _listView;

        void Build()
        {

            //var itemTemplate = P42.Utils.Uno.UIElementExtensions.AsDataTemplate(typeof(P42.Uno.Controls.Demo.TextCell));
            var itemTemplate = typeof(PageMenuCellTemplate).AsDataTemplate();

            Content = new Grid()
                .Rows(40, "*", 40)
                .Children
                (
                    new TextBlock()
                        .Text("Test Pages")
                        .StretchHorizontal()
                        .CenterVertical()
                        .CenterTextAlignment(),
                    new ListView()
                        .Row(1)
                        .Assign(out _listView)
                        .ItemTemplate(itemTemplate)
                        //.ItemTemplate("<DataTemplate xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" xmlns:d=\"http://schemas.microsoft.com/expression/blend/2008\" xmlns:local=\"using:P42.Uno.Controls.Demo\" xmlns:mc=\"http://schemas.openxmlformats.org/markup-compatibility/2006\" mc:Ignorable=\"d\" x:DataType=\"system.Type\"><StackPanel Orientation=\"Horizontal\"><TextBlock Text=\"{Binding Name}\" HorizontalAlignment=\"Left\" /></StackPanel></DataTemplate>")
                );
        }

    }

    [Bindable]
    public partial class PageMenuCellTemplate : UserControl
    {
        TextBlock _textBlock;

        public PageMenuCellTemplate()
        {
            Content = new TextBlock()
                .Assign(out _textBlock);

            DataContextChanged += CellTemplate_DataContextChanged;
        }


        private void CellTemplate_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            _textBlock.Text = "TYPE: "+ DataContext?.ToString();
        }

    }

}