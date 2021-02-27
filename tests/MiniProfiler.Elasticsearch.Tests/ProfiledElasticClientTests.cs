using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using StackExchange.Profiling.Elasticsearch;
using Xunit;
using StackExchangeMiniProfiler = StackExchange.Profiling.MiniProfiler;

namespace MiniProfiler.Elasticsearch.Tests {
    public class ProfiledElasticClientTests {
        [Fact]
        public async Task IndexDocument_WithProfiledClient_ProfilerIncludesTimings() {
            // Arrange
            var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
            var settings = new ConnectionSettings(connectionPool, new InMemoryConnection())
                .DefaultIndex("test-index");

            var profiler = StackExchangeMiniProfiler.StartNew();
            var client = new ProfiledElasticClient(settings);
            var person = new { Id = "1" };

            // Act
            await client.IndexDocumentAsync(person);

            // Assert
            var customTimings = profiler.Root.CustomTimings;
            Assert.Single(customTimings);
            Assert.True(customTimings.TryGetValue("elasticsearch", out var elasticTimings));
            Assert.Single(elasticTimings);
        }
    }
}
