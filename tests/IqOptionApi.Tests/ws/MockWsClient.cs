using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using AutoFixture;
using IqOptionApi.Extensions;
using IqOptionApi.Models;
using Websocket.Client;

namespace IqOptionApi.Tests.ws {

    public class MockWsClient : Websocket.Client.IWebsocketClient {

        private Subject<string> _subject { get; } = new Subject<string>();

        public MockWsClient() {

            var fixture = new Fixture();

            var obs = Observable.Interval(TimeSpan.FromMilliseconds(200), new EventLoopScheduler())
                .Subscribe(x => {
                    _subject.OnNext(fixture.Create<ServerTime>().AsJson());
                });
        }


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

        public IObservable<string> MessageReceived => _subject;
        public int ReconnectTimeoutMs { get; set; }
        public int ErrorReconnectTimeoutMs { get; set; }
        public string Name { get; set; } = nameof(MockWsClient);
        public bool IsStarted { get; private set; }
        public bool IsRunning { get; private set; }
        public IObservable<ReconnectionType> ReconnectionHappened { get; }
        public IObservable<DisconnectionType> DisconnectionHappened { get; }
    }
}