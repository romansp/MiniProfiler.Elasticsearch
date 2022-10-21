using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using StackExchange.Profiling.Elasticsearch;
using Xunit;
using StackExchangeMiniProfiler = StackExchange.Profiling.MiniProfiler;

namespace MiniProfiler.Elasticsearch.Tests;

public class ElasticClientTests {
    [Fact]
    public async Task ElasticCallUnsuccessful_ProfilerTimingErrored() {
        // Arrange
        var connectionPool = new SingleNodeConnectionPool(new Uri("http://non-existing-host.non-existing-tld"));
        var settings = new ConnectionSettings(connectionPool)
            .DefaultIndex("test-index");

        var profiler = StackExchangeMiniProfiler.StartNew();
        var client = new ProfiledElasticClient(settings);
        var person = new { Id = "1" };

        // Act
        await client.IndexDocumentAsync(person);

        // Assert
        profiler.Root.CustomTimings.TryGetValue("elasticsearch", out var elasticTimings);
        Assert.True(elasticTimings![0].Errored);
    }

    [Fact]
    public async Task DiagnosticListener_IndexDocument_ProfilerIncludesTimings() {
        // Arrange
        using var listener = new ElasticDiagnosticListener();
        using var foo = DiagnosticListener.AllListeners.Subscribe(listener);
        var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
        var settings = new ConnectionSettings(connectionPool, new InMemoryConnection())
            .DefaultIndex("test-index");

        var profiler = StackExchangeMiniProfiler.StartNew();
        var client = new ElasticClient(settings);
        var person = new { Id = "1" };

        // Act
        await client.IndexDocumentAsync(person);

        // Assert
        AssertTimings(profiler);
    }

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
        AssertTimings(profiler);
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
        AssertTimings(profiler);
    }

    private static void AssertTimings(StackExchangeMiniProfiler profiler) {
        var customTimings = profiler.Root.CustomTimings;
        Assert.NotEmpty(customTimings);
        Assert.True(customTimings.TryGetValue("elasticsearch", out var elasticTimings));
        Assert.NotEmpty(elasticTimings);
        Assert.Collection(elasticTimings, timing => {
            Assert.True(timing.DurationMilliseconds > 0);
        });
    }
}
