﻿using System;
namespace P42.Uno.Controls
{
    /// <summary>
    /// State of permission popup permission process
    /// </summary>
    public enum PermissionState
    {
        /// <summary>
        /// Waiting for interaction with permission popup
        /// </summary>
        Pending,
        /// <summary>
        /// Permission was granted
        /// </summary>
        Ok,
        /// <summary>
        /// Permission popup was cancelled
        /// </summary>
        Cancelled,
        /// <summary>
        /// Permission was rejected
        /// </summary>
        Rejected,
    }
}
