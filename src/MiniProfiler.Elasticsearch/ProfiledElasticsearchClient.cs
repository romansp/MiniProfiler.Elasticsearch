namespace StackExchange.Profiling.Elasticsearch
{
	using global::Elasticsearch.Net;
	using global::Elasticsearch.Net.Connection;
	using Utils;

	public class ProfiledElasticsearchClient : ElasticsearchClient
	{
		public ProfiledElasticsearchClient(ConnectionConfiguration configuration, MiniProfiler profiler) 
			: base(configuration)
		{
			ProfilerUtils.ExcludeElasticsearchAssemblies();
			ProfilerUtils.ApplyConfigurationSettings(configuration);

			configuration.SetConnectionStatusHandler(response => MiniProfilerElasticsearch.HandleResponse(response, profiler));
		}
	}
}