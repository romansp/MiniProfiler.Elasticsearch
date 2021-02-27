using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using StackExchange.Profiling.Elasticsearch;
using Xunit;
using StackExchangeMiniProfiler = StackExchange.Profiling.MiniProfiler;

namespace MiniProfiler.Elasticsearch.Tests {
    public class ProfiledElasticsearchClientTests {
        [Fact]
        public async Task IndexDocument_WithProfiledClient_ProfilerIncludesTimings() {
            // Arrange
            var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
            var settings = new ConnectionConfiguration(connectionPool, new InMemoryConnection());

            var profiler = StackExchangeMiniProfiler.StartNew();
            var client = new ProfiledElasticsearchClient(settings);
            var person = new { Id = "1" };

            // Act
            await client.IndexAsync<BytesResponse>("test-index", PostData.Serializable(person));

            // Assert
            var customTimings = profiler.Root.CustomTimings;
            Assert.Single(customTimings);
            Assert.True(customTimings.TryGetValue("elasticsearch", out var elasticTimings));
            Assert.Single(elasticTimings);
        }
    }
}
