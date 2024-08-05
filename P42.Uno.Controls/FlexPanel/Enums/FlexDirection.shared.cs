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
    /// Values for <see cref="P:P42.Uno.Controls.FlexItem.Direction" />.
    /// </summary>
    [TypeConverter(typeof(FlexDirectionTypeConverter))]
    public enum FlexDirection
    {
        /// <summary>
        /// Whether items should be stacked horizontally.
        /// </summary>
        Row = 0,

        /// <summary>
        /// Like Row but in reverse order.
        /// </summary>
        RowReverse = 1,

        /// <summary>
        /// Whether items should be stacked vertically.
        /// </summary>
        Column = 2,

        /// <summary>
        /// Like Column but in reverse order.
        /// </summary>
        ColumnReverse = 3,
    }
}