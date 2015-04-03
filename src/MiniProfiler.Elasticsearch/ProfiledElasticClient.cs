namespace StackExchange.Profiling.Elasticsearch
{
	using Nest;

	public class ProfiledElasticClient : ElasticClient
	{
		public ProfiledElasticClient(ConnectionSettings configuration, MiniProfiler profiler)
			: base(configuration)
		{
			configuration.EnableMetrics();
			configuration.ExposeRawResponse();
			configuration.SetConnectionStatusHandler(response => MiniProfilerElasticsearch.HandleResponse(response, profiler));
		}
	}
}