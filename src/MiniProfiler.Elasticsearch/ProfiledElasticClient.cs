namespace StackExchange.Profiling.Elasticsearch
{
	using Nest;
	using Utils;

	public class ProfiledElasticClient : ElasticClient
	{
		public ProfiledElasticClient(ConnectionSettings configuration, MiniProfiler profiler)
			: base(configuration)
		{
			ProfilerUtils.ExcludeElasticsearchAssemblies();
			ProfilerUtils.ApplyConfigurationSettings(configuration);

			configuration.SetConnectionStatusHandler(response => MiniProfilerElasticsearch.HandleResponse(response, profiler));
		}
	}
}