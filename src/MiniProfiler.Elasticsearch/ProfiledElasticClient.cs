namespace StackExchange.Profiling.Elasticsearch
{
	using Nest;
	using Utils;

    /// <summary>
    /// Profiled version of <see cref="ElasticClient"/>. Handles responses and pushes data to current <see cref="MiniProfiler"/>'s session.
    /// </summary>
	public class ProfiledElasticClient : ElasticClient
	{
		private readonly MiniProfiler _profiler = MiniProfiler.Current;

        /// <summary>
        /// Provides base <see cref="ElasticClient"/> with profiling features to current <see cref="MiniProfiler"/> session.
        /// </summary>
        /// <param name="configuration">Instance of <see cref="ConnectionSettings"/>. Its responses will be handled and pushed to <see cref="MiniProfiler"/></param>
		public ProfiledElasticClient(ConnectionSettings configuration)
			: base(configuration)
		{
			ProfilerUtils.ExcludeElasticsearchAssemblies();
			ProfilerUtils.ApplyConfigurationSettings(configuration);
			configuration.SetConnectionStatusHandler(response => MiniProfilerElasticsearch.HandleResponse(response, _profiler));
		}
	}
}