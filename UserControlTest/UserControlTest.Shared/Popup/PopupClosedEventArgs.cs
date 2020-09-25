// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

namespace UserControlTest
{
    /// <summary>
    /// A delegate for <see cref="Popup"/> dismissing.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event arguments.</param>
    public delegate void PopupClosedEventHandler(object sender, PopupClosedEventArgs e);

    /// <summary>
    /// Provides data for the <see cref="Popup"/> Dismissing event.
    /// </summary>
    public class PopupClosedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PopupClosedEventArgs"/> class.
        /// </summary>
        /// <param name="dismissKind">Dismiss kind that triggered the closing event</param>
        public PopupClosedEventArgs(PopupDismissKind dismissKind)
        {
            DismissKind = dismissKind;
        }

        /// <summary>
        /// Gets the kind of action for the closing event.
        /// </summary>
        public PopupDismissKind DismissKind { get; private set; }
    }
}
