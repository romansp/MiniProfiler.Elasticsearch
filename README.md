#MiniProfiler.Elasticsearch
[Elasticsearch.Net and NEST](http://nest.azurewebsites.net) client for logging to [MiniProfiler](http://miniprofiler.com).
## Install
Run the following command in the Package Manager Console (NuGet).
```bash
PM> Install-Package MiniProfiler.Elasticsearch
```
## Use
Replace ``ElasticClient`` or ``ElasticsearchClient`` usages with their profiled version: ``ProfiledElasticClient`` or ``ProfiledElasticsearchClient``.

See **Sample.Elasticsearch** project for working example.
