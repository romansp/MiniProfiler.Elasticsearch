namespace StackExchange.Profiling.Elasticsearch {
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using global::Elasticsearch.Net;
    using global::Elasticsearch.Net.Diagnostics;

    /// <summary>
    /// Diagnostic listener for NEST and Elasticsearch.Net events.
    /// </summary>
    public class ElasticDiagnosticListener : IObserver<DiagnosticListener>, IDisposable {
        private bool disposedValue;
        private ConcurrentBag<IDisposable> Disposables { get; } = new ConcurrentBag<IDisposable>();

        /// <inheritdoc />
        public void OnError(Exception error) => Trace.WriteLine(error);

        /// <inheritdoc />
        public bool Completed { get; private set; }

        /// <inheritdoc />
        public string ListenerName => DiagnosticSources.RequestPipeline.SourceName;

        /// <inheritdoc />
        public void OnCompleted() => Completed = true;

        /// <inheritdoc />
        public void OnNext(DiagnosticListener value) {
            TrySubscribe(DiagnosticSources.RequestPipeline.SourceName,
                () => new RequestPipelineDiagnosticObserver(
                    v => WriteToProfiler(v.Key, v.Value),
                    v => WriteToProfiler(v.Key, v.Value)
                ), value);

            //TrySubscribe(DiagnosticSources.AuditTrailEvents.SourceName,
            //    () => new AuditDiagnosticObserver(
            //        v => WriteToProfiler(v.Key, v.Value)
            //    ), value);

            //TrySubscribe(DiagnosticSources.Serializer.SourceName,
            //    () => new SerializerDiagnosticObserver(
            //        v => WriteToProfiler(v.Key, v.Value)
            //    ), value);

            //TrySubscribe(DiagnosticSources.HttpConnection.SourceName,
            //    () => new HttpConnectionDiagnosticObserver(
            //        v => WriteToProfiler(v.Key, v.Value),
            //        v => WriteToProfiler(v.Key, v.Value)
            //    ), value);
        }

        /// <inheritdoc />
        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    foreach (var d in Disposables) {
                        d.Dispose();
                    }
                }

                disposedValue = true;
            }
        }

        /// <inheritdoc />
        public void Dispose() {
            Dispose(disposing: true);
        }

        private void TrySubscribe(string sourceName, Func<IObserver<KeyValuePair<string, object?>>> listener, DiagnosticListener value) {
            if (value.Name != sourceName) return;

            var subscription = value.Subscribe(listener());
            Disposables.Add(subscription);
        }

        private static void WriteToProfiler(string eventName, IApiCallDetails data) {
            MiniProfilerElasticsearch.HandleResponse(data);
        }

        private static void WriteToProfiler(string eventName, RequestData data) {
            // skip these events
        }
    }
}
