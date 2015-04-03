namespace StackExchange.Profiling.Elasticsearch
{
	using global::Elasticsearch.Net;
	using global::Elasticsearch.Net.Connection;

	public class ProfiledElasticsearchClient : ElasticsearchClient
	{
		public ProfiledElasticsearchClient(ConnectionConfiguration configuration, MiniProfiler profiler) 
			: base(configuration)
		{
			configuration.EnableMetrics();
			configuration.ExposeRawResponse();
			configuration.SetConnectionStatusHandler(response => MiniProfilerElasticsearch.HandleResponse(response, profiler));
		}
	}
}