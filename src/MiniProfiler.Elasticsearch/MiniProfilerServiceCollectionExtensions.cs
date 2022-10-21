using System;
using StackExchange.Profiling.Elasticsearch;
using StackExchange.Profiling.Internal;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extension methods for the MiniProfiler.Elasticsearch.
/// </summary>
public static class MiniProfilerServiceCollectionExtensions {
    /// <summary>
    /// Adds Elastic profiling for MiniProfiler via DiagnosticListener.
    /// </summary>
    /// <param name="builder">The <see cref="IMiniProfilerBuilder" /> to add services to.</param>
    /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <c>null</c>.</exception>
    public static IMiniProfilerBuilder AddElastic(this IMiniProfilerBuilder builder) {
        _ = builder ?? throw new ArgumentNullException(nameof(builder));

        builder.Services.AddSingleton<IMiniProfilerDiagnosticListener, ElasticDiagnosticListener>();

        return builder;
    }
}
