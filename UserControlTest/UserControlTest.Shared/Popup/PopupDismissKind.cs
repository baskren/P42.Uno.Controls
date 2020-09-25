// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace UserControlTest
{
    /// <summary>
    /// Enumeration to describe how an Popup was dismissed
    /// </summary>
    public enum PopupDismissKind
    {
        /// <summary>
        /// When the system dismissed the notification.
        /// </summary>
        Programmatic,

        /// <summary>
        /// When user explicitly dismissed the notification.
        /// </summary>
        User,

        /// <summary>
        /// When the system dismissed the notification after timeout.
        /// </summary>
        Timeout
    }
}
