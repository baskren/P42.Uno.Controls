// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See the LICENSE file in the project root
// for the license information.
//
// Author(s):
//  - Laurent Sansonetti (native Xamarin flex https://github.com/xamarin/flex)
//  - Stephane Delcroix (.NET port)
//  - Ben Askren (UWP/Uno port)
//
using System;
using System.ComponentModel;
using System.Globalization;

namespace P42.Uno.Controls
{
    /// <summary>
    /// Not implemented at this time
    /// Values for <see cref="P:P42.Uno.Controls.FlexItem.Position" />.
    /// </summary>
    internal enum FlexPosition
    {
        /// <summary>
        /// Whether the elements's frame will be determined by the flex rules of the layout system.
        /// </summary>
        Relative = 0,

        /// <summary>
        /// Whether the elements's frame will be determined by fixed position values (<see cref="P:P42.Uno.Controls.FlexItem.Left" />, <see cref="P:P42.Uno.Controls.FlexItem.Right" />, <see cref="P:P42.Uno.Controls.FlexItem.Top" /> and <see cref="P:P42.Uno.Controls.FlexItem.Bottom" />).
        /// </summary>
        Absolute = 1,
    }
}