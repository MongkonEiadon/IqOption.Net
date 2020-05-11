using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using IqOptionApi.Models;
using static System.DateTimeOffset;

namespace IqOptionApi.Tests
{
    public abstract class TickValue
    {
        public abstract string SourceName { get; }
        
        public ActivePair Pair { get; private set; }
        
        public double Value { get; private set; }
        
        public DateTimeOffset TimeStamp { get; set; }

        public void SetTick(ActivePair pair, double values)
        {
            Pair = pair;
            Value = values;
            TimeStamp = Now;
        }
    }

    public class IqOptionTickValue : TickValue
    {
        public override string SourceName => "IqOption";
    }

    public class ForexTickValue : TickValue
    {
        public override string SourceName => "Forex";
    }


    public class TickValueSourceService
    {
        private readonly Subject<IqOptionTickValue> _iqOptionTickValueSource = new Subject<IqOptionTickValue>();
        public IObservable<IqOptionTickValue> IqOptionTickValueSource => _iqOptionTickValueSource.AsObservable();
        
        
        private readonly Subject<ForexTickValue> _forexTickValueSource = new Subject<ForexTickValue>();
        public IObservable<ForexTickValue> ForexTickValueSource => _forexTickValueSource.AsObservable();


        public void NextIqOptionTick(IqOptionTickValue tick)
        {
            _iqOptionTickValueSource.OnNext(tick);
        }

        public void NextForexTick(ForexTickValue tick)
        {
            _forexTickValueSource.OnNext(tick);
        }
        
        public IObservable<TickValue> TickValueObservable =>
            Observable.Merge(
                IqOptionTickValueSource.Select(x => (TickValue)x),
                ForexTickValueSource.Select(x => (TickValue)x));
        
    }
}