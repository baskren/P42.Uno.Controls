using P42.Uno.Markup;
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

namespace P42.Uno.Controls
{
    partial class PopupBase : UserControl
    {

        #region Properties



        #region PopAfter Property
        /// <summary>
        /// PopupAfter backing store
        /// </summary>
        public static readonly DependencyProperty PopAfterProperty = DependencyProperty.Register(
            nameof(PopAfter),
            typeof(TimeSpan),
            typeof(PopupBase),
            new PropertyMetadata(default(TimeSpan))
        );
        /// <summary>
        /// TimeSpan before popup automatically disappears (default value = no automatic dissappear)
        /// </summary>
        public TimeSpan PopAfter
        {
            get => (TimeSpan)GetValue(PopAfterProperty);
            set => SetValue(PopAfterProperty, value);
        }
        #endregion PopAfter Property

        #region IsAnimated Property
        /// <summary>
        /// IsAnimated backing store
        /// </summary>
        public static readonly DependencyProperty IsAnimatedProperty = DependencyProperty.Register(
            nameof(IsAnimated),
            typeof(bool),
            typeof(PopupBase),
            new PropertyMetadata(default(bool))
        );
        /// <summary>
        /// Is the pop and push animated?
        /// </summary>
        public bool IsAnimated
        {
            get => (bool)GetValue(IsAnimatedProperty);
            set => SetValue(IsAnimatedProperty, value);
        }
        #endregion IsAnimated Property

        #region Margin Property
        /// <summary>
        /// Margin property backing store
        /// </summary>
        public static readonly new DependencyProperty MarginProperty = DependencyProperty.Register(
            nameof(Margin),
            typeof(Thickness),
            typeof(PopupBase),
            new PropertyMetadata(default(Thickness))
        );
        /// <summary>
        /// What is the margin for the popup (not the page overlay)
        /// </summary>
        public new Thickness Margin
        {
            get => (Thickness)GetValue(MarginProperty);
            set => SetValue(MarginProperty, value);
        }
        #endregion Margin Property

        #region Target Property
        /// <summary>
        /// Target property backing store
        /// </summary>
        public static readonly DependencyProperty TargetProperty = DependencyProperty.Register(
            nameof(Target),
            typeof(UIElement),
            typeof(PopupBase),
            new PropertyMetadata(default(UIElement))
        );
        /// <summary>
        /// What is the UIElement (if any) that the popup will point at?
        /// </summary>
        public UIElement Target
        {
            get => (UIElement)GetValue(TargetProperty);
            set => SetValue(TargetProperty, value);
        }
        #endregion Target Property







        #region PageOverlayColor Property
        /// <summary>
        /// PageOverlayColor property backing store
        /// </summary>
        public static readonly DependencyProperty PageOverlayColorProperty = DependencyProperty.Register(
            nameof(PageOverlayColor),
            typeof(Color),
            typeof(PopupBase),
            new PropertyMetadata(Colors.Black.WithAlpha(64))
        );
        /// <summary>
        /// What color is the page overlay?
        /// </summary>
        public Color PageOverlayColor
        {
            get => (Color)GetValue(PageOverlayColorProperty);
            set => SetValue(PageOverlayColorProperty, value);
        }
        #endregion PageOverlayColor Property

        #region CancelOnPageOverlayTouch Property
        /// <summary>
        /// CancelOnPageOverlayTouch backing store
        /// </summary>
        public static readonly DependencyProperty CancelOnPageOverlayTouchProperty = DependencyProperty.Register(
            nameof(CancelOnPageOverlayTouch),
            typeof(bool),
            typeof(PopupBase),
            new PropertyMetadata(true)
        );
        /// <summary>
        /// Will the popup pop upon a PageOverylay touch?
        /// </summary>
        public bool CancelOnPageOverlayTouch
        {
            get => (bool)GetValue(CancelOnPageOverlayTouchProperty);
            set => SetValue(CancelOnPageOverlayTouchProperty, value);
        }
        #endregion CancelOnPageOverlayTouch Property

        #region CancelOnBackButtonClick Property
        /// <summary>
        /// CancelOnBackButtonClick property backing store
        /// </summary>
        public static readonly DependencyProperty CancelOnBackButtonClickProperty = DependencyProperty.Register(
            nameof(CancelOnBackButtonClick),
            typeof(bool),
            typeof(PopupBase),
            new PropertyMetadata(true)
        );
        /// <summary>
        /// Will the popup pop upon a mobile device [BACK] button click?
        /// </summary>
        public bool CancelOnBackButtonClick
        {
            get => (bool)GetValue(CancelOnBackButtonClickProperty);
            set => SetValue(CancelOnBackButtonClickProperty, value);
        }
        #endregion CancelOnBackButtonClick Property

        #region Parameter Property
        /// <summary>
        /// Parameter property backing store
        /// </summary>
        public static readonly DependencyProperty ParameterProperty = DependencyProperty.Register(
            nameof(Parameter),
            typeof(object),
            typeof(PopupBase),
            new PropertyMetadata(default(object))
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
            typeof(PopupBase),
            new PropertyMetadata(default(Effect))
        );
        public Effect PushEffect
        {
            get => (Effect)GetValue(PushEffectProperty);
            set => SetValue(PushEffectProperty, value);
        }
        #endregion PushEffect Property

        #region HasShadow Property
        public static readonly DependencyProperty HasShadowProperty = DependencyProperty.Register(
            nameof(HasShadow),
            typeof(bool),
            typeof(PopupBase),
            new PropertyMetadata(default(bool))
        );
        public bool HasShadow
        {
            get => (bool)GetValue(HasShadowProperty);
            set => SetValue(HasShadowProperty, value);
        }
        #endregion HasShadow Property





        #endregion


        #region Events
        public event EventHandler Pushed;

        /// <summary>
        /// Occurs when popup has been cancelled.
        /// </summary>
        public event EventHandler<PopupPoppedEventArgs> Cancelled;

        /// <summary>
        /// Occurs when popup has popped;
        /// </summary>
        public event EventHandler<PopupPoppedEventArgs> Popped;

        /// <summary>
        /// Occurs when popup appearing animation has started
        /// </summary>
        public event EventHandler AppearingAnimationBegin;

        /// <summary>
        /// Occurs when popup appearing animation has ended
        /// </summary>
        public event EventHandler AppearingAnimationEnd;

        /// <summary>
        /// occurs when popup disappearing animation has started
        /// </summary>
        public event EventHandler DisappearingAnimationBegin;

        /// <summary>
        /// Occurs when popup disappearing animation has ended
        /// </summary>
        public event EventHandler DisappearingAnimationEnd;

        #endregion


        #region Construction

        #endregion



    }
}
