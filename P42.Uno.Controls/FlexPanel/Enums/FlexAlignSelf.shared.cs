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

using System.ComponentModel;

namespace P42.Uno.Controls
{
    /// <summary>
    /// Values for <see cref="P:P42.Uno.Controls.FlexItem.AlignSelf" />.
    /// </summary>
    [TypeConverter(typeof(FlexAlignSelfTypeConverter))]
    public enum FlexAlignSelf
    {
        /// <summary>
        /// Whether an item should be packed according to the alignment value of its parent.
        /// </summary>
        Auto = 0,

        /// <summary>
        /// Whether an item's should be stretched out.
        /// </summary>
        Stretch = 1,

        /// <summary>
        /// Whether an item should be packed around the center.
        /// </summary>
        Center = 2,

        /// <summary>
        /// Whether an item should be packed at the start.
        /// </summary>
        Start = 3,

        /// <summary>
        /// Whether an item should be packed at the end.
        /// </summary>
        End = 4,
    }
}