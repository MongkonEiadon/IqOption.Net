using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IqOptionApi.Core
{
    public class AwaitingObservableTask : IDisposable
    {
        
        private sealed class AsyncLockReleaser : IDisposable
        {
            private readonly AsyncLock _asyncLock;

            public AsyncLockReleaser(AsyncLock asyncLock)
            {
                _asyncLock = asyncLock;
            }

            public void Dispose()
            {
                _asyncLock.Release();
            }
        }
        
        
        
        
        public void Dispose()
        {
            var action = Task.Run(() => { });
            var completionObs = Observable.Interval(TimeSpan.FromSeconds(1));
        }
    }
}