namespace StackExchange.Profiling.Elasticsearch
{
	using global::Elasticsearch.Net;
	using global::Elasticsearch.Net.Connection;

	public class ProfiledElasticsearchClient : ElasticsearchClient
	{
		public ProfiledElasticsearchClient(ConnectionConfiguration configuration) : base(configuration)
		{
			configuration.EnableMetrics();
			configuration.ExposeRawResponse();
			configuration.SetConnectionStatusHandler(MiniProfilerElasticsearch.HandleResponse);
		}
	}
}