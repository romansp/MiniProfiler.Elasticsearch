namespace StackExchange.Profiling {
    using System;
    using System.Diagnostics;
    using StackExchange.Profiling.Elasticsearch;
    using StackExchange.Profiling.Elasticsearch.Utils;
    using StackExchange.Profiling.Internal;

    /// <summary>
    /// Extension methods for the MiniProfiler.Elasticsearch.
    /// </summary>
    public static class MiniProfilerBaseOptionsExtensions {
        /// <summary>
        /// Adds Elastic profiling for MiniProfiler via DiagnosticListener.
        /// </summary>
        /// <typeparam name="T">The specific options type to chain with.</typeparam>
        /// <param name="options">The <see cref="MiniProfilerBaseOptions" /> to register on (just for chaining).</param>
        /// <exception cref="ArgumentNullException"><paramref name="options"/> is <c>null</c>.</exception>
        public static T AddElastic<T>(this T options) where T : MiniProfilerBaseOptions {
            options.ExcludeElasticAssemblies();

            DiagnosticListener.AllListeners.Subscribe(new ElasticDiagnosticListener());

            return options;
        }
    }
}
