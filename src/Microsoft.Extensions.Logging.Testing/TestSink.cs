// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Extensions.Logging.Testing
{
    public class TestSink : ITestSink
    {
        private object _lock = new object();

        public TestSink(
            Func<WriteContext, bool> writeEnabled = null,
            Func<BeginScopeContext, bool> beginEnabled = null)
        {
            WriteEnabled = writeEnabled;
            BeginEnabled = beginEnabled;

            Scopes = new List<BeginScopeContext>();
            Writes = new List<WriteContext>();
        }

        public Func<WriteContext, bool> WriteEnabled { get; set; }

        public Func<BeginScopeContext, bool> BeginEnabled { get; set; }

        public List<BeginScopeContext> Scopes { get; set; }

        public List<WriteContext> Writes { get; set; }

        public WriteContext[] WritesFromSource<T>()
        {
            lock (_lock)
            {
                return Writes.Where(message => message.LoggerName.Equals(typeof(T).FullName, StringComparison.Ordinal)).ToArray();
            }
        }

        public void Write(WriteContext context)
        {
            lock (_lock)
            {
                if (WriteEnabled == null || WriteEnabled(context))
                {
                    Writes.Add(context);
                }
            }
        }

        public void Begin(BeginScopeContext context)
        {
            if (BeginEnabled == null || BeginEnabled(context))
            {
                Scopes.Add(context);
            }
        }

        public static bool EnableWithTypeName<T>(WriteContext context)
        {
            return context.LoggerName.Equals(typeof(T).FullName);
        }

        public static bool EnableWithTypeName<T>(BeginScopeContext context)
        {
            return context.LoggerName.Equals(typeof(T).FullName);
        }
    }
}