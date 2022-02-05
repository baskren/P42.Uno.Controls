// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
#if !NETSTANDARD1_4
using System.Runtime.InteropServices;
#endif
using System.Threading;

//#nullable enable

namespace Microsoft.Toolkit.Diagnostics
{
    /// <summary>
    /// Helper methods to efficiently throw exceptions.
    /// </summary>
    public static partial class ThrowHelper
    {
        public static void ThrowArrayTypeMismatchException(string message)
        {
            throw new ArrayTypeMismatchException(message);
        }

        public static void ThrowArrayTypeMismatchException(string message, Exception innerException)
        {
            throw new ArrayTypeMismatchException(message, innerException);
        }

        public static void ThrowArgumentException(string message)
        {
            throw new ArgumentException(message);
        }

        public static void ThrowArgumentException(string message, Exception innerException)
        {
            throw new ArgumentException(message, innerException);
        }

        public static void ThrowArgumentException(string name, string message)
        {
            throw new ArgumentException(message, name);
        }

        public static void ThrowArgumentException(string name, string message, Exception innerException)
        {
            throw new ArgumentException(message, name, innerException);
        }

        public static void ThrowArgumentNullException(string name)
        {
            throw new ArgumentNullException(name);
        }

        public static void ThrowArgumentNullException(string name, Exception innerException)
        {
            throw new ArgumentNullException(name, innerException);
        }

        public static void ThrowArgumentNullException(string name, string message)
        {
            throw new ArgumentNullException(name, message);
        }

        public static void ThrowArgumentOutOfRangeException(string name)
        {
            throw new ArgumentOutOfRangeException(name);
        }

        public static void ThrowArgumentOutOfRangeException(string name, Exception innerException)
        {
            throw new ArgumentOutOfRangeException(name, innerException);
        }

        public static void ThrowArgumentOutOfRangeException(string name, string message)
        {
            throw new ArgumentOutOfRangeException(name, message);
        }

        public static void ThrowArgumentOutOfRangeException(string name, object value, string message)
        {
            throw new ArgumentOutOfRangeException(name, value, message);
        }

#if !NETSTANDARD1_4
        public static void ThrowCOMException(string message)
        {
            throw new COMException(message);
        }

        public static void ThrowCOMException(string message, Exception innerException)
        {
            throw new COMException(message, innerException);
        }

        public static void ThrowCOMException(string message, int error)
        {
            throw new COMException(message, error);
        }

        public static void ThrowExternalException(string message)
        {
            throw new ExternalException(message);
        }

        public static void ThrowExternalException(string message, Exception innerException)
        {
            throw new ExternalException(message, innerException);
        }

        public static void ThrowExternalException(string message, int error)
        {
            throw new ExternalException(message, error);
        }
#endif

        public static void ThrowFormatException(string message)
        {
            throw new FormatException(message);
        }

        public static void ThrowFormatException(string message, Exception innerException)
        {
            throw new FormatException(message, innerException);
        }

#if !NETSTANDARD1_4
        public static void ThrowInsufficientMemoryException(string message)
        {
            throw new InsufficientMemoryException(message);
        }

        public static void ThrowInsufficientMemoryException(string message, Exception innerException)
        {
            throw new InsufficientMemoryException(message, innerException);
        }
#endif

        public static void ThrowInvalidDataException(string message)
        {
            throw new InvalidDataException(message);
        }

        public static void ThrowInvalidDataException(string message, Exception innerException)
        {
            throw new InvalidDataException(message, innerException);
        }

        public static void ThrowInvalidOperationException(string message)
        {
            throw new InvalidOperationException(message);
        }

        public static void ThrowInvalidOperationException(string message, Exception innerException)
        {
            throw new InvalidOperationException(message, innerException);
        }

        public static void ThrowLockRecursionException(string message)
        {
            throw new LockRecursionException(message);
        }

        public static void ThrowLockRecursionException(string message, Exception innerException)
        {
            throw new LockRecursionException(message, innerException);
        }

        public static void ThrowMissingFieldException(string message)
        {
            throw new MissingFieldException(message);
        }

        public static void ThrowMissingFieldException(string message, Exception innerException)
        {
            throw new MissingFieldException(message, innerException);
        }

#if !NETSTANDARD1_4
        public static void ThrowMissingFieldException(string className, string fieldName)
        {
            throw new MissingFieldException(className, fieldName);
        }
#endif

        public static void ThrowMissingMemberException(string message)
        {
            throw new MissingMemberException(message);
        }

        public static void ThrowMissingMemberException(string message, Exception innerException)
        {
            throw new MissingMemberException(message, innerException);
        }

#if !NETSTANDARD1_4
        public static void ThrowMissingMemberException(string className, string memberName)
        {
            throw new MissingMemberException(className, memberName);
        }
#endif

        public static void ThrowMissingMethodException(string message)
        {
            throw new MissingMethodException(message);
        }

        public static void ThrowMissingMethodException(string message, Exception innerException)
        {
            throw new MissingMethodException(message, innerException);
        }

#if !NETSTANDARD1_4
        public static void ThrowMissingMethodException(string className, string methodName)
        {
            throw new MissingMethodException(className, methodName);
        }
#endif

        public static void ThrowNotSupportedException(string message)
        {
            throw new NotSupportedException(message);
        }

        public static void ThrowNotSupportedException(string message, Exception innerException)
        {
            throw new NotSupportedException(message, innerException);
        }

        public static void ThrowObjectDisposedException(string objectName)
        {
            throw new ObjectDisposedException(objectName);
        }

        public static void ThrowObjectDisposedException(string objectName, Exception innerException)
        {
            throw new ObjectDisposedException(objectName, innerException);
        }

        public static void ThrowObjectDisposedException(string objectName, string message)
        {
            throw new ObjectDisposedException(objectName, message);
        }

        public static void ThrowOperationCanceledException(string message)
        {
            throw new OperationCanceledException(message);
        }

        public static void ThrowOperationCanceledException(string message, Exception innerException)
        {
            throw new OperationCanceledException(message, innerException);
        }

        public static void ThrowOperationCanceledException(CancellationToken token)
        {
            throw new OperationCanceledException(token);
        }

        public static void ThrowOperationCanceledException(string message, CancellationToken token)
        {
            throw new OperationCanceledException(message, token);
        }

        public static void ThrowOperationCanceledException(string message, Exception innerException, CancellationToken token)
        {
            throw new OperationCanceledException(message, innerException, token);
        }

        public static void ThrowPlatformNotSupportedException(string message)
        {
            throw new PlatformNotSupportedException(message);
        }

        public static void ThrowPlatformNotSupportedException(string message, Exception innerException)
        {
            throw new PlatformNotSupportedException(message, innerException);
        }

        public static void ThrowSynchronizationLockException(string message)
        {
            throw new SynchronizationLockException(message);
        }

        public static void ThrowSynchronizationLockException(string message, Exception innerException)
        {
            throw new SynchronizationLockException(message, innerException);
        }

        public static void ThrowTimeoutException(string message)
        {
            throw new TimeoutException(message);
        }

        public static void ThrowTimeoutException(string message, Exception innerException)
        {
            throw new TimeoutException(message, innerException);
        }

        /// <summary>
        public static void ThrowUnauthorizedAccessException(string message)
        {
            throw new UnauthorizedAccessException(message);
        }

        public static void ThrowUnauthorizedAccessException(string message, Exception innerException)
        {
            throw new UnauthorizedAccessException(message, innerException);
        }

        public static void ThrowWin32Exception(int error)
        {
            throw new Win32Exception(error);
        }

        public static void ThrowWin32Exception(int error, string message)
        {
            throw new Win32Exception(error, message);
        }

        public static void ThrowWin32Exception(string message)
        {
            throw new Win32Exception(message);
        }

        public static void ThrowWin32Exception(string message, Exception innerException)
        {
            throw new Win32Exception(message, innerException);
        }
    }
}
