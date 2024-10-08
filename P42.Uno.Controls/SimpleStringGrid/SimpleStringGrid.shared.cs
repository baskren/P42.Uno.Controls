using P42.Uno.Markup;
using P42.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Shapes;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI;

namespace P42.Uno.Controls
{
    [Microsoft.UI.Xaml.Data.Bindable]
    public partial class SimpleStringGrid : UserControl
    {
        #region Properties

        #region AlternatingRowBackground Property
        public static readonly DependencyProperty AlternatingRowBackgroundProperty = DependencyProperty.Register(
            nameof(AlternatingRowBackground),
            typeof(Brush),
            typeof(SimpleStringGrid),
            new PropertyMetadata(P42.Uno.Markup.SystemBrushes.DefaultTextForeground, (d, e) => ((SimpleStringGrid)d).OnSourceChanged())
        );
        public Brush AlternatingRowBackground
        {
            get => (Brush)GetValue(AlternatingRowBackgroundProperty);
            set => SetValue(AlternatingRowBackgroundProperty, value);
        }
        #endregion AlternatingRowBackground Property

        #region AlternatingRowForeground Property
        public static readonly DependencyProperty AlternatingRowForegroundProperty = DependencyProperty.Register(
            nameof(AlternatingRowForeground),
            typeof(Brush),
            typeof(SimpleStringGrid),
            new PropertyMetadata(P42.Uno.Markup.SystemBrushes.TextFillPrimary, (d, e) => ((SimpleStringGrid)d).OnSourceChanged())
        );
        public Brush AlternatingRowForeground
        {
            get => (Brush)GetValue(AlternatingRowForegroundProperty);
            set => SetValue(AlternatingRowForegroundProperty, value);
        }
        #endregion AlternatingRowForeground Property

        #region ItemsSource Property
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            nameof(ItemsSource),
            typeof(IList<IList<string>>),
            typeof(SimpleStringGrid),
            new PropertyMetadata(default(IList<IList<string>>), (d,e) =>  ((SimpleStringGrid)d).OnSourceChanged())
        );
        public IList<IList<string>> ItemsSource
        {
            get => (IList<IList<string>>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }
        #endregion ItemsSource Property

        #region RowBackground Property
        public static readonly DependencyProperty RowBackgroundProperty = DependencyProperty.Register(
            nameof(RowBackground),
            typeof(Brush),
            typeof(SimpleStringGrid),
            new PropertyMetadata(new SolidColorBrush(Microsoft.UI.Colors.Transparent), (d, e) => ((SimpleStringGrid)d).OnSourceChanged())
        );
        public Brush RowBackground
        {
            get => (Brush)GetValue(RowBackgroundProperty);
            set => SetValue(RowBackgroundProperty, value);
        }
        #endregion RowBackground Property

        #region RowForeground Property
        public static readonly DependencyProperty RowForegroundProperty = DependencyProperty.Register(
            nameof(RowForeground),
            typeof(Brush),
            typeof(SimpleStringGrid),
            new PropertyMetadata(P42.Uno.Markup.SystemBrushes.DefaultTextForeground, (d, e) => ((SimpleStringGrid)d).OnSourceChanged())
        );
        public Brush RowForeground
        {
            get => (Brush)GetValue(RowForegroundProperty);
            set => SetValue(RowForegroundProperty, value);
        }
        #endregion RowForeground Property

        #region RowHeight Property
        public static readonly DependencyProperty RowHeightProperty = DependencyProperty.Register(
            nameof(RowHeight),
            typeof(double),
            typeof(SimpleStringGrid),
            new PropertyMetadata(default(double))
        );
        public double RowHeight
        {
            get => (double)GetValue(RowHeightProperty);
            set => SetValue(RowHeightProperty, value);
        }
        #endregion RowHeight Property

        #region GridLinesVisibility Property
        public static readonly DependencyProperty GridLinesVisibilityProperty = DependencyProperty.Register(
            nameof(GridLinesVisibility),
            typeof(DataGridGridLinesVisibility),
            typeof(SimpleStringGrid),
            new PropertyMetadata(default(DataGridGridLinesVisibility),(d,e) =>
            {
                if (
                    (((DataGridGridLinesVisibility)e.OldValue) | DataGridGridLinesVisibility.Vertical) !=
                    (((DataGridGridLinesVisibility)e.NewValue) | DataGridGridLinesVisibility.Vertical)
                    )
                    ((SimpleStringGrid)d).ResetColumns();
                else
                    ((SimpleStringGrid)d).OnSourceChanged();
            })
        );
        public DataGridGridLinesVisibility GridLinesVisibility
        {
            get => (DataGridGridLinesVisibility)GetValue(GridLinesVisibilityProperty);
            set => SetValue(GridLinesVisibilityProperty, value);
        }
        #endregion GridLinesVisibility Property

        #region HorizontalGridLinesBrush Property
        public static readonly DependencyProperty HorizontalGridLinesBrushProperty = DependencyProperty.Register(
            nameof(HorizontalGridLinesBrush),
            typeof(Brush),
            typeof(SimpleStringGrid),
            new PropertyMetadata(SystemBrushes.ControlStrokeDefault)
        );
        public Brush HorizontalGridLinesBrush
        {
            get => (Brush)GetValue(HorizontalGridLinesBrushProperty);
            set => SetValue(HorizontalGridLinesBrushProperty, value);
        }
        #endregion HorizontalGridLinesBrush Property

        #region HorizontalScrollBarVisibility Property
        public static readonly DependencyProperty HorizontalScrollBarVisibilityProperty = DependencyProperty.Register(
            nameof(HorizontalScrollBarVisibility),
            typeof(ScrollBarVisibility),
            typeof(SimpleStringGrid),
            new PropertyMetadata(default(ScrollBarVisibility), (d,e) => ((SimpleStringGrid)d)._scrollViewer.HorizontalScrollBarVisibility = (ScrollBarVisibility)e.NewValue)
        );
        public ScrollBarVisibility HorizontalScrollBarVisibility
        {
            get => (ScrollBarVisibility)GetValue(HorizontalScrollBarVisibilityProperty);
            set => SetValue(HorizontalScrollBarVisibilityProperty, value);
        }
        #endregion HorizontalScrollBarVisibility Property

        #region VerticalGridLinesBrush Property
        public static readonly DependencyProperty VerticalGridLinesBrushProperty = DependencyProperty.Register(
            nameof(VerticalGridLinesBrush),
            typeof(Brush),
            typeof(SimpleStringGrid),
            new PropertyMetadata(default(Brush), (d, e) => ((SimpleStringGrid)d).OnSourceChanged())
        );
        public Brush VerticalGridLinesBrush
        {
            get => (Brush)GetValue(VerticalGridLinesBrushProperty);
            set => SetValue(VerticalGridLinesBrushProperty, value);
        }
        #endregion VerticalGridLinesBrush Property

        #region VerticalScrollBarVisibility Property
        public static readonly DependencyProperty VerticalScrollBarVisibilityProperty = DependencyProperty.Register(
            nameof(VerticalScrollBarVisibility),
            typeof(ScrollBarVisibility),
            typeof(SimpleStringGrid),
            new PropertyMetadata(default(ScrollBarVisibility), (d, e) => ((SimpleStringGrid)d)._scrollViewer.VerticalScrollBarVisibility = (ScrollBarVisibility)e.NewValue)
        );
        public ScrollBarVisibility VerticalScrollBarVisibility
        {
            get => (ScrollBarVisibility)GetValue(VerticalScrollBarVisibilityProperty);
            set => SetValue(VerticalScrollBarVisibilityProperty, value);
        }
        #endregion VerticalScrollBarVisibility Property


        #endregion


        #region Fields
        Grid _grid = new Grid()
        {
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch,
        };

        ScrollViewer _scrollViewer = new ScrollViewer
        {
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch,
        };

        List<TextBlock> textBlocksStore = new List<TextBlock>();
        List<Rectangle> rectanglesStore = new List<Rectangle>();
        int columnsCount;
        List<TextBlock> textBlocksBuffer = new List<TextBlock>();
        List<Rectangle> backgroundRectanglesBuffer = new List<Rectangle>();
        List<Rectangle> hzGridlinesRectanglesBuffer = new List<Rectangle>();
        List<Rectangle> vtGridlinesRectanglesBuffer = new List<Rectangle>();
        List<double> MaxColumnWidths = new List<double>();
        #endregion


        #region Construction
        public SimpleStringGrid()
        {
            HorizontalAlignment = HorizontalAlignment.Stretch;
            VerticalAlignment = VerticalAlignment.Stretch;
            Content = _scrollViewer.Content(_grid);

        }

        #endregion


        #region Event Handlers
        #endregion


        #region Helper Methods

        void ResetColumns()
        {
            MaxColumnWidths.Clear();
            OnSourceChanged();
        }

        void OnSourceChanged()
        {
            RecordColumnWidths();

            Clear();
            UpdateColumnsCount();

            if (ItemsSource is null)
            {
                MaxColumnWidths.Clear();
                return;
            }

            int rowIndex = 0;
            if ((GridLinesVisibility & DataGridGridLinesVisibility.Horizontal) == 0)
                AddRow(0, rowIndex++, true);

            for (int i=0; i < ItemsSource.Count; i++)
            {
                AddRow(i, rowIndex);

                var row = ItemsSource[i];

                int colIndex = 0;
                if ((GridLinesVisibility & DataGridGridLinesVisibility.Vertical) == 0)
                    AddColumn(i, colIndex++, true);

                for (int j=0; j<row.Count;j++)
                {
                    AddColumn(i, colIndex);
                    AddText(row[j], i, rowIndex, colIndex++);
                    if ((GridLinesVisibility & DataGridGridLinesVisibility.Vertical) == 0)
                        AddColumn(i, colIndex++, true);
                }

                rowIndex++;
                if ((GridLinesVisibility & DataGridGridLinesVisibility.Horizontal) == 0)
                    AddRow(i, rowIndex++, true);

            }

            _grid.Children.AddRange(backgroundRectanglesBuffer);
            backgroundRectanglesBuffer.Clear();

            _grid.Children.AddRange(hzGridlinesRectanglesBuffer);
            hzGridlinesRectanglesBuffer.Clear();

            _grid.Children.AddRange(vtGridlinesRectanglesBuffer);
            vtGridlinesRectanglesBuffer.Clear();

            _grid.Children.AddRange(textBlocksBuffer);
            textBlocksBuffer.Clear();


        }

        void RecordColumnWidths()
        {
            for (int i = 0; i < _grid.ColumnDefinitions.Count; i++)
            {
                while (MaxColumnWidths.Count <= i)
                    MaxColumnWidths.Add(0.0);
                
                MaxColumnWidths[i] = Math.Max(MaxColumnWidths[i],_grid.ColumnDefinitions[i].ActualWidth);
            }
        }

        void AddText(string text, int i, int rowIndex, int colIndex)
        {
            textBlocksBuffer.Add(
                GetTextBlock()
                    .Text(text)
                    .RowCol(rowIndex, colIndex)
                    .Foreground(i%2>0
                        ? AlternatingRowForeground 
                        : RowForeground)
                );
        }

        void AddRow(int i, int rowIndex, bool isSeparator = false) 
        {
            if (isSeparator)
            {
                _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1) });
                hzGridlinesRectanglesBuffer.Add(GetRectangle().Fill(HorizontalGridLinesBrush).RowCol(rowIndex, 0).ColumnSpan(columnsCount * 2 + 1).RowSpan(1));
            }
            else
            {
                _grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                var fill = i % 2 > 0
                    ? AlternatingRowBackground 
                    : RowBackground;
                backgroundRectanglesBuffer.Add(GetRectangle().Fill(fill).RowCol(rowIndex, 0).ColumnSpan(columnsCount * 2 + 1).RowSpan(1));
            }
        }

        void AddColumn(int i, int colIndex, bool isSeparator = false)
        {
            if (i == 0)
            {
                ColumnDefinition columnDef;
                if (isSeparator)
                {
                    columnDef = new ColumnDefinition { Width = new GridLength(1) };
                    _grid.ColumnDefinitions.Add(columnDef);
                    var rect = GetRectangle();
                    rect.Fill(VerticalGridLinesBrush);
                    Grid.SetRow(rect, 0);
                    Grid.SetColumn(rect, colIndex);
                    Grid.SetRowSpan(rect, ItemsSource.Count * 2);
                    Grid.SetColumnSpan(rect, 1);
                    vtGridlinesRectanglesBuffer.Add(rect);
                }
                else if (colIndex == columnsCount * 2 - 1)
                {
                    columnDef = new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) };
                    _grid.ColumnDefinitions.Add(columnDef);
                }
                else
                {
                    columnDef = new ColumnDefinition { Width = GridLength.Auto };
                    _grid.ColumnDefinitions.Add(columnDef);
                }

                if (colIndex < MaxColumnWidths.Count)
                    columnDef.MinWidth = MaxColumnWidths[colIndex];
            }
        }

        void UpdateColumnsCount()
        {
            columnsCount = 0;
            if (ItemsSource is null)
                return;

            for (int i = 0; i < ItemsSource.Count; i++)
            {
                var row = ItemsSource[i];
                columnsCount = Math.Max(row.Count, columnsCount);
            }

        }



        void Clear()
        {
            foreach (var child in _grid.Children)
            {
                if (child is TextBlock tb)
                    textBlocksStore.Add(tb);
                else if (child is Rectangle rect)
                    rectanglesStore.Add(rect);
            }

            _grid.Children.Clear();
            _grid.ColumnDefinitions.Clear();
            _grid.RowDefinitions.Clear();
        }

        TextBlock GetTextBlock()
        {
            if (textBlocksStore.LastOrDefault() is TextBlock tb)
            {
                textBlocksStore.Remove(tb);
                return tb;
            }

            return new TextBlock()
            {
                HorizontalAlignment= HorizontalAlignment.Left,
                VerticalAlignment= VerticalAlignment.Center,
                Foreground = SystemBrushes.DefaultTextForeground,
                Margin = new Thickness(6),
                FontSize = 15
            };
        }

        Rectangle GetRectangle()
        {
            if (rectanglesStore.LastOrDefault() is Rectangle rect)
            {
                rectanglesStore.Remove(rect);
                return rect;
            }

            return new Rectangle().Stretch();
        }
        #endregion

    }


    public enum DataGridGridLinesVisibility
    {
        /// <summary>
        /// None DataGridGridLinesVisibility
        /// </summary>
        None = 0,

        /// <summary>
        /// Horizontal DataGridGridLinesVisibility
        /// </summary>
        Horizontal = 1,

        /// <summary>
        /// Vertical DataGridGridLinesVisibility
        /// </summary>
        Vertical = 2,

        /// <summary>
        /// All DataGridGridLinesVisibility
        /// </summary>
        All = 3,
    }
}
