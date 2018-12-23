# MiniProfiler.Elasticsearch
Put your [Elasticsearch.Net and NEST](https://github.com/elastic/elasticsearch-net) requests timings directly into [MiniProfiler](https://github.com/MiniProfiler/dotnet).

![profiler-popup](https://user-images.githubusercontent.com/3474842/30780873-de83efd8-a11d-11e7-8735-49dea4a1d4f1.png)
![profiler-queries](https://user-images.githubusercontent.com/3474842/30780952-edf8adea-a11e-11e7-8d64-c65331f389bf.png)

## Install
Run the following command in the Package Manager Console (NuGet).
```bash
PM> Install-Package MiniProfiler.Elasticsearch
```
## Versions compatibility
| MiniProfiler.Elasticsearch | NEST and Elasticsearch.Net | MiniProfiler | Build Status | NuGet Feed |
| -------------------------- | -------------------------- | ------------ | ------------ | ---------- |
| `3.x` | `1.x` | `3.x` | [![Build status](https://ci.appveyor.com/api/projects/status/m15gemuqkcs1rbv4/branch/3.x?svg=true)](https://ci.appveyor.com/project/romansp/miniprofiler-elasticsearch/branch/3.x) | [![Nuget feed](https://img.shields.io/badge/nuget-v3.2.0-blue.svg)](https://www.nuget.org/packages/MiniProfiler.Elasticsearch/3.2.0)
| `4.x` | `2.x` | `3.x` | [![Build status](https://ci.appveyor.com/api/projects/status/m15gemuqkcs1rbv4/branch/4.x?svg=true)](https://ci.appveyor.com/project/romansp/miniprofiler-elasticsearch/branch/4.x) | [![Nuget feed](https://img.shields.io/badge/nuget-v4.0.0-blue.svg)](https://www.nuget.org/packages/MiniProfiler.Elasticsearch/4.0.0)
| `5.x` | `5.x` | `3.x` | [![Build status](https://ci.appveyor.com/api/projects/status/m15gemuqkcs1rbv4/branch/5.x?svg=true)](https://ci.appveyor.com/project/romansp/miniprofiler-elasticsearch/branch/5.x) | [![Nuget feed](https://img.shields.io/badge/nuget-v5.0.0-blue.svg)](https://www.nuget.org/packages/MiniProfiler.Elasticsearch/5.0.0)
| `6.x` | `6.x` | `4.x` | [![Build status](https://ci.appveyor.com/api/projects/status/m15gemuqkcs1rbv4/branch/master?svg=true)](https://ci.appveyor.com/project/romansp/miniprofiler-elasticsearch/branch/master) | [![Nuget feed](https://img.shields.io/nuget/vpre/MiniProfiler.Elasticsearch.svg)](https://www.nuget.org/packages/MiniProfiler.Elasticsearch)

## Usage
Update usages of ``ElasticClient`` or ``ElasticsearchClient`` with their respected profiled version ``ProfiledElasticClient`` or ``ProfiledElasticsearchClient``.

Reference [Sample.Elasticsearch](https://github.com/romansp/MiniProfiler.Elasticsearch/tree/master/samples/Sample.Elasticsearch) for working example.
