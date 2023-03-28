﻿using P42.Uno.Markup;
using P42.Utils.Uno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Shapes;
using Microsoft.UI;
using Windows.UI.ViewManagement;

namespace P42.Uno.Controls
{
    /// <summary>
    /// Base Popup for UWP / UNO PLATFORM
    /// </summary>
    [Microsoft.UI.Xaml.Data.Bindable]
    public partial class TargetedPopup : ITargetedPopup
    {
        #region Properties

        #region Override Properties

        #region Opacity Property
        public static readonly new DependencyProperty OpacityProperty = DependencyProperty.Register(
            nameof(Opacity),
            typeof(double),
            typeof(TargetedPopup),
            new PropertyMetadata(1.0, (d,e) => ((TargetedPopup)d).UpdateOpacity())
        );
        public new double Opacity
        {
            get => (double)GetValue(OpacityProperty);
            set => SetValue(OpacityProperty, value);
        }
        #endregion Opacity Property

        #region Margin Property
        public static readonly new DependencyProperty MarginProperty = DependencyProperty.Register(
            nameof(Margin),
            typeof(Thickness),
            typeof(TargetedPopup),
            new PropertyMetadata(new Thickness(40), (d,e) => ((TargetedPopup)d).UpdateMarginAndAlignment())
        );
        public new Thickness Margin
        {
            get => (Thickness)GetValue(MarginProperty);
            set => SetValue(MarginProperty, value);
        }
        #endregion Margin Property

        #region HorizontalAlignment Property
        public static readonly new DependencyProperty HorizontalAlignmentProperty = DependencyProperty.Register(
            nameof(HorizontalAlignment),
            typeof(HorizontalAlignment),
            typeof(TargetedPopup),
            new PropertyMetadata(HorizontalAlignment.Center, (d, e) => ((TargetedPopup)d).UpdateMarginAndAlignment())
        );
        public new HorizontalAlignment HorizontalAlignment
        {
            get => (HorizontalAlignment)GetValue(HorizontalAlignmentProperty);
            set => SetValue(HorizontalAlignmentProperty, value);
        }
        #endregion HorizontalAlignment Property

        #region VerticalAlignment Property
        public static readonly new DependencyProperty VerticalAlignmentProperty = DependencyProperty.Register(
            nameof(VerticalAlignment),
            typeof(VerticalAlignment),
            typeof(TargetedPopup),
            new PropertyMetadata(VerticalAlignment.Center, (d, e) => ((TargetedPopup)d).UpdateMarginAndAlignment())
        );
        public new VerticalAlignment VerticalAlignment
        {
            get => (VerticalAlignment)GetValue(VerticalAlignmentProperty);
            set => SetValue(VerticalAlignmentProperty, value);
        }
        #endregion VerticalAlignment Property

        #region Background Property
        public static readonly new DependencyProperty BackgroundProperty = DependencyProperty.Register(
            nameof(Background),
            typeof(Brush),
            typeof(TargetedPopup),
            new PropertyMetadata(default(Brush))
        );
        [Obsolete("Use BackgroundColor, instead")]
        public new Brush Background
        {
            get => new SolidColorBrush(BackgroundColor);
            set
            {
                if (value is SolidColorBrush brush)
                    BackgroundColor = brush.Color;
                else
                    throw new Exception("Background not supported, use BackgroundColor instead");
            }
        }
        #endregion Background Property

        #region BackgroundColor Property
        public static readonly DependencyProperty BackgroundColorProperty = DependencyProperty.Register(
            nameof(BackgroundColor),
            typeof(Color),
            typeof(TargetedPopup),
            new PropertyMetadata(default(Color))
        );
        public Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }
        #endregion BackgroundColor Property

        #region BorderBrush Property
        public static readonly new DependencyProperty BorderBrushProperty = DependencyProperty.Register(
            nameof(BorderBrush),
            typeof(Brush),
            typeof(TargetedPopup),
            new PropertyMetadata(default(Brush))
        );
        [Obsolete("Use BorderColor, instead")]
        public new Brush BorderBrush
        {
            get => new SolidColorBrush(BorderColor);
            set
            {
                if (value is SolidColorBrush brush)
                    BorderColor = brush.Color;
                else
                    throw new Exception("BorderBrush not supported, use BorderColor instead");
            }
        }
        #endregion BorderBrush Property

        #region BorderColor Property
        public static readonly DependencyProperty BorderColorProperty = DependencyProperty.Register(
            nameof(BorderColor),
            typeof(Color),
            typeof(TargetedPopup),
            new PropertyMetadata(default(Color))
        );
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }
        #endregion BorderColor Property

        #region BorderThickness Property
        public static readonly new DependencyProperty BorderThicknessProperty = DependencyProperty.Register(
            nameof(BorderThickness),
            typeof(Thickness),
            typeof(TargetedPopup),
            new PropertyMetadata(default(Thickness))
        );
        [Obsolete("Use BorderWidth instead")]
        public new Thickness BorderThickness
        {
            get => new Thickness(BorderWidth);
            set
            {
                if (value is Thickness thickness)
                    BorderWidth = thickness.Max();
            }
        }
        #endregion BorderThickness Property

        #region BorderWidth Property
        public static readonly DependencyProperty BorderWidthProperty = DependencyProperty.Register(
            nameof(BorderWidth),
            typeof(double),
            typeof(TargetedPopup),
            new PropertyMetadata(1.0)
        );
        public double BorderWidth
        {
            get => (double)GetValue(BorderWidthProperty);
            set => SetValue(BorderWidthProperty, value);
        }
        #endregion BorderWidth Property

        #region CornerRadius Property
        public static readonly new DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            nameof(CornerRadius),
            typeof(double),
            typeof(TargetedPopup),
            new PropertyMetadata(6.0)
        );
        public new double CornerRadius
        {
            get => (double)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
        #endregion CornerRadius Property

        #endregion

        #region Target Properties

        #region Target Property
        public static readonly DependencyProperty TargetProperty = DependencyProperty.Register(
            nameof(Target),
            typeof(UIElement),
            typeof(TargetedPopup),
            new PropertyMetadata(default(UIElement), new PropertyChangedCallback((d, e) => ((TargetedPopup)d).UpdateMarginAndAlignment()))
        );
        /// <summary>
        /// The UIElement the popup will point at (no pointer if Target is null or not found)
        /// </summary>
        public UIElement Target
        {
            get => (UIElement)GetValue(TargetProperty);
            set => SetValue(TargetProperty, value);
        }
        #endregion Target Property

        #region TargetRect Property
        public static readonly DependencyProperty TargetRectProperty = DependencyProperty.Register(
            nameof(TargetRect),
            typeof(Rect),
            typeof(TargetedPopup),
            new PropertyMetadata(default(Rect))
        );
        public Rect TargetRect
        {
            get => (Rect)GetValue(TargetRectProperty);
            set => SetValue(TargetRectProperty, value);
        }
        #endregion TargetRect Property

        #endregion

        #region Pointer Properties

        #region PointerBias Property
        public static readonly DependencyProperty PointerBiasProperty = DependencyProperty.Register(
            nameof(PointerBias),
            typeof(double),
            typeof(TargetedPopup),
            new PropertyMetadata(0.5, new PropertyChangedCallback((d, e) => ((TargetedPopup)d).UpdateMarginAndAlignment()))
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
            typeof(TargetedPopup),
            new PropertyMetadata(default(double))
        );

        /// <summary>
        /// For those who want a lot of control!
        /// </summary>
        public double PointerCornerRadius
        {
            get => (double)GetValue(PointerCornerRadiusProperty);
            set => SetValue(PointerCornerRadiusProperty, value);
        }
        #endregion PointerCornerRadius Property

        #region Pointer Directions

        /// <summary>
        /// No, really, what direction is the pointer pointing?
        /// </summary>
        public PointerDirection ActualPointerDirection { get; private set; }

        #region PreferredPointerDirection Property
        public static readonly DependencyProperty PreferredPointerDirectionProperty = DependencyProperty.Register(
            nameof(PreferredPointerDirection),
            typeof(PointerDirection),
            typeof(TargetedPopup),
            new PropertyMetadata(PointerDirection.Any, new PropertyChangedCallback((d, e) => ((TargetedPopup)d).UpdateMarginAndAlignment()))
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
            typeof(TargetedPopup),
            new PropertyMetadata(default(PointerDirection), new PropertyChangedCallback((d, e) => ((TargetedPopup)d).UpdateMarginAndAlignment()))
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
            typeof(TargetedPopup),
            new PropertyMetadata(10.0, new PropertyChangedCallback((d, e) => ((TargetedPopup)d).UpdateMarginAndAlignment()))
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
            typeof(TargetedPopup),
            new PropertyMetadata(2.0)
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
            typeof(TargetedPopup),
            new PropertyMetadata(default(bool), new PropertyChangedCallback((d, e) => ((TargetedPopup)d).UpdateMarginAndAlignment()))
        );
        /// <summary>
        /// If Target is off screen (but known), should the popup still point at it?
        /// </summary>
        public bool PointToOffScreenElements
        {
            get => (bool)GetValue(PointToOffScreenElementsProperty);
            set => SetValue(PointToOffScreenElementsProperty, value);
        }
        #endregion PointToOffScreenElements Property

        #region PointerMargin Property
        public static readonly DependencyProperty PointerMarginProperty = DependencyProperty.Register(
            nameof(PointerMargin),
            typeof(double),
            typeof(TargetedPopup),
            new PropertyMetadata(3.0, new PropertyChangedCallback((d, e) => ((TargetedPopup)d).UpdateMarginAndAlignment()))
        );

        /// <summary>
        /// How much space between popup pointer and Target?
        /// </summary>
        public double PointerMargin
        {
            get => (double)GetValue(PointerMarginProperty);
            set => SetValue(PointerMarginProperty, value);
        }
        #endregion PointerMargin Property

        #endregion

        #region PageOverlay Properties

        #region PageOverlayBrush Property
        public static readonly DependencyProperty PageOverlayBrushProperty = DependencyProperty.Register(
            nameof(PageOverlayBrush),
            typeof(Brush),
            typeof(TargetedPopup),
            new PropertyMetadata(new SolidColorBrush(Colors.Transparent))
        );
        public Brush PageOverlayBrush
        {
            get => (Brush)GetValue(PageOverlayBrushProperty);
            set => SetValue(PageOverlayBrushProperty, value);
        }
        #endregion PageOverlayBrush Property

        #region IsPageOverlayHitTestVisible Property
        public static readonly DependencyProperty IsPageOverlayHitTestVisibleProperty = DependencyProperty.Register(
            nameof(IsPageOverlayHitTestVisible),
            typeof(bool),
            typeof(TargetedPopup),
            new PropertyMetadata(true)
        );
        public bool IsPageOverlayHitTestVisible
        {
            get => (bool)GetValue(IsPageOverlayHitTestVisibleProperty);
            set => SetValue(IsPageOverlayHitTestVisibleProperty, value);
        }
        #endregion IsPageOverlayHitTestVisible Property

        #endregion PageOverlay properties

        #region HasShadow Property
        public static readonly DependencyProperty HasShadowProperty = DependencyProperty.Register(
            nameof(HasShadow),
            typeof(bool),
            typeof(TargetedPopup),
            new PropertyMetadata(true)
        );
        public bool HasShadow
        {
            get => (bool)GetValue(HasShadowProperty);
            set => SetValue(HasShadowProperty, value);
        }
        #endregion HasShadow Property

        #region Push/Pop Properties

        //TODO: TEST
        #region PopAfter Property
        public static readonly DependencyProperty PopAfterProperty = DependencyProperty.Register(
            nameof(PopAfter),
            typeof(TimeSpan),
            typeof(TargetedPopup),
            new PropertyMetadata(default(TimeSpan))
        );

        /// <summary>
        /// If greater than default TimeSpan, this is the amount of time the popup will display before automatically being popped.`
        /// </summary>
        public TimeSpan PopAfter
        {
            get => (TimeSpan)GetValue(PopAfterProperty);
            set => SetValue(PopAfterProperty, value);
        }
        #endregion PopAfter Property

        //TODO: TEST
        #region PopOnPointerMove Property
        public static readonly DependencyProperty PopOnPointerMoveProperty = DependencyProperty.Register(
            nameof(PopOnPointerMove),
            typeof(bool),
            typeof(TargetedPopup),
            new PropertyMetadata(default(bool))
        );
        /// <summary>
        /// Causes the popup to be dismissed (popped) when the Pointer (mouse) moves outside of the Target
        /// </summary>
        public bool PopOnPointerMove
        {
            get => (bool)GetValue(PopOnPointerMoveProperty);
            set => SetValue(PopOnPointerMoveProperty, value);
        }
        #endregion PopOnPointerMove Property

        #region PopOnPageOverlayTouch Property
        /// <summary>
        /// CancelOnPageOverlayTouch backing store
        /// </summary>
        public static readonly DependencyProperty PopOnPageOverlayTouchProperty = DependencyProperty.Register(
            nameof(PopOnPageOverlayTouch),
            typeof(bool),
            typeof(TargetedPopup),
            new PropertyMetadata(true)
        );
        /// <summary>
        /// Will the popup pop upon a PageOverlay touch?
        /// </summary>
        public bool PopOnPageOverlayTouch
        {
            get => (bool)GetValue(PopOnPageOverlayTouchProperty);
            set => SetValue(PopOnPageOverlayTouchProperty, value);
        }
        #endregion PopOnPageOverlayTouch Property

        //TODO: IMPLEMENT AND TEST
        #region PopOnBackButtonClick Property
        /// <summary>
        /// CancelOnBackButtonClick property backing store
        /// </summary>
        public static readonly DependencyProperty PopOnBackButtonClickProperty = DependencyProperty.Register(
            nameof(PopOnBackButtonClick),
            typeof(bool),
            typeof(TargetedPopup),
            new PropertyMetadata(true)
        );
        /// <summary>
        /// Will the popup pop upon a mobile device [BACK] button click?
        /// </summary>
        public bool PopOnBackButtonClick
        {
            get => (bool)GetValue(PopOnBackButtonClickProperty);
            set => SetValue(PopOnBackButtonClickProperty, value);
        }
        #endregion PopOnBackButtonClick Property

        #region Parameter Property
        /// <summary>
        /// Parameter property backing store
        /// </summary>
        public static readonly DependencyProperty ParameterProperty = DependencyProperty.Register(
            nameof(Parameter),
            typeof(object),
            typeof(TargetedPopup),
            new PropertyMetadata(default)
        );
        /// <summary>
        /// Object that can be set prior to appearance of Popup for the purpose of application to processing after the popup is disappeared
        /// </summary>
        public object Parameter
        {
            get => (object)GetValue(ParameterProperty);
            set => SetValue(ParameterProperty, value);
        }
        #endregion Parameter Property

        #region PushEffect Property
        public static readonly DependencyProperty PushEffectProperty = DependencyProperty.Register(
            nameof(PushEffect),
            typeof(Effect),
            typeof(TargetedPopup),
            new PropertyMetadata(default(Effect))
        );
        public Effect PushEffect
        {
            get => (Effect)GetValue(PushEffectProperty);
            set => SetValue(PushEffectProperty, value);
        }
        #endregion PushEffect Property

        #region PushEffectMode Property
        public static readonly DependencyProperty PushEffectModeProperty = DependencyProperty.Register(
            nameof(PushEffectMode),
            typeof(EffectMode),
            typeof(TargetedPopup),
            new PropertyMetadata(default(EffectMode))
        );
        public EffectMode PushEffectMode
        {
            get => (EffectMode)GetValue(PushEffectModeProperty);
            set => SetValue(PushEffectModeProperty, value);
        }
        #endregion PushEffectMode Property

        #region PoppedCause Property
        /// <summary>
        /// Why did the popup pop?
        /// </summary>
        public PopupPoppedCause PoppedCause { get; private set; }
        #endregion

        #region PoppedTrigger Property
        /// <summary>
        /// What triggered the popup to pop?
        /// </summary>
        public object PoppedTrigger { get; private set; }
        #endregion

        #region PushPopState Property
        public static readonly DependencyProperty PushPopStateProperty = DependencyProperty.Register(
            nameof(PushPopState),
            typeof(PushPopState),
            typeof(TargetedPopup),
            new PropertyMetadata(default(PushPopState), new PropertyChangedCallback((d,e)=>((TargetedPopup)d).OnPushPopStateChanged(e)))
        );

        protected virtual void OnPushPopStateChanged(DependencyPropertyChangedEventArgs e)
        {
            
        }

        /// <summary>
        /// Is the popup Pushing, Pushed, Popping, or Popped?
        /// </summary>
        public PushPopState PushPopState
        {
            get => (PushPopState)GetValue(PushPopStateProperty);
            internal set => SetValue(PushPopStateProperty, value);
        }
        #endregion PushPopState Property

        #region AnimationDuration Property
        public static readonly DependencyProperty AnimationDurationProperty = DependencyProperty.Register(
            nameof(AnimationDuration),
            typeof(TimeSpan),
            typeof(TargetedPopup),
            new PropertyMetadata(TimeSpan.FromMilliseconds(200))
        );
        /// <summary>
        /// How long to animate the dismissal of the popup?
        /// </summary>
        public TimeSpan AnimationDuration
        {
            get => (TimeSpan)GetValue(AnimationDurationProperty);
            set => SetValue(AnimationDurationProperty, value);
        }
        #endregion AnimationDuration Property

        #endregion Push/Pop Properties

        static int _pushingCount = 0;
        public static bool IsPushing
        {
            get => _pushingCount > 0;
            private set
            {
                _pushingCount += value ? 1 : -1;
            }

        }

        #endregion


        #region Private Properties
        bool HasBorder
        {
            get
            {
                if (BorderWidth <= 0)
                    return false;
                if (BorderBrush is Brush brush)
                {
                    if (brush is SolidColorBrush solidBrush)
                        return solidBrush.Color.A > 0;
                    return true;
                }
                return false;
            }
        }
        #endregion


        #region Events
        public event EventHandler Pushed;
        /// <summary>
        /// Occurs when popup has been cancelled.
        /// </summary>
        public event EventHandler<PopupPoppedEventArgs> Popped;
        #endregion


        #region Construction / Initialization

        /// <summary>
        /// Create new TargetedPopup
        /// </summary>
        /// <param name="target">UI Element popup points to</param>
        /// <param name="bubbleContent">What's the popup's content?</param>
        /// <returns></returns>
        public static async Task<TargetedPopup> CreateAsync(UIElement target, UIElement bubbleContent, TimeSpan popAfter = default, Effect pushEffect = Effect.None, EffectMode effectMode = EffectMode.Default)
        {
            var result = new TargetedPopup
            {
                Target = target,
                Content = bubbleContent,
                PopAfter = popAfter,
                PushEffect = pushEffect,
                PushEffectMode = effectMode
            };
            await result.PushAsync();
            return result;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public TargetedPopup()
        {
            Build();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="target">UI Element popup points to</param>
        public TargetedPopup(UIElement target) : this()
        {
            if (target != null)
                Target = target;
        }

        #endregion


        #region Pointer Move Event Handlers

        Point _enteredPoint = new(-1,-1);
        private void OnPointerEntered(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            _enteredPoint = e.GetCurrentPoint(P42.Utils.Uno.Platform.Window.Content).Position;
            //System.Diagnostics.Debug.WriteLine("TargetedPopup.OnPointerEntered e: [" + _enteredPoint.X + ", " + _enteredPoint.Y + "]");
        }

        async void OnPointerMoved(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (PopOnPointerMove)
            {
                var position = e.GetCurrentPoint(P42.Utils.Uno.Platform.Window.Content).Position;
                System.Diagnostics.Debug.WriteLine("TargetedPopup.OnPointerMoved e: [" + position.X + ", " + position.Y + "]");

                if (Target != null)
                {

                    var zone = new Rect(position.X - 5, position.Y - 5, 10, 10);

                    var targetBounds = Target.GetBounds();
                    var borderBounds = ContentBorder.GetBounds();

                    if (Microsoft.UI.Xaml.RectHelper.Intersect(targetBounds, zone) is Rect intersect0
                        && intersect0.Width > 0
                        && intersect0.Height > 0)
                    {
                        System.Diagnostics.Debug.WriteLine("\t\t RectHelper.Intersect(targetBounds, zone) = " + RectHelper.Intersect(targetBounds, zone));
                        System.Diagnostics.Debug.WriteLine("\t\t in Target ["+ targetBounds + "]");
                        return;
                    }
                    else if (Microsoft.UI.Xaml.RectHelper.Intersect(borderBounds, zone) is Rect intersect1
                        && intersect1.Width > 0
                        && intersect1.Height > 0)
                    {
                        System.Diagnostics.Debug.WriteLine("\t\t RectHelper.Intersect(borderBounds, zone) = " + RectHelper.Intersect(borderBounds, zone));
                        System.Diagnostics.Debug.WriteLine("\t\t in Border");
                        return;
                    }
                    else if (Microsoft.UI.Xaml.RectHelper.Intersect(borderBounds, targetBounds) is Rect intersect2
                        && intersect2.Width <= 0
                        && intersect2.Height <= 0)
                    {
                        System.Diagnostics.Debug.WriteLine("\t\t RectHelper.Intersect(borderBounds, targetBounds) = " + RectHelper.Intersect(borderBounds, targetBounds));
                        System.Diagnostics.Debug.WriteLine("\t\t testing Bridge");

                        var bridge = new Rect();
                        if (targetBounds.Left > borderBounds.Right)
                        {
                            bridge.X = borderBounds.Right;
                            bridge.Width = targetBounds.Left - borderBounds.Right;
                        }
                        else if (targetBounds.Right < borderBounds.Left)
                        {
                            bridge.X = targetBounds.Right;
                            bridge.Width = borderBounds.Left - targetBounds.Right;
                        }
                        else
                        {
                            bridge.X = Math.Max(targetBounds.X, borderBounds.X);
                            bridge.Width = Math.Min(targetBounds.Width, borderBounds.Width);
                        }
                        if (targetBounds.Top > borderBounds.Bottom)
                        {
                            bridge.Y = borderBounds.Bottom;
                            bridge.Height = targetBounds.Top - borderBounds.Bottom;
                        }
                        else if (targetBounds.Bottom < borderBounds.Top)
                        {
                            bridge.Y = targetBounds.Bottom;
                            bridge.Height = borderBounds.Top - targetBounds.Bottom;
                        }
                        else
                        {
                            bridge.Y = Math.Max(targetBounds.Y, borderBounds.Y);
                            bridge.Width = Math.Min(targetBounds.Height, borderBounds.Height);
                        }

                        bridge.X -= 5;
                        bridge.Y -= 5;
                        bridge.Width += 10;
                        bridge.Height += 10;

                        if (Microsoft.UI.Xaml.RectHelper.Intersect(bridge, zone) is Rect intersect3
                            && intersect3.Width > 0
                            && intersect3.Height > 0)
                        {
                            System.Diagnostics.Debug.WriteLine("\t\t in Bridge");
                            return;
                        }
                        
                    }
                    System.Diagnostics.Debug.WriteLine("\t\t Popping");
                    await PopAsync(PopupPoppedCause.PointerMoved);

                }
                else
                {
                    if (_enteredPoint.X == -1 && _enteredPoint.Y == -1)
                        _enteredPoint = e.GetCurrentPoint(P42.Utils.Uno.Platform.Window.Content).Position;

                    var dx = position.X - _enteredPoint.X;
                    var dy = position.Y - _enteredPoint.Y;
                    var distance = Math.Sqrt(dx * dx + dy * dy);
                    if (distance > 10)
                        await PopAsync(PopupPoppedCause.PointerMoved);
                }
            }
        }
        #endregion


        #region Push / Pop

        /// <summary>
        /// Push (display) the popup
        /// </summary>
        /// <param name="animated"></param>
        /// <returns></returns>
        public virtual async Task PushAsync(bool animated = false)
        {
            if (PushPopState == PushPopState.Pushed || PushPopState == PushPopState.Pushing)
                return;

            if (PushPopState == PushPopState.Popping)
                await WaitForPoppedAsync();

            await InnerPushAsync(animated);
        }

        async Task InnerPushAsync(bool animated)
        { 
            PushPopState = PushPopState.Pushing;
            await OnPushBeginAsync();
            _popCompletionSource?.TrySetResult(new PopupPoppedEventArgs(PopupPoppedCause.NotPushed, false));
            _popCompletionSource = null;

            PoppedCause = PopupPoppedCause.BackgroundTouch;
            PoppedTrigger = null;

            // required to render popup the first time.
            // UpdateOpacity(0.001);


            try
            {
                System.Diagnostics.Debug.WriteLine("TargetedPopup.InnerPushAsync ======= ");
                UpdateMarginAndAlignment();

                UpdateOpacity(0.0);
                Popups.Add(this);
                await Task.Delay(5);

                await Feedback.PlayAsync(PushEffect, PushEffectMode);
                if (animated)
                {
                    void action(double percent) => UpdateOpacity(Opacity * percent);
                    var animator = new P42.Utils.Uno.ActionAnimator(0.11, 0.95, TimeSpan.FromMilliseconds(300), action);
                    await animator.RunAsync();
                }
                UpdateOpacity(Opacity);

                if (PopAfter > default(TimeSpan))
                {
                    P42.Utils.Timer.StartTimer(PopAfter, async () =>
                    {
                        await PopAsync(PopupPoppedCause.Timeout, animated, "Timeout");
                        return false;
                    });
                }


                PushPopState = PushPopState.Pushed;
                await OnPushEndAsync();
                Pushed?.Invoke(this, EventArgs.Empty);
                _pushCompletionSource?.TrySetResult(true);
            }
            catch (Exception)
            {
                await InnerPop(PopupPoppedCause.Exception, animated);
            }
        }

        /// <summary>
        /// Pop (un-display) popup
        /// </summary>
        /// <param name="cause"></param>
        /// <param name="animated"></param>
        /// <param name="trigger"></param>
        /// <returns></returns>
        public virtual async Task PopAsync(PopupPoppedCause cause = PopupPoppedCause.MethodCalled, bool animated = false, [CallerMemberName] object trigger = null)
        {
            if (PushPopState == PushPopState.Popping || PushPopState == PushPopState.Popped)
                return;
            
            if (PushPopState == PushPopState.Pushing)
                await WaitForPushAsync();

            await InnerPop(cause, animated, trigger);
        }

        async Task InnerPop(PopupPoppedCause cause, bool animated = false, [CallerMemberName] object trigger = null)
        {
            _pushCompletionSource?.TrySetResult(false);
            _pushCompletionSource = null;

            PushPopState = PushPopState.Popping;
            await OnPopBeginAsync();

            PoppedCause = cause;
            PoppedTrigger = trigger;


            if (animated)
            {
                void action(double percent) => UpdateOpacity(Opacity * percent);
                var animator = new P42.Utils.Uno.ActionAnimator(0.95, 0.11, TimeSpan.FromMilliseconds(300), action);
                await animator.RunAsync();
            }

            CompletePop(PoppedCause, PoppedTrigger);
            await OnPopEndAsync();
        }

        void CompletePop(PopupPoppedCause poppedCause, object poppedTrigger)
        {
            UpdateOpacity(0.001);
            Popups.Remove(this);

            PushPopState = PushPopState.Popped;
            var result = new PopupPoppedEventArgs(poppedCause, poppedTrigger);
            Popped?.Invoke(this, result);
            _popCompletionSource?.TrySetResult(result);
        }

        TaskCompletionSource<PopupPoppedEventArgs> _popCompletionSource;
        /// <summary>
        /// Wait for popup to be popped
        /// </summary>
        /// <returns></returns>
        public async Task<PopupPoppedEventArgs> WaitForPoppedAsync()
        {
            _popCompletionSource ??= new TaskCompletionSource<PopupPoppedEventArgs>();
            return await _popCompletionSource.Task;
        }

        TaskCompletionSource<bool> _pushCompletionSource;
        /// <summary>
        /// Wait for popup to be pushed
        /// </summary>
        /// <returns></returns>
        async Task WaitForPushAsync()
        {
            _pushCompletionSource ??= new TaskCompletionSource<bool>();
            await _pushCompletionSource.Task;
        }
        #endregion


        #region Protected Push / Pop Methods
        /// <summary>
        /// Invoked at start on appearing animation
        /// </summary>
        /// <returns></returns>
        protected virtual async Task OnPushBeginAsync()
        {
            await Task.CompletedTask;
        }

        /// <summary>
        /// Invoked at end of appearing animation
        /// </summary>
        /// <returns></returns>
        protected virtual async Task OnPushEndAsync()
        {
            await Task.CompletedTask;
        }

        /// <summary>
        /// Invoked at start of disappearing animation
        /// </summary>
        /// <returns></returns>
        protected virtual async Task OnPopBeginAsync()
        {
            await Task.CompletedTask;
        }

        /// <summary>
        /// Invoked at end of disappearing animation
        /// </summary>
        /// <returns></returns>
        protected virtual async Task OnPopEndAsync()
        {
            await Task.CompletedTask;
        }
        #endregion


        #region Layout
        void UpdateOpacity(double value = -1)
        {
            if (value < 0)
                value = Opacity;

            ContentBorder.Opacity = ShadowBorder.Opacity = PageOverlay.Opacity = value;
        }

        internal class AlignmentMarginsAndPointer
        {
            public HorizontalAlignment HorizontalAlignment { get; private set; }
            public VerticalAlignment VerticalAlignment { get; private set; }    
            public Thickness Margin { get; private set; }
            public PointerDirection PointerDirection { get; private set; }
            public double PointerAxialPosition { get; private set; } = 0.5;

            public Size Size { get; private set; }

            public AlignmentMarginsAndPointer(Size size, HorizontalAlignment hz, VerticalAlignment vt, Thickness margin, PointerDirection pointerDirection, double axialPosition = 0.5)
            {
                Size = size;
                HorizontalAlignment = hz;
                VerticalAlignment = vt;
                Margin = margin;
                PointerDirection = pointerDirection;
                HorizontalAlignment = hz;
                PointerAxialPosition = axialPosition;
            }
        }

        void SetAlignmentMarginsAndPointer(AlignmentMarginsAndPointer value)
        {
            ContentBorder.HorizontalAlignment = ShadowBorder.HorizontalAlignment = value.HorizontalAlignment;
            ContentBorder.VerticalAlignment = ShadowBorder.VerticalAlignment = value.VerticalAlignment;
            ShadowBorder.CornerRadius = CornerRadius;
            ShadowBorder.Margin = value.Margin.Subtract(ShadowBorder.BlurSigma * 2);
            ContentBorder.Margin = value.Margin;
            ActualPointerDirection = ContentBorder.PointerDirection = value.PointerDirection;
            ContentBorder.PointerAxialPosition = value.PointerAxialPosition;
            ShadowBorder.PointerAxialPosition = ContentBorder.PointerAxialPosition + ShadowBorder.BlurSigma * 2;
            //ContentBorder.InvalidateMeasure();

            System.Diagnostics.Debug.WriteLine("TargetedPopup.UpdateMarginAndAlignment vtAlign:" + value.VerticalAlignment + " hzAlign:" + value.HorizontalAlignment);
            System.Diagnostics.Debug.WriteLine("TargetedPopup.UpdateMarginAndAlignment margin:" + value.Margin + " shadowMargin:" + ShadowBorder.Margin);

#if __ANDROID__
            ContentBorder.Invalidate();
            ContentBorder.InvalidateArrange();
            ContentBorder.InvalidateMeasure();
            //ContentBorder.InvalidateDrawable();
            ContentBorder.InvalidateOutline();
#endif
        }


        void UpdateMarginAndAlignment()
        {
            if (PushPopState == PushPopState.Popped || PushPopState == PushPopState.Popping)
                return;

            SetAlignmentMarginsAndPointer(GetAlignmentMarginsAndPointerMeasurements());
        }

        internal AlignmentMarginsAndPointer GetAlignmentMarginsAndPointerMeasurements(UIElement content = null)
        {

            var windowSize = AppWindow.Size(this);
            if (windowSize.Width < 1 || windowSize.Height < 1)
                return null;


            var safeMargin = AppWindow.SafeMargin(this);
            System.Diagnostics.Debug.WriteLine($"TargetedPopup.UpdateMarginAndAlignment windowSize:[{windowSize}] Margin:[{Margin}] safeMargin:[{safeMargin}]");

            var windowWidth = windowSize.Width - Margin.Horizontal() - safeMargin.Horizontal();
            var windowHeight = windowSize.Height - Margin.Vertical() - safeMargin.Vertical();
            System.Diagnostics.Debug.WriteLine($"TargetedPopup.UpdateMarginAndAlignment windowWidth:[{windowWidth}] windowHeight:[{windowHeight}]");


            var cleanSize = MeasureCleanBorder(new Size(windowWidth, windowHeight), content: content);
            System.Diagnostics.Debug.WriteLine($"TargetedPopup.UpdateMarginAndAlignment cleanSize:[{cleanSize}] : [{windowWidth},{windowHeight}]");

            if (PreferredPointerDirection == PointerDirection.None || Target is null)
                return new AlignmentMarginsAndPointer(cleanSize, HorizontalAlignment, VerticalAlignment, Margin.Add(safeMargin), PointerDirection.None);

            var targetBounds = TargetBounds();
#if __ANDROID__
            if (Target != null)
            {
                var shift = AppWindow.StatusBarHeight();
                targetBounds = new Rect(targetBounds.Left, targetBounds.Top - shift, targetBounds.Width, targetBounds.Height);
            }
#endif


            System.Diagnostics.Debug.WriteLine(GetType() + ".UpdateBorderMarginAndAlignment targetBounds:["+targetBounds+"]");
            var availableSpace = AvailableSpace(targetBounds.Grow(PointerMargin), safeMargin);
            //System.Diagnostics.Debug.WriteLine($"TargetedPopup.UpdateMarginAndAlignment availableSpace:[{availableSpace}]");
            var stats = BestFit(availableSpace, cleanSize, safeMargin);
            //System.Diagnostics.Debug.WriteLine($"TargetedPopup.UpdateMarginAndAlignment stats:[{stats}]");

            if (stats.PointerDirection == PointerDirection.None)
                return new AlignmentMarginsAndPointer(cleanSize, HorizontalAlignment.Center, VerticalAlignment.Center, Margin.Add(safeMargin), PointerDirection.None);

            var actualPointerDirection = stats.PointerDirection;
            var axialPosition = 0.5;
            var margin = Margin.Add(safeMargin);
            var hzAlign = HorizontalAlignment;
            var vtAlign = VerticalAlignment;

            if (stats.PointerDirection.IsHorizontal())
            {
                if (stats.PointerDirection == PointerDirection.Left)
                {
                    margin.Left = targetBounds.Right + PointerMargin;
                    if (HorizontalAlignment != HorizontalAlignment.Stretch)
                        hzAlign = HorizontalAlignment.Left;
                }
                else if (stats.PointerDirection == PointerDirection.Right)
                {
                    margin.Right = (windowSize.Width - targetBounds.Left) + PointerMargin;
                    if (HorizontalAlignment != HorizontalAlignment.Stretch)
                        hzAlign = HorizontalAlignment.Right;
                }

                if (VerticalAlignment == VerticalAlignment.Top)
                    margin.Top = Math.Max(Margin.Top, targetBounds.Top);
                else if (VerticalAlignment == VerticalAlignment.Center)
                {
                    margin.Top = Math.Max(Margin.Top, (targetBounds.Top + targetBounds.Bottom) / 2.0 - stats.BorderSize.Height / 2.0);
                    vtAlign = VerticalAlignment.Top;
                }
                else if (VerticalAlignment == VerticalAlignment.Bottom)
                    margin.Bottom = Math.Max(Margin.Bottom, windowSize.Height - targetBounds.Bottom);

                if (margin.Top + stats.BorderSize.Height > windowSize.Height - Margin.Bottom)
                    margin.Top = windowSize.Height - Margin.Bottom - stats.BorderSize.Height;

                var shortest = Math.Min(stats.BorderSize.Height, Target.ActualSize.Y);
                //System.Diagnostics.Debug.WriteLine($"TargetedPopup.SetAlignmentAndMargins : stats.Border.Height:[{stats.BorderSize.Height}] target.Height:[{Target.ActualSize.Y}] shortest [{shortest}]");
                //System.Diagnostics.Debug.WriteLine($"TargetedPopup.SetAlignmentAndMargins : PointerBias:[{PointerBias}]");
                if (VerticalAlignment == VerticalAlignment.Top)
                    axialPosition = shortest * PointerBias;
                else if (VerticalAlignment == VerticalAlignment.Bottom)
                    axialPosition = Math.Max(0, stats.BorderSize.Height - shortest * PointerBias);
                else if (targetBounds.Height < stats.BorderSize.Height)
                    axialPosition = Math.Max(0, (targetBounds.Top - margin.Top) + targetBounds.Height * PointerBias);
                else
                    axialPosition = stats.BorderSize.Height * PointerBias;

                //System.Diagnostics.Debug.WriteLine($"TargetedPopup.SetAlignmentAndMargins : PointerAxialPosition:[{axialPosition}]");
            }
            else
            {
                if (stats.PointerDirection == PointerDirection.Up)
                {
                    margin.Top = targetBounds.Bottom + PointerMargin;
                    if (VerticalAlignment != VerticalAlignment.Stretch)
                        vtAlign = VerticalAlignment.Top;
                }
                else if (stats.PointerDirection == PointerDirection.Down)
                {
                    margin.Bottom = (windowSize.Height - targetBounds.Top) + PointerMargin;
                    if (VerticalAlignment != VerticalAlignment.Stretch)
                        vtAlign = VerticalAlignment.Bottom;
                }
                //System.Diagnostics.Debug.WriteLine("TargetedPopup.UpdateMarginAndAlignment margin: " + margin);

                if (HorizontalAlignment == HorizontalAlignment.Left)
                    margin.Left = Math.Max(Margin.Left, targetBounds.Left);
                else if (HorizontalAlignment == HorizontalAlignment.Center)
                {
                    margin.Left = Math.Max(Margin.Left, (targetBounds.Left + targetBounds.Right) / 2.0 - stats.BorderSize.Width / 2.0);
                    hzAlign = HorizontalAlignment.Left;
                }
                else if (HorizontalAlignment == HorizontalAlignment.Right)
                    margin.Right = Math.Max(Margin.Right, windowSize.Width - targetBounds.Right);

                if (margin.Left + stats.BorderSize.Width > windowSize.Width - Margin.Right)
                    margin.Left = windowSize.Width - Margin.Right - stats.BorderSize.Width;

                var shortest = Math.Min(stats.BorderSize.Width, Target.ActualSize.X);
                if (HorizontalAlignment == HorizontalAlignment.Left)
                    axialPosition = shortest * PointerBias;
                else if (HorizontalAlignment == HorizontalAlignment.Right)
                    axialPosition = Math.Max(0, stats.BorderSize.Width - shortest * PointerBias);
                else if (targetBounds.Width < stats.BorderSize.Width)
                    axialPosition = Math.Max(0,(targetBounds.Left - margin.Left) + targetBounds.Width * PointerBias);
                else
                    axialPosition = stats.BorderSize.Width * PointerBias;
            }

            return new AlignmentMarginsAndPointer(stats.BorderSize, hzAlign, vtAlign, margin, actualPointerDirection, axialPosition);

        }


        DirectionStats BestFit(Thickness availableSpace, Size cleanSize, Thickness safeMargin)
        {
            // given the amount of free space, determine if the borderSize will fit 
            var windowSize = AppWindow.Size(this);
            var windowSpaceW = Math.Max(0, windowSize.Width - Margin.Horizontal() - safeMargin.Horizontal());
            var windowSpaceH = Math.Max(0, windowSize.Height - Margin.Vertical() - safeMargin.Vertical());
            var windowSpace = new Size(windowSpaceW, windowSpaceH);

            var freeSpaceW = Math.Max(0, windowSpace.Width - cleanSize.Width);
            var freeSpaceH = Math.Max(0, windowSpace.Height - cleanSize.Height);

            var cleanStat = new DirectionStats
            {
                PointerDirection = PointerDirection.None,
                BorderSize = cleanSize,
                FreeSpace = new Size(freeSpaceW, freeSpaceH)
            };

            #region Check if clean border fits in preferred pointer quadrants
            // see if the existing measurement data works
            if (GetBestDirectionStat(GetRectangleBorderStatsForDirection(PreferredPointerDirection, cleanStat, availableSpace)) is DirectionStats stats0)
                return stats0;
            #endregion

            #region Check if border + content could fit in any of the preferred pointer quadrants
            // at this point in time valid and invalid fits are in the stats list
            if (GetBestDirectionStat(GetMeasuredStatsForDirection(PreferredPointerDirection, cleanStat, availableSpace, windowSpace)) is DirectionStats stats1)
                return stats1;
            #endregion

            // the stats list only contains invalid fallback fits ... but perhaps not all fallback fits have yet been tried
            var uncheckedFallbackPointerDirection = (FallbackPointerDirection ^ PreferredPointerDirection) | FallbackPointerDirection;

            #region Check if clean border fits in unchecked fallback pointer quadrants
            if (GetBestDirectionStat(GetRectangleBorderStatsForDirection(uncheckedFallbackPointerDirection, cleanStat, availableSpace)) is DirectionStats stats2)
                return stats2;
            #endregion

            #region Check if border + content could fit in any of the unchecked fallback pointer quadrants
            if (GetBestDirectionStat(GetMeasuredStatsForDirection(uncheckedFallbackPointerDirection, cleanStat, availableSpace, windowSpace)) is DirectionStats stats3)
                return stats3;
            #endregion

            return cleanStat;
        }

        static DirectionStats? GetBestDirectionStat(List<DirectionStats> stats)
        {
            if (stats.Count == 1)
                return stats[0];
            if (stats.Count > 1)
            {
                var max = stats[0];
                foreach (var s in stats)
                {
                    if (s.MinAvail == max.MinAvail && s.MaxAvail > max.MaxAvail)
                        max = s;
                    else if (s.MinAvail > max.MinAvail)
                        max = s;
                }
                return max;
            }
            return null;
        }

        Rect TargetBounds()
        {
            var targetBounds = Target is null ? Rect.Empty : Target.GetBounds();

            double targetLeft = (Target is null ? TargetRect.Left : targetBounds.Left);
            double targetRight = (Target is null ? TargetRect.Right : targetBounds.Right);
            double targetTop = (Target is null ? TargetRect.Top : targetBounds.Top);
            double targetBottom = (Target is null ? TargetRect.Bottom: targetBounds.Bottom);

            return new Rect(targetLeft, targetTop, targetRight - targetLeft, targetBottom - targetTop);
        }

        Thickness AvailableSpace(Rect target, Thickness safeMargin)
        {
            var windowBounds = AppWindow.Size(this);
            if (Target != null || (TargetRect.Width > 0 || TargetRect.Height > 0))
            {
                if (target.Right > 0 && target.Left < windowBounds.Width && target.Bottom > 0 && target.Top < windowBounds.Height)
                {
                    var availL = target.Left - Margin.Left - safeMargin.Left;
                    var availR = windowBounds.Width - Margin.Right - safeMargin.Right - target.Right;
                    var availT = target.Top - Margin.Top - safeMargin.Top;
                    var availB = windowBounds.Height - target.Bottom - Margin.Bottom - safeMargin.Bottom;

                    var maxWidth = MaxWidth;
                    if (Width > 0 && Width < maxWidth)
                        maxWidth = Width;
                    if (maxWidth > 0 && HorizontalAlignment != HorizontalAlignment.Stretch)
                    {
                        availL = Math.Min(availL, maxWidth);
                        availR = Math.Min(availR, maxWidth);
                    }

                    var maxHeight = MaxHeight;
                    if (Height > 0 && Height < maxHeight)
                        maxHeight = Height;
                    if (maxHeight > 0 && VerticalAlignment != VerticalAlignment.Stretch)
                    {
                        availT = Math.Min(availT, maxHeight);
                        availB = Math.Min(availB, maxHeight);
                    }

                    return new Thickness(availL, availT, availR, availB);
                }
            }

            if (PointToOffScreenElements)
                return new Thickness(windowBounds.Width - Margin.Horizontal() - safeMargin.Horizontal(), windowBounds.Height - Margin.Vertical() - safeMargin.Vertical(), windowBounds.Width - Margin.Horizontal() - safeMargin.Horizontal(), windowBounds.Height - Margin.Vertical() - safeMargin.Vertical());

            return new Thickness(-1, -1, -1, -1);
        }

        List<DirectionStats> GetRectangleBorderStatsForDirection(PointerDirection pointerDirection, DirectionStats cleanStat, Thickness availableSpace)
        {
            //System.Diagnostics.Debug.WriteLine(GetType() + ".GetRectangleBorderStatsForDirection cleanStat:["+cleanStat+"]");
            var stats = new List<DirectionStats>();
            if (pointerDirection.LeftAllowed() && (availableSpace.Right - cleanStat.BorderSize.Width) >= PointerLength)
            {
                var stat = cleanStat;
                stat.PointerDirection = PointerDirection.Left;
                stat.BorderSize.Width += PointerLength;
                var free = availableSpace.Right - stat.BorderSize.Width;
                if (free >= 0)
                {
                    stat.FreeSpace.Width = free;
                    stats.Add(stat);
                }
            }

            if (pointerDirection.RightAllowed() && (availableSpace.Left - cleanStat.BorderSize.Width) >= PointerLength)
            {
                var stat = cleanStat;
                stat.PointerDirection = PointerDirection.Right;
                stat.BorderSize.Width += PointerLength;
                var free = availableSpace.Left - stat.BorderSize.Width;
                if (free >= 0)
                {
                    stat.FreeSpace.Width = free;
                    stats.Add(stat);
                }
            }

            if (pointerDirection.UpAllowed() && (availableSpace.Bottom - cleanStat.BorderSize.Height) >= PointerLength)
            {
                var stat = cleanStat;
                stat.PointerDirection = PointerDirection.Up;
                stat.BorderSize.Height += PointerLength;
                var free = availableSpace.Bottom - stat.BorderSize.Height;
                if (free >= 0)
                {
                    stat.FreeSpace.Height = free;
                    stats.Add(stat);
                }
            }

            if (pointerDirection.DownAllowed() && (availableSpace.Top - cleanStat.BorderSize.Height) >= PointerLength)
            {
                var stat = cleanStat;
                stat.PointerDirection = PointerDirection.Down;
                stat.BorderSize.Height += PointerLength;
                var free = availableSpace.Top - stat.BorderSize.Height;
                if (free >= 0)
                {
                    stat.FreeSpace.Height = free;
                    stats.Add(stat);
                }
            }

            return stats;
        }

        List<DirectionStats> GetMeasuredStatsForDirection(PointerDirection pointerDirection, DirectionStats cleanStat, Thickness availableSpace, Size windowSpace)
        {
            var stats = new List<DirectionStats>();
            if (pointerDirection.LeftAllowed())
            {
                if (availableSpace.Right > 0)
                {
                    var size = new Size(availableSpace.Right, windowSpace.Height);
                    var borderSize = MeasureCleanBorder(size, cleanStat.BorderSize);
                    var stat = cleanStat;
                    stat.PointerDirection = PointerDirection.Left;
                    stat.BorderSize = borderSize;
                    var free = availableSpace.Right - borderSize.Width;
                    if (free >= 0)
                    {
                        stat.FreeSpace.Width = free;
                        stats.Add(stat);
                    }
                }
            }

            if (pointerDirection.RightAllowed())
            {
                if (availableSpace.Left > 0)
                {
                    var size = new Size(availableSpace.Left, windowSpace.Height);
                    var borderSize = MeasureCleanBorder(size, cleanStat.BorderSize);
                    var stat = cleanStat;
                    stat.PointerDirection = PointerDirection.Right;
                    stat.BorderSize = borderSize;
                    var free = availableSpace.Left - borderSize.Width;
                    if (free >= 0)
                    {
                        stat.FreeSpace.Width = free;
                        stats.Add(stat);
                    }
                }
            }

            if (pointerDirection.UpAllowed())
            {
                if (availableSpace.Bottom > 0)
                {
                    var size = new Size(windowSpace.Width, availableSpace.Bottom);
                    var borderSize = MeasureCleanBorder(size, cleanStat.BorderSize);
                    var stat = cleanStat;
                    stat.PointerDirection = PointerDirection.Up;
                    stat.BorderSize = borderSize;
                    var free = availableSpace.Bottom - borderSize.Height;
                    if (free >= 0)
                    {
                        stat.FreeSpace.Height = free;
                        stats.Add(stat);
                    }
                }
            }

            if (pointerDirection.DownAllowed())
            {
                if (availableSpace.Top > 0)
                {
                    var size = new Size(windowSpace.Width, availableSpace.Top);
                    var borderSize = MeasureCleanBorder(size, cleanStat.BorderSize);
                    var stat = cleanStat;
                    stat.PointerDirection = PointerDirection.Down;
                    stat.BorderSize = borderSize;
                    var free = availableSpace.Top - borderSize.Height;
                    if (free >= 0)
                    {
                        stat.FreeSpace.Height = free;
                        stats.Add(stat);
                    }
                }
            }

            return stats;
        }

        Size MeasureCleanBorder(Size available, Size failSize = default, UIElement content = null)
        {
            //System.Diagnostics.Debug.WriteLine("\n");
            //System.Diagnostics.Debug.WriteLine($"TargetedPopup.MeasureCleanBorder({available})");

            var width = available.Width;
            var height = available.Height;
            if (this.HasPrescribedWidth())
            {
                System.Diagnostics.Debug.WriteLine($"TargetedPopup.MeasureCleanBorder HasPrescribedWidth");
                //width = Math.Min(Width, width);
                width = Width;
            }
            if (this.HasPrescribedHeight())
            {
                System.Diagnostics.Debug.WriteLine($"TargetedPopup.MeasureCleanBorder HasPrescribedHeight");
                //height = Math.Min(Height, height);
                height = Height;
            }

            if (this.HasPrescribedWidth() && this.HasPrescribedHeight())
                return new Size(width, height);

            if (this.HasMinWidth())
                width = Math.Max(width, MinWidth);
            if (this.HasMinHeight())
                height = Math.Max(height, MinHeight);

            if (this.HasMaxWidth())
                width = Math.Min(width, MaxWidth);
            if (this.HasMaxHeight())
                height = Math.Min(height, MaxHeight);

            //System.Diagnostics.Debug.WriteLine($"TargetedPopup.MeasureCleanBorder width[{width}][{height}]");

            if (Content is null)
            {
                System.Diagnostics.Debug.WriteLine("TargetedPopup.MeasureCleanBorder : IS EMPTY ");
                return new Size(width, height);
            }

            if (HorizontalAlignment == HorizontalAlignment.Stretch && VerticalAlignment == VerticalAlignment.Stretch)
            {
                System.Diagnostics.Debug.WriteLine("TargetedPopup.MeasureCleanBorder : STRETCH ");
                return new Size(width, height);
            }

            var border = BorderWidth * (HasBorder ? 1 : 0) * 2;
            var availableWidth = width - (Padding.Horizontal() + border + 1);
            var availableHeight = height - (Padding.Vertical() + border + 1);
            //System.Diagnostics.Debug.WriteLine($"TargetedPopup.MeasureCleanBorder borderSize:[{borderSize}] Padding:[{Padding}]  availableWidth:[" + availableWidth+"] availableHeight:["+availableHeight+"]");
            if (availableWidth > 0 && availableHeight > 0)
            {
                content ??= ContentBorder.Content as UIElement;
                content ??= ContentBorder._contentPresenter;
                content.Measure(new Size(availableWidth, availableHeight));
                var result = content.DesiredSize;
                //System.Diagnostics.Debug.WriteLine("TargetedPopup.MeasureCleanBorder  _contentPresenter.RenderSize:[" + content.RenderSize + "]");
                //System.Diagnostics.Debug.WriteLine("TargetedPopup.MeasureCleanBorder  _contentPresenter.DesiredSize:[" + content.DesiredSize + "]");
                //System.Diagnostics.Debug.WriteLine("TargetedPopup.MeasureCleanBorder  _contentPresenter.ActualSize:[" + content.ActualSize + "]");

                result.Width += Padding.Horizontal() + border + 1;
                result.Height += Padding.Vertical() + border + 1;

                var resultSize = new Size(
                    this.HasPrescribedWidth()
                        ? width : result.Width,
                    this.HasPrescribedHeight()
                        ? height : result.Height
                    );

                return resultSize;
            }

            if (failSize == default)
                return new Size(width,height);

            return failSize;
        }


#endregion

    }
}
