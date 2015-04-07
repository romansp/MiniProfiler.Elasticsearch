namespace StackExchange.Profiling.Elasticsearch
{
	using global::Elasticsearch.Net;
	using global::Elasticsearch.Net.Connection;
	using Utils;

	public class ProfiledElasticsearchClient : ElasticsearchClient
	{
		private readonly MiniProfiler _profiler = MiniProfiler.Current;

		public ProfiledElasticsearchClient(ConnectionConfiguration configuration) 
			: base(configuration)
		{
			ProfilerUtils.ExcludeElasticsearchAssemblies();
			ProfilerUtils.ApplyConfigurationSettings(configuration);
			configuration.SetConnectionStatusHandler(response => MiniProfilerElasticsearch.HandleResponse(response, _profiler));
		}
	}
}