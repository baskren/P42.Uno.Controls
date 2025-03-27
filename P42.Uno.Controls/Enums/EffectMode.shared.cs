
namespace P42.Uno.Controls;

/// <summary>
/// Will an effect play?
/// </summary>
public enum EffectMode
{
    /// <summary>
    /// Will play depending on the device's settings
    /// </summary>
    Default,
    /// <summary>
    /// Will not play
    /// </summary>
    Off,
    /// <summary>
    /// Will play even if the device has been set otherwise
    /// </summary>
    On,

}
