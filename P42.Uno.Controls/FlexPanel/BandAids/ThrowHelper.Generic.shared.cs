// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.ComponentModel;
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
        public static T ThrowArrayTypeMismatchException<T>(string message)
        {
            throw new ArrayTypeMismatchException(message);
        }

        public static T ThrowArrayTypeMismatchException<T>(string message, Exception innerException)
        {
            throw new ArrayTypeMismatchException(message, innerException);
        }

        public static T ThrowArgumentException<T>(string message)
        {
            throw new ArgumentException(message);
        }

        public static T ThrowArgumentException<T>(string message, Exception innerException)
        {
            throw new ArgumentException(message, innerException);
        }

        public static T ThrowArgumentException<T>(string name, string message)
        {
            throw new ArgumentException(message, name);
        }

        public static T ThrowArgumentException<T>(string name, string message, Exception innerException)
        {
            throw new ArgumentException(message, name, innerException);
        }

        public static T ThrowArgumentNullException<T>(string name)
        {
            throw new ArgumentNullException(name);
        }

        public static T ThrowArgumentNullException<T>(string name, Exception innerException)
        {
            throw new ArgumentNullException(name, innerException);
        }

        public static T ThrowArgumentNullException<T>(string name, string message)
        {
            throw new ArgumentNullException(name, message);
        }

        public static T ThrowArgumentOutOfRangeException<T>(string name)
        {
            throw new ArgumentOutOfRangeException(name);
        }

        public static T ThrowArgumentOutOfRangeException<T>(string name, Exception innerException)
        {
            throw new ArgumentOutOfRangeException(name, innerException);
        }

        public static T ThrowArgumentOutOfRangeException<T>(string name, string message)
        {
            throw new ArgumentOutOfRangeException(name, message);
        }

        public static T ThrowArgumentOutOfRangeException<T>(string name, object value, string message)
        {
            throw new ArgumentOutOfRangeException(name, value, message);
        }

#if !NETSTANDARD1_4
        public static T ThrowCOMException<T>(string message)
        {
            throw new COMException(message);
        }

        public static T ThrowCOMException<T>(string message, Exception innerException)
        {
            throw new COMException(message, innerException);
        }

        public static T ThrowCOMException<T>(string message, int error)
        {
            throw new COMException(message, error);
        }

        public static T ThrowExternalException<T>(string message)
        {
            throw new ExternalException(message);
        }

        public static T ThrowExternalException<T>(string message, Exception innerException)
        {
            throw new ExternalException(message, innerException);
        }

        public static T ThrowExternalException<T>(string message, int error)
        {
            throw new ExternalException(message, error);
        }
#endif

        public static T ThrowFormatException<T>(string message)
        {
            throw new FormatException(message);
        }

        public static T ThrowFormatException<T>(string message, Exception innerException)
        {
            throw new FormatException(message, innerException);
        }

#if !NETSTANDARD1_4
        public static T ThrowInsufficientMemoryException<T>(string message)
        {
            throw new InsufficientMemoryException(message);
        }

        public static T ThrowInsufficientMemoryException<T>(string message, Exception innerException)
        {
            throw new InsufficientMemoryException(message, innerException);
        }
#endif

        public static T ThrowInvalidDataException<T>(string message)
        {
            throw new InvalidDataException(message);
        }

        public static T ThrowInvalidDataException<T>(string message, Exception innerException)
        {
            throw new InvalidDataException(message, innerException);
        }

        public static T ThrowInvalidOperationException<T>(string message)
        {
            throw new InvalidOperationException(message);
        }

        public static T ThrowInvalidOperationException<T>(string message, Exception innerException)
        {
            throw new InvalidOperationException(message, innerException);
        }

        public static T ThrowLockRecursionException<T>(string message)
        {
            throw new LockRecursionException(message);
        }

        public static T ThrowLockRecursionException<T>(string message, Exception innerException)
        {
            throw new LockRecursionException(message, innerException);
        }

        public static T ThrowMissingFieldException<T>(string message)
        {
            throw new MissingFieldException(message);
        }

        public static T ThrowMissingFieldException<T>(string message, Exception innerException)
        {
            throw new MissingFieldException(message, innerException);
        }

#if !NETSTANDARD1_4
        public static T ThrowMissingFieldException<T>(string className, string fieldName)
        {
            throw new MissingFieldException(className, fieldName);
        }
#endif

        public static T ThrowMissingMemberException<T>(string message)
        {
            throw new MissingMemberException(message);
        }

        public static T ThrowMissingMemberException<T>(string message, Exception innerException)
        {
            throw new MissingMemberException(message, innerException);
        }

#if !NETSTANDARD1_4
        public static T ThrowMissingMemberException<T>(string className, string memberName)
        {
            throw new MissingMemberException(className, memberName);
        }
#endif

        public static T ThrowMissingMethodException<T>(string message)
        {
            throw new MissingMethodException(message);
        }

        public static T ThrowMissingMethodException<T>(string message, Exception innerException)
        {
            throw new MissingMethodException(message, innerException);
        }

#if !NETSTANDARD1_4
        public static T ThrowMissingMethodException<T>(string className, string methodName)
        {
            throw new MissingMethodException(className, methodName);
        }
#endif

        public static T ThrowNotSupportedException<T>(string message)
        {
            throw new NotSupportedException(message);
        }

        public static T ThrowNotSupportedException<T>(string message, Exception innerException)
        {
            throw new NotSupportedException(message, innerException);
        }

        public static T ThrowObjectDisposedException<T>(string objectName)
        {
            throw new ObjectDisposedException(objectName);
        }

        public static T ThrowObjectDisposedException<T>(string objectName, Exception innerException)
        {
            throw new ObjectDisposedException(objectName, innerException);
        }

        public static T ThrowObjectDisposedException<T>(string objectName, string message)
        {
            throw new ObjectDisposedException(objectName, message);
        }

        public static T ThrowOperationCanceledException<T>(string message)
        {
            throw new OperationCanceledException(message);
        }

        public static T ThrowOperationCanceledException<T>(string message, Exception innerException)
        {
            throw new OperationCanceledException(message, innerException);
        }

        public static T ThrowOperationCanceledException<T>(CancellationToken token)
        {
            throw new OperationCanceledException(token);
        }

        public static T ThrowOperationCanceledException<T>(string message, CancellationToken token)
        {
            throw new OperationCanceledException(message, token);
        }

        public static T ThrowOperationCanceledException<T>(string message, Exception innerException, CancellationToken token)
        {
            throw new OperationCanceledException(message, innerException, token);
        }

        public static T ThrowPlatformNotSupportedException<T>(string message)
        {
            throw new PlatformNotSupportedException(message);
        }

        public static T ThrowPlatformNotSupportedException<T>(string message, Exception innerException)
        {
            throw new PlatformNotSupportedException(message, innerException);
        }

        public static T ThrowSynchronizationLockException<T>(string message)
        {
            throw new SynchronizationLockException(message);
        }

        public static T ThrowSynchronizationLockException<T>(string message, Exception innerException)
        {
            throw new SynchronizationLockException(message, innerException);
        }

        public static T ThrowTimeoutException<T>(string message)
        {
            throw new TimeoutException(message);
        }

        public static T ThrowTimeoutException<T>(string message, Exception innerException)
        {
            throw new TimeoutException(message, innerException);
        }

        public static T ThrowUnauthorizedAccessException<T>(string message)
        {
            throw new UnauthorizedAccessException(message);
        }

        public static T ThrowUnauthorizedAccessException<T>(string message, Exception innerException)
        {
            throw new UnauthorizedAccessException(message, innerException);
        }

        public static T ThrowWin32Exception<T>(int error)
        {
            throw new Win32Exception(error);
        }

        public static T ThrowWin32Exception<T>(int error, string message)
        {
            throw new Win32Exception(error, message);
        }

        public static T ThrowWin32Exception<T>(string message)
        {
            throw new Win32Exception(message);
        }

        public static T ThrowWin32Exception<T>(string message, Exception innerException)
        {
            throw new Win32Exception(message, innerException);
        }
    }
}
