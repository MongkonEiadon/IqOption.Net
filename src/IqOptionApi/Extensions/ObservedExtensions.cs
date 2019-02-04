using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly:InternalsVisibleTo(assemblyName: "IqOptionApi.Unit", AllInternalsVisible = true)]
namespace IqOptionApi.Extensions {

    internal static class ObservableExtensions
    {
        public static IObservable<R> ToObservable<T, R>(this T target, string name, Func<T, R> func)
            where T : INotifyPropertyChanged {
            return Observable.Create<R>(o => {
                PropertyChangedEventHandler eventHandler = new PropertyChangedEventHandler((s, pce) => {
                    if (pce.PropertyName == null || pce.PropertyName == name)
                        o.OnNext(func(target));
                });
                target.PropertyChanged += eventHandler;
                return () => target.PropertyChanged -= eventHandler;
            });
        }

        public static IObservable<R> ToObservable<T, R>(this T target, Expression<Func<T, R>> property)
            where T : INotifyPropertyChanged {
            var body = (property as LambdaExpression).Body;
            var propertyName = "";

            if (body is MemberExpression)
                propertyName = (body as MemberExpression).Member.Name;
            else if (body is MethodCallExpression)
                propertyName = (body as MethodCallExpression).Method.Name;
            else
                throw new NotSupportedException("Only use expressions that call a single property or method");

            var getValueFunc = property.Compile();
            return Observable.Create<R>(o => {
                PropertyChangedEventHandler eventHandler = new PropertyChangedEventHandler((s, pce) => {
                    if (pce.PropertyName == null || pce.PropertyName == propertyName)
                        o.OnNext(getValueFunc(target));
                });
                target.PropertyChanged += eventHandler;
                return () => target.PropertyChanged -= eventHandler;
            });
        }

        public static Task<TResult> WaitForNextObservedResultAsync<TResult>(this IObservable<TResult> This, Action action) {

            var tcs = new TaskCompletionSource<TResult>();
            try {

                var obs = This
                    .Merge(AsyncStart().Select(x => default(TResult)))
                    .SubscribeOn(ThreadPoolScheduler.Instance)
                    .Subscribe(
                        onNext: x => { tcs.TrySetResult(x); },
                        onCompleted: () => { tcs.TrySetResult(default(TResult)); });

                // invoke action
                action();

                // destroy obs
                tcs.Task.ContinueWith(t => {
                    if (t.IsCompleted) {
                        obs.Dispose();
                    }
                });

            }
            catch (Exception ex) {

                tcs.TrySetException(ex);
            }

            return tcs.Task;
        }

        public static IObservable<Unit> AsyncStart(TimeSpan? delay = null) {
            if (delay == null) {
                delay = TimeSpan.FromSeconds(3);
            }

            return Observable.Timer(delay.Value).Select(x => Unit.Default);
        }
    }
}