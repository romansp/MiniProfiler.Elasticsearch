using System.Collections.Generic;
using StackExchange.Profiling.Internal;

namespace StackExchange.Profiling.Elasticsearch.Internal;

internal static class ProfilerUtils {
    /// <summary>
    /// Elasticsearch-related assemblies to exclude from profiling
    /// </summary>
    internal static HashSet<string> ExcludedAssemblies { get; } = new HashSet<string> {
        "Elasticsearch.Net",
        "Elasticsearch.Net.Diagnostics",
        "Nest",
        typeof(MiniProfilerElasticsearch).Namespace,
        typeof(MiniProfilerElasticsearch).Assembly.GetName().Name,
    };

    /// <summary>
    /// Excludes Elastic assemblies from passed in <paramref name="options"/>, so they won't be included into <see cref="MiniProfiler"/> timings' call-stack.
    /// </summary>
    /// <param name="options"></param>
    internal static void ExcludeElasticAssemblies(this MiniProfilerBaseOptions options) {
        foreach (var excludedAssembly in ExcludedAssemblies) {
            options.ExcludeAssembly(excludedAssembly);
        }
    }

    /// <summary>
    /// Excludes Elastic assemblies from <see cref="MiniProfiler.DefaultOptions"/>, so they won't be included into <see cref="MiniProfiler"/> timings' call-stack.
    /// </summary>
    internal static void ExcludeElasticAssemblies() => MiniProfiler.DefaultOptions.ExcludeElasticAssemblies();
}
