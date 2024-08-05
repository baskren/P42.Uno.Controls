namespace P42.Uno.Controls;


public static class PopupExtensions
{
    public static void DisableAlternativeCancel(this TargetedPopup popup, bool value = true)
    {
        popup.PopOnBackButtonClick = !value;
        popup.PopOnBackButtonClick = !value;
        popup.PopOnPointerMove = !value;
    }
}
