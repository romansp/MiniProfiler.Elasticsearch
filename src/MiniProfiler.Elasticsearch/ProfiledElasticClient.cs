namespace StackExchange.Profiling.Elasticsearch
{
	using Nest;

	public class ProfiledElasticClient : ElasticClient
	{
		public ProfiledElasticClient(ConnectionSettings configuration) : base(configuration)
		{
			configuration.EnableMetrics();
			configuration.ExposeRawResponse();
			configuration.SetConnectionStatusHandler(MiniProfilerElasticsearch.HandleResponse);
		}
	}
}