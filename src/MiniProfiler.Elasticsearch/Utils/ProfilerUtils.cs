namespace StackExchange.Profiling.Elasticsearch.Utils
{
    using global::Elasticsearch.Net;

    internal static class ProfilerUtils
    {
        /// <summary>
        /// Enabling configuration settings prior to receive internal API call timings.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configuration"></param>
        internal static void ApplyConfigurationSettings<T>(ConnectionConfiguration<T> configuration) where T : ConnectionConfiguration<T>
        {
            configuration.DisableDirectStreaming();
        }

        /// <summary>
        /// Exclude assemblies, so they won't be included into <see cref="MiniProfiler"/> timings' call-stack.
        /// </summary>
        internal static void ExcludeElasticsearchAssemblies()
        {
            MiniProfiler.DefaultOptions.ExcludeAssembly("Elasticsearch.Net");
            MiniProfiler.DefaultOptions.ExcludeAssembly("Nest");
            MiniProfiler.DefaultOptions.ExcludeAssembly(typeof(MiniProfilerElasticsearch).Assembly.GetName().Name);
        }
    }
}