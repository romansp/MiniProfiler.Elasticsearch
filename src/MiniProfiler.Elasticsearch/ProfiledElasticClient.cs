namespace StackExchange.Profiling.Elasticsearch
{
	using Nest;
	using Utils;

	public class ProfiledElasticClient : ElasticClient
	{
		private readonly MiniProfiler _profiler = MiniProfiler.Current;

		public ProfiledElasticClient(ConnectionSettings configuration)
			: base(configuration)
		{
			ProfilerUtils.ExcludeElasticsearchAssemblies();
			ProfilerUtils.ApplyConfigurationSettings(configuration);
			configuration.SetConnectionStatusHandler(response => MiniProfilerElasticsearch.HandleResponse(response, _profiler));
		}
	}
}