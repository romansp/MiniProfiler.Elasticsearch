namespace StackExchange.Profiling.Elasticsearch;

using System;
using System.Linq;
using global::Elasticsearch.Net;
using Profiling;

/// <summary>
/// <see cref="IApiCallDetails"/> handler class.
/// </summary>
internal static class MiniProfilerElasticsearch {
    /// <summary>
    /// Handles <see cref="IApiCallDetails"/> and pushes <see cref="CustomTiming"/> to current <see cref="MiniProfiler"/> session.
    /// </summary>
    /// <param name="apiCallDetails"><see cref="IApiCallDetails"/> to be handled.</param>
    internal static void HandleResponse(IApiCallDetails? apiCallDetails) {
        _ = apiCallDetails ?? throw new ArgumentNullException(nameof(apiCallDetails));

        var profiler = MiniProfiler.Current;
        if (profiler is null || profiler.Head is null || apiCallDetails.DebugInformation is null) {
            return;
        }

        profiler.Head.AddCustomTiming("elasticsearch", new CustomTiming(profiler, apiCallDetails.DebugInformation) {
            DurationMilliseconds = (decimal?)apiCallDetails.AuditTrail?.Sum(c => (c.Ended - c.Started).TotalMilliseconds),
            ExecuteType = apiCallDetails.HttpMethod.ToString(),
            Errored = !apiCallDetails.Success
        });
    }
}
