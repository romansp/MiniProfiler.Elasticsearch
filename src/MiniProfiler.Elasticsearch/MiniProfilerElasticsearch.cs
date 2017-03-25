namespace StackExchange.Profiling.Elasticsearch
{
    using System;
    using System.Linq;
    using System.Text;
    using global::Elasticsearch.Net;
    using Profiling;

    /// <summary>
    /// <see cref="IApiCallDetails"/> handler class.
    /// </summary>
    internal static class MiniProfilerElasticsearch
    {
        /// <summary>
        /// Handles <see cref="IApiCallDetails"/> and pushes <see cref="CustomTiming"/> to current <see cref="MiniProfiler"/> session.
        /// </summary>
        /// <param name="apiCallDetails"><see cref="IApiCallDetails"/> to be handled.</param>
        /// <param name="profiler">Current <see cref="MiniProfiler"/> session instance.</param>
        internal static void HandleResponse(IApiCallDetails apiCallDetails, MiniProfiler profiler)
        {
            if (profiler == null || profiler.Head == null || apiCallDetails.DebugInformation == null)
                return;

            profiler.Head.AddCustomTiming("elasticsearch", new CustomTiming(profiler, apiCallDetails.DebugInformation)
            {
                Id = Guid.NewGuid(),
                DurationMilliseconds = (decimal?)apiCallDetails?.AuditTrail?.Sum(c => (c.Ended - c.Started).TotalMilliseconds),
                ExecuteType = apiCallDetails.HttpMethod.ToString(),
            });
        }
    }
}