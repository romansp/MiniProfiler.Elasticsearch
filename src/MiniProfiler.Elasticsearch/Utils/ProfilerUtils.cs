namespace StackExchange.Profiling.Elasticsearch.Utils
{
	using global::Elasticsearch.Net.Connection;

	public static class ProfilerUtils
	{
		internal static void ApplyConfigurationSettings<T>(ConnectionConfiguration<T> configuration) where T : ConnectionConfiguration<T>
		{
			configuration.EnableMetrics();
			configuration.ExposeRawResponse();
		}
	}
}