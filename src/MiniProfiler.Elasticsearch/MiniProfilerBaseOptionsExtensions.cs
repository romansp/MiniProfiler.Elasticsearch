using System;
using System.Diagnostics;
using StackExchange.Profiling.Elasticsearch;
using StackExchange.Profiling.Internal;

namespace StackExchange.Profiling {
    public static class MiniProfilerBaseOptionsExtensions {
        /// <summary>
        /// Adds Entity Framework Core profiling for MiniProfiler via DiagnosticListener.
        /// </summary>
        /// <typeparam name="T">The specific options type to chain with.</typeparam>
        /// <param name="options">The <see cref="MiniProfilerBaseOptions" /> to register on (just for chaining).</param>
        /// <exception cref="ArgumentNullException"><paramref name="options"/> is <c>null</c>.</exception>
        public static T AddElastic<T>(this T options) where T : MiniProfilerBaseOptions {
            //var initializer = new DiagnosticInitializer(new[] { new ElasticDiagnosticListener() });
            //initializer.Start();

            DiagnosticListener.AllListeners.Subscribe(new ElasticDiagnosticListener());

            return options;
        }
    }
}
