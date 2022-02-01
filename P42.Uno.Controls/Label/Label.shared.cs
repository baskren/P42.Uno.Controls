using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Uno.Disposables;
using Uno.Extensions;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml;
using Uno.UI.DataBinding;
using System;
using Uno.UI;
using System.Collections;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Text;
using Windows.Foundation;
using Windows.UI.Input;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Automation.Peers;
using Uno;
using P42.Uno.Markup;
using P42.Utils.Uno;
using Windows.UI.Xaml.Controls;

namespace P42.Uno.Controls
{
	[ContentProperty(Name = "Text")]
	public partial class AutoTextBlock : UserControl
	{

		#region Properties

		#region Inlines

		ObservableCollection<Inline> _inlines;

        /// <summary>
        /// Gets an InlineCollection containing the top-level Inline elements that comprise the contents of the TextBlock.
        /// </summary>
        /// <remarks>
        /// Accessing this property initializes an InlineCollection, whose content will be synchronized with the Text.
        /// This can have a significant impact on performance. Only access this property if absolutely necessary.
        /// </remarks>
        public IList<Inline> Inlines
        {
			get
			{
				if (_inlines is null)
				{
					_inlines = new ObservableCollection<Inline>();
					_inlines.CollectionChanged += OnInlinesCollectionChanged;
				}
				return _inlines;
			}
        }

        #endregion


        #region TextWrapping Dependency Property
        public static DependencyProperty TextWrappingProperty =
			DependencyProperty.Register(
				nameof(TextWrapping),
				typeof(TextWrapping),
				typeof(AutoTextBlock),
				new PropertyMetadata(TextWrapping.NoWrap, (s, e) => ((AutoTextBlock)s).OnTextWrappingChanged())
			);
		void OnTextWrappingChanged()
		{
			_textBlock.TextWrapping = TextWrapping;
			if (LabelAutoFit != LabelAutoFit.None)
				InvalidateMeasure();
		}
		public TextWrapping TextWrapping
		{
			get => (TextWrapping)GetValue(TextWrappingProperty);
			set => SetValue(TextWrappingProperty, value);
		}
		#endregion

		#region Text Dependency Property
		public static DependencyProperty TextProperty =
			DependencyProperty.Register(
				nameof(Text),
				typeof(string),
				typeof(AutoTextBlock),
				new PropertyMetadata(string.Empty, (s, e) => ((AutoTextBlock)s).OnTextChanged())
			);
		protected virtual void OnTextChanged()
		{
			_textBlock.Text = Text;
			if (LabelAutoFit != LabelAutoFit.None)
				InvalidateMeasure();
		}
		public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}
		#endregion

		#region MaxLines Dependency Property
		public static DependencyProperty MaxLinesProperty =
			DependencyProperty.Register(
				"MaxLines",
				typeof(int),
				typeof(AutoTextBlock),
				new PropertyMetadata(0, (s, e) => ((AutoTextBlock)s).OnMaxLinesChanged())
			);
		private void OnMaxLinesChanged()
		{
			_textBlock.MaxLines = MaxLines;
			if (LabelAutoFit != LabelAutoFit.None)
				InvalidateMeasure();
		}
		public int MaxLines
		{
			get => (int)GetValue(MaxLinesProperty);
			set => SetValue(MaxLinesProperty, value);
		}
		#endregion

		#region TextTrimming Dependency Property
		public static DependencyProperty TextTrimmingProperty =
			DependencyProperty.Register(
				"TextTrimming",
				typeof(TextTrimming),
				typeof(AutoTextBlock),
				new PropertyMetadata(TextTrimming.None, (s, e) => ((AutoTextBlock)s).OnTextTrimmingChanged())
			);
		private void OnTextTrimmingChanged() => _textBlock.TextTrimming = TextTrimming;
		public TextTrimming TextTrimming
		{
			get => (TextTrimming)GetValue(TextTrimmingProperty);
			set => SetValue(TextTrimmingProperty, value);
		}
		#endregion

		#region HorizontalTextAlignment Dependency Property
		public static DependencyProperty HorizontalTextAlignmentProperty =
			DependencyProperty.Register(
				"HorizontalTextAlignment",
				typeof(TextAlignment),
				typeof(AutoTextBlock),
				new PropertyMetadata(TextAlignment.Left, (s, e) => ((AutoTextBlock)s).OnHorizontalTextAlignmentChanged())
			);
		void OnHorizontalTextAlignmentChanged() => _textBlock.HorizontalTextAlignment = HorizontalTextAlignment;
		public TextAlignment HorizontalTextAlignment
		{
			get => (TextAlignment)GetValue(HorizontalTextAlignmentProperty);
			set => SetValue(HorizontalTextAlignmentProperty, value);
		}
		#endregion

		#region LineHeight Dependency Property
		public static DependencyProperty LineHeightProperty =
			DependencyProperty.Register(
				"LineHeight",
				typeof(double),
				typeof(AutoTextBlock),
				new PropertyMetadata(0d, (s, e) => ((AutoTextBlock)s).OnLineHeightChanged()));
		private void OnLineHeightChanged()
		{
			_textBlock.LineHeight = LineHeight;
			if (LabelAutoFit != LabelAutoFit.None)
				InvalidateMeasure();
		}
		public double LineHeight
		{
			get => (double)GetValue(LineHeightProperty);
			set => SetValue(LineHeightProperty, value);
		}
		#endregion

		#region LineStackingStrategy Dependency Property
		public static DependencyProperty LineStackingStrategyProperty { get; } =
			DependencyProperty.Register(
				"LineStackingStrategy",
				typeof(LineStackingStrategy),
				typeof(AutoTextBlock),
				new PropertyMetadata(LineStackingStrategy.MaxHeight, (s, e) => ((AutoTextBlock)s).OnLineStackingStrategyChanged()));
		private void OnLineStackingStrategyChanged()
		{
			_textBlock.LineStackingStrategy = LineStackingStrategy;
			if (LabelAutoFit != LabelAutoFit.None)
				InvalidateMeasure();
		}
		public LineStackingStrategy LineStackingStrategy
		{
			get => (LineStackingStrategy)GetValue(LineStackingStrategyProperty);
			set => SetValue(LineStackingStrategyProperty, value);
		}
        #endregion


        #region TextAlignment Property
        public static readonly DependencyProperty TextAlignmentProperty =
            DependencyProperty.Register(
                nameof(TextAlignment),
                typeof(TextAlignment),
                typeof(AutoTextBlock),
                new PropertyMetadata(default(HorizontalAlignment), (s, e) => ((AutoTextBlock)s).OnTextAlignmentChanged(e)));
        protected virtual void OnTextAlignmentChanged(DependencyPropertyChangedEventArgs e)
			=>_textBlock.TextAlignment = TextAlignment;
        public TextAlignment TextAlignment
        {
            get => (TextAlignment)GetValue(TextAlignmentProperty);
            set => SetValue(TextAlignmentProperty, value);
        }
        #endregion TextAlignment Property


        #region TextDecorations
        public static DependencyProperty TextDecorationsProperty =
			DependencyProperty.Register(
				"TextDecorations",
				typeof(uint),
				typeof(AutoTextBlock),
				new PropertyMetadata(TextDecorations.None, (s, e) => ((AutoTextBlock)s).OnTextDecorationsChanged()));
		void OnTextDecorationsChanged() => _textBlock.TextDecorations = TextDecorations;
		public TextDecorations TextDecorations
		{
			get => (TextDecorations)GetValue(TextDecorationsProperty);
			set => SetValue(TextDecorationsProperty, value);
		}
		#endregion



		#region Lines property
		/// <summary>
		/// The backing store for the lines property.
		/// </summary>
		public static readonly DependencyProperty LinesProperty =
			DependencyProperty.Register(nameof(Lines), typeof(int), typeof(AutoTextBlock), new PropertyMetadata(0, (s,e) => ((AutoTextBlock)s).OnLinesChanged()));
        private void OnLinesChanged()
        {
			if (LabelAutoFit != LabelAutoFit.None)
				InvalidateMeasure();
        }
        /// <summary>
        /// Gets or sets the number of lines used in a fit.  If zero and fit is not AutoFit.None or ignored, will maximize the font size to best width and height with minimum number of lines.
        /// </summary>
        /// <value>The lines.</value>
        public int Lines
		{
			get => (int)GetValue(LinesProperty);
			set => SetValue(LinesProperty, value);
		}
		#endregion

		#region LabelAutoFit Property
		public static DependencyProperty LabelAutoFitProperty =
			DependencyProperty.Register(
				nameof(LabelAutoFit),
				typeof(LabelAutoFit),
				typeof(AutoTextBlock),
				new PropertyMetadata(LabelAutoFit.None, (s, e) => ((AutoTextBlock)s).OnLabelAutoFitChanged()));
		void OnLabelAutoFitChanged() { InvalidateMeasure(); }
		public LabelAutoFit LabelAutoFit
        {
			get => (LabelAutoFit)GetValue(LabelAutoFitProperty);
			set => SetValue(LabelAutoFitProperty, value);
        }
		#endregion

		#region MinFontSize Property
		public static DependencyProperty MinFontSizeProperty =
			DependencyProperty.Register(
				nameof(MinFontSize),
				typeof(double),
				typeof(AutoTextBlock),
				new PropertyMetadata(6, (s, e) => ((AutoTextBlock)s).OnMinFontSizeChanged()));
		void OnMinFontSizeChanged()
		{
			if (LabelAutoFit != LabelAutoFit && FittedFontSize < MinFontSize)
				InvalidateMeasure();
		}
		public double MinFontSize
		{
			get => (double)GetValue(MinFontSizeProperty);
			set => SetValue(MinFontSizeProperty, value);
		}
		#endregion

		#region VerticalTextAlignment Property
		public static DependencyProperty VerticalTextAlignmentProperty =
			DependencyProperty.Register(
				nameof(VerticalTextAlignment),
				typeof(VerticalAlignment),
				typeof(AutoTextBlock),
				new PropertyMetadata(VerticalAlignment.Top, (s, e) => ((AutoTextBlock)s).OnVerticalTextAlignmentChanged()));
		void OnVerticalTextAlignmentChanged() => _textBlock.VerticalAlignment = VerticalAlignment;
		public VerticalAlignment VerticalTextAlignment
        {
			get => (VerticalAlignment)GetValue(VerticalTextAlignmentProperty);
			set => SetValue(VerticalTextAlignmentProperty, value);
        }
		#endregion

		#region FittedFontSize
		public static DependencyProperty FittedFontSizeProperty =
			DependencyProperty.Register(
				nameof(FittedFontSize),
				typeof(double),
				typeof(AutoTextBlock),
				new PropertyMetadata(-1.0, (s,e) => ((AutoTextBlock)s).OnFontSizeChanged(s,FittedFontSizeProperty))
			);
		public double FittedFontSize
        {
			get => (double)GetValue(FittedFontSizeProperty);
			private set => SetValue(FittedFontSizeProperty, value);
        }
		#endregion

		#region SynchronizedFontSize
		public static DependencyProperty SynchronizedFontSizeProperty =
			DependencyProperty.Register(
				nameof(SynchronizedFontSize),
				typeof(double),
				typeof(AutoTextBlock),
				new PropertyMetadata(double.NaN, (s,e) => ((AutoTextBlock)s).OnFontSizeChanged(s, SynchronizedFontSizeProperty))
			);
        private void OnSynchronizedFontSizeChanged(DependencyObject s, DependencyPropertyChangedEventArgs e)
			=>_textBlock.FontSize = SynchronizedFontSize;
        public double SynchronizedFontSize
        {
			get => (double)GetValue(SynchronizedFontSizeProperty);
			set => SetValue(SynchronizedFontSizeProperty, value);
        }
		#endregion

        #endregion

        public override string GetAccessibilityInnerText() => _textBlock.Text;


		#region Fields
		const double Precision = 0.05f;
		TextBlock _textBlock = new TextBlock();
		TextBlock _testTextBlock = new TextBlock();
		Grid _grid = new Grid();
		bool _initializing;
		#endregion


		#region Construction / Destruction
		public AutoTextBlock()
		{
			InitializeProperties();
		}

		/// <summary>
		/// Calls On[Property]Changed for most DPs to ensure the values are correctly applied to the native control
		/// </summary>
		private void InitializeProperties()
		{
			HorizontalContentAlignment = HorizontalAlignment.Stretch;
			VerticalContentAlignment = VerticalAlignment.Stretch;

			// TextBlock Property Implementations
			// BaselineOffset : Not implemented in Uno
			RegisterPropertyChangedCallback(UserControl.CharacterSpacingProperty, OnCharacterSpacingChanged);
			// ContentStart : Not implemented in Uno
			// ContentEnd : Not implemented in Uno
			RegisterPropertyChangedCallback(UserControl.FontFamilyProperty, OnFontFamilyChanged);
			RegisterPropertyChangedCallback(UserControl.FontSizeProperty, OnFontSizeChanged);
			// FontStretch : Not implemented in Uno
			RegisterPropertyChangedCallback(UserControl.FontStyleProperty, OnFontStyleChanged);
			RegisterPropertyChangedCallback(UserControl.FontWeightProperty, OnFontWeightChanged);
			_textBlock.Bind(TextBlock.ForegroundProperty, this, nameof(Foreground));
			_textBlock.Bind(TextBlock.HorizontalTextAlignmentProperty, this, nameof(HorizontalTextAlignment));
			// InLines : local
			// IsColorFontEnabled : Not implemented in Uno
			// IsTextScaleFactorEnabled : Not implemented in Uno
			// IsTextSelectionEnabled : Not implmeented in Uno
			// IsTextTrimmed : Not implemented in Uno
			// LineHeight : local
			// LineStackingStrategy : local
			// MaxLines : local
			// OpticalMarginAlignment : Not implmented in Uno
			// Padding : Grid.Padding
			// SelectedText : Not implemented in Uno
			// SelectionEnd : Not implemented in Uno
			// SelectionFlyout : Not implmented in Uno
			// SelectionHeightlightColor : Not implmented in Uno
			// SelectionStart : Not implemnted in Uno
			// TextAlignment : local
			// TextDecorations : local
			// TextHighlighters : Not implmented in Uno
			// TextLineBounds : Not implmented in Uno
			// Text : local
			// TextReadingOrder : Not implmented in Uno
			// TextTrimming : local
			// TextWrapping : local


			// Control Property Implementations
			_grid.Bind(Grid.BackgroundProperty, this, nameof(Background));
			_grid.Bind(Grid.BackgroundSizingProperty, this, nameof(BackgroundSizing));
			_grid.Bind(Grid.BorderBrushProperty, this, nameof(BorderBrush));
			_grid.Bind(Grid.BorderThicknessProperty, this, nameof(BorderThickness));
			// CharacterSpacing : above
			_grid.Bind(Grid.CornerRadiusProperty, this, nameof(CornerRadiusProperty));
			// DefaultKeyStyle : not implemented
			// DefaultStyleResourceUri : not implemented
			// ElementSoundMode : not implemented
			// FocusState : not implemented
			// FontFamily : above
			// FontSize : above
			// FontStretch : above
			// FontStyle : above
			// FontWeight : above
			// Foreground : above
			RegisterPropertyChangedCallback(UserControl.HorizontalContentAlignmentProperty, OnHorizontalContentAlignmentChanged);
			// IsEnabled : not implmented
			// IsFocusEngaged : not implmented
			// IsFocusEngagementEnabled : not implmented
			// isTabStop : not implemented
			// IsTextScaleFactorEnabled : not implemented
			// Padding : above
			// RequiresPointer : not implemented
			// TabIndex : not implemented
			// TabNavigation : not implemented
			// Template : not implemented
			// UseSystemFocusVisuals : not implemented
			RegisterPropertyChangedCallback(UserControl.VerticalContentAlignmentProperty, OnVerticalContentAlignmentChanged);
			// XYFocusDown : not implemented
			// XYFocusLeft : not implemented
			// XYFocusRight : not implemented
			// XYFocusUp : not implemented

			// FrameworkElement
			_textBlock.Bind(TextBlock.DataContextProperty, this, nameof(DataContext));

			_initializing = true;
			OnTextWrappingChanged();
			OnTextChanged();
			OnMaxLinesChanged();
			OnTextTrimmingChanged();
			OnHorizontalTextAlignmentChanged();
			OnLineHeightChanged();
			OnLineStackingStrategyChanged();
			//OnPaddingChanged();
			OnTextDecorationsChanged();
			OnLinesChanged();
			OnLabelAutoFitChanged();
			OnMinFontSizeChanged();
			OnVerticalTextAlignmentChanged();
			_initializing = false;

			_grid.Children.Add(_textBlock);
			Content = _grid;

            Unloaded += OnUnloaded;
		}

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
			if (_inlines is ObservableCollection<Inline> inlines)
				inlines.CollectionChanged += OnInlinesCollectionChanged;
		}

		#endregion


		#region UserControl Property change handlers
		private void OnCharacterSpacingChanged(DependencyObject sender, DependencyProperty dp)
		{
			_textBlock.CharacterSpacing = CharacterSpacing;
			if (LabelAutoFit != LabelAutoFit.None)
				InvalidateMeasure();
		}

		private void OnFontFamilyChanged(DependencyObject sender, DependencyProperty dp)
		{
			_textBlock.FontFamily = FontFamily;
			if (LabelAutoFit != LabelAutoFit.None)
				InvalidateMeasure();
		}

		private void OnFontSizeChanged(DependencyObject sender, DependencyProperty dp)
		{
			if (double.IsNaN(SynchronizedFontSize) || SynchronizedFontSize < 0)
			{
				if (double.IsNaN(FittedFontSize) || FittedFontSize < 0)
				{
					_textBlock.FontSize = FontSize;
					if (dp == FittedFontSizeProperty && !double.IsNaN(FittedFontSize) && FittedFontSize > 0)
						InvalidateMeasure();
				}
				else
					_textBlock.FontSize = FittedFontSize;
			}
			else
			{
				_textBlock.FontSize = SynchronizedFontSize;
			}
		}

		private void OnFontStyleChanged(DependencyObject sender, DependencyProperty dp)
		{
			_textBlock.FontStyle = FontStyle;
			if (LabelAutoFit != LabelAutoFit.None)
				InvalidateMeasure();
		}

		private void OnFontWeightChanged(DependencyObject sender, DependencyProperty dp)
		{
			_textBlock.FontWeight = FontWeight;
			if (LabelAutoFit != LabelAutoFit.None)
				InvalidateMeasure();
		}

		private void OnHorizontalContentAlignmentChanged(DependencyObject sender, DependencyProperty dp)
			=> HorizontalContentAlignment = HorizontalAlignment.Stretch;

		private void OnVerticalContentAlignmentChanged(DependencyObject sender, DependencyProperty dp)
			=> VerticalContentAlignment = VerticalAlignment.Stretch;

		private void OnInlinesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					for (int i = e.NewItems.Count - 1; i >= 0; i--)
						_textBlock.Inlines.Insert(e.NewStartingIndex, e.NewItems[i] as Inline);
					break;
				case NotifyCollectionChangedAction.Remove:
					for (int i = 0; i < e.OldItems.Count; i++)
						_textBlock.Inlines.RemoveAt(e.OldStartingIndex);
					break;
				case NotifyCollectionChangedAction.Replace:
					for (int i = 0; i < e.OldItems.Count; i++)
						_textBlock.Inlines.RemoveAt(e.OldStartingIndex);
					for (int i = e.NewItems.Count - 1; i >= 0; i--)
						_textBlock.Inlines.Insert(e.NewStartingIndex, e.NewItems[i] as Inline);
					break;
				case NotifyCollectionChangedAction.Move:
					for (int i = 0; i < e.OldItems.Count; i++)
						_textBlock.Inlines.RemoveAt(e.OldStartingIndex);
					for (int i = e.NewItems.Count - 1; i >= 0; i--)
						_textBlock.Inlines.Insert(e.NewStartingIndex, e.NewItems[i] as Inline);
					break;
				case NotifyCollectionChangedAction.Reset:
					_textBlock.Inlines.Clear();
					break;
			}
			if (LabelAutoFit != LabelAutoFit.None)
				InvalidateMeasure();
		}

        #endregion


        #region Layout
        protected Size InternalMeasure(Size availableSize)
        {
			if (_initializing || double.IsNaN(availableSize.Width) || double.IsNaN(availableSize.Height) || availableSize.Width <= 0 || availableSize.Height <= 0)
				return Size.Empty;

			var control = _textBlock;

			var width = Math.Min(availableSize.Width, int.MaxValue / 2);
			var height = Math.Min(availableSize.Height, int.MaxValue / 2);
			var result = new Size(width, height);

			if (control is null)
				return result;

			var tmpFontSize = FontSize;

			if (!double.IsNaN(SynchronizedFontSize) && SynchronizedFontSize > MinFontSize)
				tmpFontSize = SynchronizedFontSize;

			var minFontSize = MinFontSize;

			control.MaxLines = 0; // int.MaxValue / 3;
			control.MaxWidth = double.PositiveInfinity;
			control.MaxHeight = double.PositiveInfinity;
			control.MinHeight = 0;
			control.MinWidth = 0;

			control.FontSize = tmpFontSize;
			control.Measure(new Size(width, double.PositiveInfinity));
			var tmpHt = control.DesiredSize.Height;

			if (Lines == 0)
            {
				// do our best job to fit the existing space.
				if (control.DesiredSize.Width - width > -Precision || control.DesiredSize.Height - height > -Precision)
				{
					tmpFontSize = ZeroLinesFit(control, minFontSize, tmpFontSize, width, height);
					control.FontSize = tmpFontSize;
					control.Measure(new Size(width, double.PositiveInfinity));
					tmpHt = control.DesiredSize.Height;
				}
			}
			else if (LabelAutoFit == LabelAutoFit.Lines)
			{
				tmpHt = height;
				if (height > int.MaxValue / 3)
				{
					//tmpHt = height = _fontMetrics.HeightForLinesAtFontSize(element.Lines, tmpFontSize);
					tmpHt = height = _textBlock.HeightForLinesAtFontSize(Lines, tmpFontSize);
				}
				else
				{
					// set the font size to fit Label.Lines into the available height
					var constrainedSize = control.DesiredSize;

					tmpFontSize = _textBlock.FontSizeFromLinesInHeight(Lines, tmpHt);
					tmpFontSize = Math.Max(MinFontSize, tmpFontSize);

					control.FontSize = tmpFontSize;
					control.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
					if (Lines > 1)
					{
						var unconstrainedSize = control.DesiredSize;
						if (unconstrainedSize.Width > constrainedSize.Width)
						{
							control.FontSize = tmpFontSize;
							control.Measure(new Size(width, double.PositiveInfinity));
						}
					}
				}
			}
			else if (LabelAutoFit == LabelAutoFit.Width)
			{
				if (control.ActualWidth > control.DesiredSize.Width || control.DesiredSize.Height / control.LineHeight > Lines)
				{
					tmpFontSize = WidthAndLinesFit(control, Lines + 1, minFontSize, tmpFontSize, width);
					control.FontSize = tmpFontSize;
					control.Measure(new Size(width, double.PositiveInfinity));
					tmpHt = control.DesiredSize.Height;
				}
			}

			// none of these should happen so let's keep an eye out for it to be sure everything upstream is working
			if (tmpFontSize > FontSize)
				throw new Exception("fitting somehow returned a tmpFontSize > label.FontSize");
			if (tmpFontSize < MinFontSize)
				throw new Exception("fitting somehow returned a tmpFontSize < label.MinFontSize");
			// the following doesn't apply when where growing 
			//if (tmpFontSize > label.DecipheredMinFontSize() && (control.DesiredSize.Width > Math.Ceiling(w) || control.DesiredSize.Height > Math.Ceiling(Math.Max(availableSize.Height, label.Height))) )
			//    throw new Exception("We should never exceed the available bounds if the FontSize is greater than label.MinFontSize");

			// we needed the following in Android as well.  Xamarin layout really doesn't like this to be changed in real time.
			if (tmpFontSize == FontSize || ((FontSize == -1 || double.IsNaN(FontSize)) && tmpFontSize == AutoTextBlockExtensions.DefaultFontSize))
				FittedFontSize = -1;
			else if (Math.Abs(FittedFontSize - tmpFontSize) > 1)
				FittedFontSize = tmpFontSize;

			if (!double.IsNaN(SynchronizedFontSize))
				_textBlock.FontSize = SynchronizedFontSize;

			result = new Size(Math.Ceiling(control.DesiredSize.Width), Math.Ceiling(tmpHt));

			control.MaxLines = MaxLines; // int.MaxValue / 3;
			control.MaxWidth = MaxWidth;
			control.MaxHeight = MaxHeight;
			control.MinHeight = MinHeight;
			control.MinWidth = MinWidth;

			return result;
		}

		static double ZeroLinesFit(TextBlock control, double min, double max, double availWidth, double availHeight)
		{
			if (control is null)
				return max;

			if (availHeight > int.MaxValue / 3)
				return max;
			if (availWidth > int.MaxValue / 3)
				return max;

			if (control.FontSize == max && availHeight >= control.DesiredSize.Height)
				return max;
			if (control.FontSize == min && availHeight <= control.DesiredSize.Height)
				return min;

			if (max - min < Precision)
				return min;

			var mid = (max + min) / 2.0;
			control.FontSize = mid;
			control.Measure(new Size(availWidth - 4, double.PositiveInfinity));
			var height = control.DesiredSize.Height;

			if (height > availHeight)
				return ZeroLinesFit(control, min, mid, availWidth, availHeight);
			if (height < availHeight)
				return ZeroLinesFit(control, mid, max, availWidth, availHeight);
			return mid;
		}

		double WidthAndLinesFit(TextBlock control, int lines, double min, double max, double availWidth)
		{
			if (control is null)
				return max;

			if (max - min < Precision)
			{
				if (control.FontSize != min)
				{
					control.FontSize = min;
					control?.Measure(new Size(availWidth - 4, double.PositiveInfinity));
				}
				return min;
			}
			var mid = (max + min) / 2.0;
			control.FontSize = mid;
			control.Measure(new Size(availWidth, double.PositiveInfinity));
			var midHeight = control.DesiredSize.Height;
			if (control.DesiredSize.Height > midHeight || control.DesiredSize.Width > availWidth)
				return WidthAndLinesFit(control, lines, min, mid, availWidth);
			return WidthAndLinesFit(control, lines, mid, max, availWidth);
		}

		double FindBestWidthForLineFit(TextBlock control, double fontSize, double heightTarget, double min, double max)
		{
			if (control is null)
				return max;

			if (max - min < Precision)
				return min;
			var mid = (max + min) / 2.0;
			control.FontSize= fontSize;
			control.Measure(new Size(mid, double.PositiveInfinity));
			var height = control.DesiredSize.Height;
			if (height <= heightTarget)
				return FindBestWidthForLineFit(control, fontSize, heightTarget, min, mid);
			else
				return FindBestWidthForLineFit(control, fontSize, heightTarget, mid, max);
		}
        #endregion
    }

}