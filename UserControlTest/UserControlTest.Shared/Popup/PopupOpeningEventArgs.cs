// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

namespace UserControlTest
{
    /// <summary>
    /// A delegate for <see cref="Popup"/> opening.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event arguments.</param>
    public delegate void PopupOpeningEventHandler(object sender, PopupOpeningEventArgs e);

    /// <summary>
    /// Provides data for the <see cref="Popup"/> Dismissing event.
    /// </summary>
    public class PopupOpeningEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PopupOpeningEventArgs"/> class.
        /// </summary>
        public PopupOpeningEventArgs()
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether the notification should be opened.
        /// </summary>
        public bool Cancel { get; set; }
    }
}
