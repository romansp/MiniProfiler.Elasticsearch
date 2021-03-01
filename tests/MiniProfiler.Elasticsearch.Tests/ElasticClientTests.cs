using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using StackExchange.Profiling.Elasticsearch;
using Xunit;
using StackExchangeMiniProfiler = StackExchange.Profiling.MiniProfiler;

namespace MiniProfiler.Elasticsearch.Tests {
    public class ElasticClientTests {
        [Fact]
        public async Task ProfiledElasticClient_IndexDocument_ProfilerIncludesTimings() {
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

        [Fact]
        public async Task ProfiledLowLevelClient_IndexDocument_ProfilerIncludesTimings() {
            // Arrange
            var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
            var settings = new ConnectionConfiguration(connectionPool, new InMemoryConnection());

            var profiler = StackExchangeMiniProfiler.StartNew();
            var client = new ProfiledElasticLowLevelClient(settings);
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
