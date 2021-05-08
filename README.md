# MiniProfiler.Elasticsearch
Put your [Elasticsearch.Net and NEST](https://github.com/elastic/elasticsearch-net) requests timings directly into [MiniProfiler](https://github.com/MiniProfiler/dotnet).

[![Build status](https://ci.appveyor.com/api/projects/status/m15gemuqkcs1rbv4/branch/master?svg=true)](https://ci.appveyor.com/project/romansp/miniprofiler-elasticsearch/branch/master) [![Nuget feed](https://img.shields.io/nuget/vpre/MiniProfiler.Elasticsearch.svg)](https://www.nuget.org/packages/MiniProfiler.Elasticsearch)

![profiler-popup](https://user-images.githubusercontent.com/3474842/30780873-de83efd8-a11d-11e7-8735-49dea4a1d4f1.png)
![profiler-queries](https://user-images.githubusercontent.com/3474842/30780952-edf8adea-a11e-11e7-8d64-c65331f389bf.png)

## Usage
You have two options on how to start profiling your Elastic requests.

### Option 1. Register in services collection
In your `Startup.cs`, call `AddElastic()`:

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddMiniProfiler()
            .AddElastic();
}
```

### Option 2. Create profiled client manually
Update usages of `ElasticClient` or `ElasticLowLevelClient` with their respected profiled version `ProfiledElasticClient` or `ProfiledElasticLowLevelClient`.

```c#
services.AddSingleton<IElasticClient>(x => 
{
    var node = new Uri("http://localhost:9200");
    var connectionSettings = new ConnectionSettings(node).DefaultIndex("elasticsearch-sample");
    return new ProfiledElasticClient(connectionSettings);
});
```

## Sample
See [Sample.Elasticsearch.Core](https://github.com/romansp/MiniProfiler.Elasticsearch/tree/master/samples/Sample.Elasticsearch.Core) for working example.
