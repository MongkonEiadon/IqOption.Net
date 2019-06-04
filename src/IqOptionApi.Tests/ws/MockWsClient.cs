using System;
using System.Reactive.Concurrency;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Reactive.Testing;
using Websocket.Client;

namespace IqOptionApi.Tests.ws {
    public class MockWsClient : IWebsocketClient {
        private IObservable<ResponseMessage> _messageReceived;

        public MockWsClient() {
            var scheduler = new TestScheduler();
            scheduler.Schedule(TimeSpan.FromTicks(20), () => Start());
        }

        private Subject<string> _subject { get; } = new Subject<string>();


        public void Dispose() {
            _subject.Dispose();
        }

        public Task Start() {
            IsRunning = true;
            IsStarted = true;


            return Task.CompletedTask;
        }

        public Task Send(string message) {
            return Task.CompletedTask;
        }

        public Task SendInstant(string message) {
            return Task.CompletedTask;
        }

        public Task Reconnect() {
            return Task.CompletedTask;
        }

        public Uri Url { get; set; }

        public IObservable<string> MessageReceived => _subject;
        public int ReconnectTimeoutMs { get; set; }
        public int ErrorReconnectTimeoutMs { get; set; }
        public string Name { get; set; } = nameof(MockWsClient);
        public bool IsStarted { get; private set; }
        public bool IsRunning { get; private set; }
        public Encoding MessageEncoding { get; set; }

        IObservable<ResponseMessage> IWebsocketClient.MessageReceived => _messageReceived;

        public IObservable<ReconnectionType> ReconnectionHappened { get; }
        public IObservable<DisconnectionType> DisconnectionHappened { get; }
    }
}