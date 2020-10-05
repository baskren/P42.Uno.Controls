using P42.Utils.Uno;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Uno.Extensions;
using Uno.Extensions.ValueType;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

#if NETFX_CORE
using Popup = Windows.UI.Xaml.Controls.Primitives.Popup;
#else
using Popup = Windows.UI.Xaml.Controls.Popup;
#endif


// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace P42.Uno.Popups
{
    
    [TemplatePart(Name = ContentPresenterName, Type = typeof(ContentPresenter))]
    //[TemplatePart(Name = BorderElementName, Type = typeof(BubbleBorder))]
    [TemplatePart(Name = PopupElementName, Type = typeof(Toast))]
    public partial class Toast : ContentControl, ITargetedPopup
    {

        #region Properties
        #region HasShadow Property
        public static readonly DependencyProperty HasShadowProperty = DependencyProperty.Register(
            nameof(HasShadow),
            typeof(bool),
            typeof(Toast),
            new PropertyMetadata(default(bool))
        );
        public bool HasShadow
        {
            get => (bool)GetValue(HasShadowProperty);
            set => SetValue(HasShadowProperty, value);
        }

        #endregion

        #region PopAfter Property
        public static readonly DependencyProperty PopAfterProperty = DependencyProperty.Register(
            nameof(PopAfter),
            typeof(TimeSpan),
            typeof(Toast),
            new PropertyMetadata(default(TimeSpan))
        );
        public TimeSpan PopAfter
        {
            get => (TimeSpan)GetValue(PopAfterProperty);
            set => SetValue(PopAfterProperty, value);
        }
        #endregion PopAfter Property

        #region Target Properties

        #region Target Property
        public static readonly DependencyProperty TargetProperty = DependencyProperty.Register(
            nameof(Target),
            typeof(UIElement),
            typeof(Toast),
            new PropertyMetadata(default(UIElement))
        );
        public UIElement Target
        {
            get => (UIElement)GetValue(TargetProperty);
            set => SetValue(TargetProperty, value);
        }
        #endregion Target Property

        #region TargetPoint Property
        public static readonly DependencyProperty TargetPointProperty = DependencyProperty.Register(
            nameof(TargetPoint),
            typeof(Point),
            typeof(Toast),
            new PropertyMetadata(default(Point))
        );
        public Point TargetPoint
        {
            get => (Point)GetValue(TargetPointProperty);
            set => SetValue(TargetPointProperty, value);
        }
        #endregion TargetPoint Property

        #endregion

        #region Pointer Properties

        #region PointerBias Property
        public static readonly DependencyProperty PointerBiasProperty = DependencyProperty.Register(
            nameof(PointerBias),
            typeof(double),
            typeof(Toast),
            new PropertyMetadata(0.5)
        );
        /// <summary>
        /// Gets or sets the bias (0.0 is start; 0.5 is center;  1.0 is end; greater than 1.0 is pixels from start; less than 0.0 is pixels from end)of the pointer relative to the chosen face on the target.
        /// </summary>
        /// <value>The target bias.</value>
        public double PointerBias
        {
            get => (double)GetValue(PointerBiasProperty);
            set => SetValue(PointerBiasProperty, value);
        }
        #endregion PointerBias Property

        #region PointerCornerRadius Property
        public static readonly DependencyProperty PointerCornerRadiusProperty = DependencyProperty.Register(
            nameof(PointerCornerRadius),
            typeof(double),
            typeof(Toast),
            new PropertyMetadata(default(double))
        );
        public double PointerCornerRadius
        {
            get => (double)GetValue(PointerCornerRadiusProperty);
            set => SetValue(PointerCornerRadiusProperty, value);
        }
        #endregion PointerCornerRadius Property

        #region Pointer Directions

        public PointerDirection ActualPointerDirection { get; private set; }

        #region PreferredPointerDirection Property
        public static readonly DependencyProperty PreferredPointerDirectionProperty = DependencyProperty.Register(
            nameof(PreferredPointerDirection),
            typeof(PointerDirection),
            typeof(Toast),
            new PropertyMetadata(default(PointerDirection))
        );
        public PointerDirection PreferredPointerDirection
        {
            get => (PointerDirection)GetValue(PreferredPointerDirectionProperty);
            set => SetValue(PreferredPointerDirectionProperty, value);
        }
        #endregion PreferredPointerDirection Property

        #region FallbackPointerDirection Property
        public static readonly DependencyProperty FallbackPointerDirectionProperty = DependencyProperty.Register(
            nameof(FallbackPointerDirection),
            typeof(PointerDirection),
            typeof(Toast),
            new PropertyMetadata(default(PointerDirection))
        );
        public PointerDirection FallbackPointerDirection
        {
            get => (PointerDirection)GetValue(FallbackPointerDirectionProperty);
            set => SetValue(FallbackPointerDirectionProperty, value);
        }
        #endregion FallbackPointerDirection Property

        #endregion

        #region PointerLength Property
        public static readonly DependencyProperty PointerLengthProperty = DependencyProperty.Register(
            nameof(PointerLength),
            typeof(double),
            typeof(Toast),
            new PropertyMetadata(10.0)
        );
        /// <summary>
        /// Gets or sets the length of the bubble layout's pointer.
        /// </summary>
        /// <value>The length of the pointer.</value>
        public double PointerLength
        {
            get => (double)GetValue(PointerLengthProperty);
            set => SetValue(PointerLengthProperty, value);
        }
        #endregion PointerLength Property

        #region PointerTipRadius Property
        public static readonly DependencyProperty PointerTipRadiusProperty = DependencyProperty.Register(
            nameof(PointerTipRadius),
            typeof(double),
            typeof(Toast),
            new PropertyMetadata(default(double))
        );
        /// <summary>
        /// Gets or sets the radius of the bubble's pointer tip.
        /// </summary>
        /// <value>The pointer tip radius.</value>
        public double PointerTipRadius
        {
            get => (double)GetValue(PointerTipRadiusProperty);
            set => SetValue(PointerTipRadiusProperty, value);
        }
        #endregion PointerTipRadius Property

        #region PointToOffScreenElements Property
        public static readonly DependencyProperty PointToOffScreenElementsProperty = DependencyProperty.Register(
            nameof(PointToOffScreenElements),
            typeof(bool),
            typeof(Toast),
            new PropertyMetadata(default(bool))
        );
        public bool PointToOffScreenElements
        {
            get => (bool)GetValue(PointToOffScreenElementsProperty);
            set => SetValue(PointToOffScreenElementsProperty, value);
        }
        #endregion PointToOffScreenElements Property


        #endregion

        public PopupPoppedCause PoppedCause => _popup.PoppedCause;

        public object PoppedTrigger => _popup.PoppedTrigger;

        #endregion

        #region Fields
        const string ContentPresenterName = "TargetedPopup_Popup_Border_ContentPresenter";
        const string BorderElementName = "TargetedPopup_Popup_Border";
        const string PopupElementName = "TargetedPopup_Popup";
        ContentPresenter _contentPresenter;
        TargetedPopup _popup;
        #endregion


        #region Construction / Initialization
        public Toast()
        {
        }



        public event EventHandler<PopupPoppedEventArgs> Popped
        {
            add =>_popup.Popped += value;
            remove =>_popup.Popped -= value;
        }

        public Task PopAsync(PopupPoppedCause cause, [CallerMemberName] object trigger = null)
        {
            return _popup.PopAsync(cause, trigger);
        }

        public Task PushAsync()
        {
            if (Parent is Grid grid)
            {
                //Grid.SetRow(this, 0);
                //Grid.SetColumn(this, 0);
                //Grid.SetColumnSpan(this, grid.ColumnDefinitions.Count);
                //Grid.SetRowSpan(this, grid.RowDefinitions.Count);
                grid.Children.Remove(this);
            }

            return _popup.PushAsync();
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            
            _contentPresenter = GetTemplateChild(ContentPresenterName) as ContentPresenter;
            var popupChild = GetTemplateChild(PopupElementName);
            
            _popup = popupChild as TargetedPopup;

            if (_popup is null)
            {
                System.Diagnostics.Debug.WriteLine(GetType() + ".");
            }
            
        }
        #endregion


    }
}
