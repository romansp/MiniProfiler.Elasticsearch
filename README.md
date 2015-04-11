# MiniProfiler.Elasticsearch
[Elasticsearch.Net and NEST](http://nest.azurewebsites.net) client for logging to [MiniProfiler](http://miniprofiler.com).
# Installation
via NuGet

``Install-Package MiniProfiler.Elasticsearch``
# Usage
Replace ``ElasticClient`` or ``ElasticsearchClient`` usages with their profiled version: ``ProfiledElasticClient`` or ``ProfiledElasticsearchClient``.

See **Sample.Elasticsearch** project for working example.
