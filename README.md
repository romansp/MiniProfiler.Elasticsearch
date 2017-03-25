# MiniProfiler.Elasticsearch
[Elasticsearch.Net and NEST](https://github.com/elastic/elasticsearch-net) client for logging to [MiniProfiler](https://github.com/MiniProfiler/dotnet).
## Install
Run the following command in the Package Manager Console (NuGet).
```bash
PM> Install-Package MiniProfiler.Elasticsearch
```
## Versions compatibility
| MiniProfiler.Elasticsearch | NEST and Elasticsearch.Net | MiniProfiler | Build Status | NuGet Feed |
| -------------------------- | -------------------------- | ------------ | ------------ | ---------- |
| `3.x` | `1.x` | `3.x` | [![Build status](https://ci.appveyor.com/api/projects/status/y6k4dia1iamrki8m/branch/3.x?svg=true)](https://ci.appveyor.com/project/romansp/miniprofiler-elasticsearch/branch/3.x) | [![Nuget feed](https://img.shields.io/badge/nuget-v3.2.0-blue.svg)](https://www.nuget.org/packages/MiniProfiler.Elasticsearch/3.2.0)
| `4.x` | `2.x` | `3.x` | [![Build status](https://ci.appveyor.com/api/projects/status/y6k4dia1iamrki8m/branch/4.x?svg=true)](https://ci.appveyor.com/project/romansp/miniprofiler-elasticsearch/branch/4.x) | [![Nuget feed](https://img.shields.io/badge/nuget-v4.0.0-blue.svg)](https://www.nuget.org/packages/MiniProfiler.Elasticsearch/4.0.0)
| `5.x` | `5.x` | `3.x` | [![Build status](https://ci.appveyor.com/api/projects/status/y6k4dia1iamrki8m/branch/master?svg=true)](https://ci.appveyor.com/project/romansp/miniprofiler-elasticsearch/branch/master) | [![Nuget feed](https://img.shields.io/nuget/vpre/MiniProfiler.Elasticsearch.svg)](https://www.nuget.org/packages/MiniProfiler.Elasticsearch/4.0.0)

## Usage
Replace ``ElasticClient`` or ``ElasticsearchClient`` usages with their profiled version: ``ProfiledElasticClient`` or ``ProfiledElasticsearchClient``.

See **Sample.Elasticsearch** project for working example.
