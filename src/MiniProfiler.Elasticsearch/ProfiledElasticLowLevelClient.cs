using Elasticsearch.Net;
using StackExchange.Profiling.Elasticsearch.Internal;

namespace StackExchange.Profiling.Elasticsearch;

/// <summary>
/// Profiled version of <see cref="ElasticLowLevelClient"/>. Handles responses and pushes data to current <see cref="MiniProfiler"/>'s session.
/// </summary>
public class ProfiledElasticLowLevelClient : ElasticLowLevelClient {
    /// <summary>
    /// Provides base <see cref="ElasticLowLevelClient"/> with profiling features to current <see cref="MiniProfiler"/> session.
    /// </summary>
    /// <param name="configuration">Instance of <see cref="ConnectionConfiguration"/>. Its responses will be handled and pushed to <see cref="MiniProfiler"/></param>
    public ProfiledElasticLowLevelClient(ConnectionConfiguration configuration) : base(configuration) {
        ProfilerUtils.ExcludeElasticAssemblies();
        configuration.OnRequestCompleted(apiCallDetails => MiniProfilerElasticsearch.HandleResponse(apiCallDetails));
    }
}
