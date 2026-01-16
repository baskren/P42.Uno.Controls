namespace P42.Uno.Controls;

/// <summary>
/// System Sound Effects
/// </summary>
public enum Effect
{
    /// <summary>
    /// I'll just sit here quietly.
    /// </summary>
    None,
    /// <summary>
    /// Give the system tap (press) response
    /// </summary>
    Press,
    /// <summary>
    /// Give the system selection response
    /// </summary>
    Select,
    /// <summary>
    /// Give the system return key response
    /// </summary>
    Modify,
    /// <summary>
    /// Give the system delete key response
    /// </summary>
    Delete,
    /// <summary>
    /// Notification Received! (Debug, Information)
    /// </summary>
    Info, // log levels: Debug, Information
    /// <summary>
    /// Inquiry
    /// </summary>
    Warning,
    /// <summary>
    /// Error 
    /// </summary>
    Error,
    /// <summary>
    /// Urgent Attention
    /// </summary>
    Alarm,
    /// <summary>
    /// Response requested (Permission)
    /// </summary>
    Inquiry,
    /// <summary>
    /// Progress made
    /// </summary>
    Progress
}


internal static partial class EffectExtensions
{
    extension(Effect effect)
    {
        public string? ChimeAssetAbsolutePath
            => effect == Effect.None ? null : AssetExtensions.AssetPath(effect.ChimeAssetRelativePath!);

        public string? ChimeAssetRelativePath
            => effect == Effect.None ? null : $"P42.Uno.Controls/Assets/Sounds/{effect}.mp3";

        internal Uri? ChimeAssetUri
        => effect == Effect.None ? null : new($"ms-appx:///{effect.ChimeAssetRelativePath}");
    }
}
