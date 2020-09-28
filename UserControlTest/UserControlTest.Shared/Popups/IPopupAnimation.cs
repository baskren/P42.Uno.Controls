using System.Threading.Tasks;

namespace UserControlTest.Popups
{
    /// <summary>
    /// Popup animation interface
    /// </summary>
    public interface IPopupAnimation
    {
        /// <summary>
        /// Called before Popup appears 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="popup"></param>
        void Preparing(object content, PopupBase popup);
        /// <summary>
        /// Called after the Popup disappears
        /// </summary>
        /// <param name="content"></param>
        /// <param name="popup"></param>
        void Disposing(object content, PopupBase popup);
        /// <summary>
        /// Called when animating the popup's appearance
        /// </summary>
        /// <param name="content"></param>
        /// <param name="popup"></param>
        /// <returns></returns>
        Task Appearing(object content, PopupBase popup);
        /// <summary>
        /// Called when animating the popup's disappearance
        /// </summary>
        /// <param name="content"></param>
        /// <param name="popup"></param>
        /// <returns></returns>
        Task Disappearing(object content, PopupBase popup);
    }
}
