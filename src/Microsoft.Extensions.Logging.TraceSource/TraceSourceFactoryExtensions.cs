// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
using Microsoft.Extensions.Logging.TraceSource;

namespace Microsoft.Extensions.Logging
{
    public static class TraceSourceFactoryExtensions
    {
        public static ILoggerFactory AddTraceSource(
            this ILoggerFactory factory,
            string switchName)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (switchName == null)
            {
                throw new ArgumentNullException(nameof(switchName));
            }

            return factory.AddTraceSource(new SourceSwitch(switchName));
        }

        public static ILoggerFactory AddTraceSource(
            this ILoggerFactory factory,
            string switchName,
            TraceListener listener)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (switchName == null)
            {
                throw new ArgumentNullException(nameof(switchName));
            }

            if (listener == null)
            {
                throw new ArgumentNullException(nameof(listener));
            }

            return factory.AddTraceSource(new SourceSwitch(switchName), listener);
        }

        public static ILoggerFactory AddTraceSource(
            this ILoggerFactory factory,
            SourceSwitch sourceSwitch)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (sourceSwitch == null)
            {
                throw new ArgumentNullException(nameof(sourceSwitch));
            }

#pragma warning disable CS0612 // Type or member is obsolete
            factory.AddProvider(new TraceSourceLoggerProvider(sourceSwitch));
#pragma warning restore CS0612 // Type or member is obsolete

            return factory;
        }

        public static ILoggerFactory AddTraceSource(
            this ILoggerFactory factory,
            SourceSwitch sourceSwitch,
            TraceListener listener)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (sourceSwitch == null)
            {
                throw new ArgumentNullException(nameof(sourceSwitch));
            }

            if (listener == null)
            {
                throw new ArgumentNullException(nameof(listener));
            }

#pragma warning disable CS0612 // Type or member is obsolete
            factory.AddProvider(new TraceSourceLoggerProvider(sourceSwitch, listener));
#pragma warning restore CS0612 // Type or member is obsolete

            return factory;
        }
    }
}