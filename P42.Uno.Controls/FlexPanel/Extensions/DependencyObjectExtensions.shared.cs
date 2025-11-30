// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See the LICENSE file in the project root
// for the license information.
//
// Author(s):
//  - Laurent Sansonetti (native Xamarin flex https://github.com/xamarin/flex)
//  - Stephane Delcroix (.NET port)
//  - Ben Askren (UWP/Uno port)
//

namespace P42.Uno.Controls.InternalFlexPanelExtensions;

internal static class DependencyObjectExtensions
{
    public static void SetNewValue(this DependencyObject obj, DependencyProperty dp, object value)
    {
        var currentValue = obj.GetValue(dp);
        if ((currentValue is null && !(value is null)) || !currentValue.Equals(value))
        {
            obj.SetValue(dp, value);
        }
    }
}