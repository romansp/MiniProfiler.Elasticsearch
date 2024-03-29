﻿using Nest;
using StackExchange.Profiling.Elasticsearch.Internal;

namespace StackExchange.Profiling.Elasticsearch;

/// <summary>
/// Profiled version of <see cref="ElasticClient"/>. Handles responses and pushes data to current <see cref="MiniProfiler"/>'s session.
/// </summary>
public class ProfiledElasticClient : ElasticClient {
    /// <summary>
    /// Provides base <see cref="ElasticClient"/> with profiling features to current <see cref="MiniProfiler"/> session.
    /// </summary>
    /// <param name="configuration">Instance of <see cref="ConnectionSettings"/>. Its responses will be handled and pushed to <see cref="MiniProfiler"/></param>
    public ProfiledElasticClient(ConnectionSettings configuration) : base(configuration) {
        ProfilerUtils.ExcludeElasticAssemblies();
        configuration.OnRequestCompleted(apiCallDetails => MiniProfilerElasticsearch.HandleResponse(apiCallDetails));
    }
}
