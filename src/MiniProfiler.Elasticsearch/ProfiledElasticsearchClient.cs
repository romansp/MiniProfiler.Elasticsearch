namespace StackExchange.Profiling.Elasticsearch
{
	using global::Elasticsearch.Net;
	using global::Elasticsearch.Net.Connection;
	using Utils;

    /// <summary>
    /// Profiled version of <see cref="ElasticsearchClient"/>. Handles responses and pushes data to current <see cref="MiniProfiler"/>'s session.
    /// </summary>
	public class ProfiledElasticsearchClient : ElasticsearchClient
	{
		private readonly MiniProfiler _profiler = MiniProfiler.Current;

        /// <summary>
        /// Provides base <see cref="ElasticsearchClient"/> with profiling features to current <see cref="MiniProfiler"/> session.
        /// </summary>
        /// <param name="configuration">Instance of <see cref="ConnectionConfiguration"/>. Its responses will be handled and pushed to <see cref="MiniProfiler"/></param>
		public ProfiledElasticsearchClient(ConnectionConfiguration configuration) 
			: base(configuration)
		{
			ProfilerUtils.ExcludeElasticsearchAssemblies();
			ProfilerUtils.ApplyConfigurationSettings(configuration);
			configuration.SetConnectionStatusHandler(response => MiniProfilerElasticsearch.HandleResponse(response, _profiler));
		}
	}
}