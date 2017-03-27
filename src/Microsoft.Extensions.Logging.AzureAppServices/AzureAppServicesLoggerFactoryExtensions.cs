﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.Extensions.Logging.AzureAppServices;
using Microsoft.Extensions.Logging.AzureAppServices.Internal;

namespace Microsoft.Extensions.Logging
{
    /// <summary>
    /// Extension methods for <see cref="AzureAppServicesDiagnosticsLoggerProvider"/>.
    /// </summary>
    public static class AzureAppServicesLoggerFactoryExtensions
    {
        /// <summary>
        /// Adds an Azure Web Apps diagnostics logger.
        /// </summary>
        /// <param name="factory">The extension method argument</param>
        public static ILoggerFactory AddAzureWebAppDiagnostics(this ILoggerFactory factory)
        {
            return AddAzureWebAppDiagnostics(factory, new AzureAppServicesDiagnosticsSettings());
        }

        /// <summary>
        /// Adds an Azure Web Apps diagnostics logger.
        /// </summary>
        /// <param name="factory">The extension method argument</param>
        /// <param name="settings">The setting object to configure loggers.</param>
        public static ILoggerFactory AddAzureWebAppDiagnostics(this ILoggerFactory factory, AzureAppServicesDiagnosticsSettings settings)
        {
            if (WebAppContext.Default.IsRunningInAzureWebApp)
            {
                // Only add the provider if we're in Azure WebApp. That cannot change once the apps started
#pragma warning disable CS0612 // Type or member is obsolete
                factory.AddProvider(new AzureAppServicesDiagnosticsLoggerProvider(WebAppContext.Default, settings));
#pragma warning restore CS0612 // Type or member is obsolete
            }
            return factory;
        }
    }
}
