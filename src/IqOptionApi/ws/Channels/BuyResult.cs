using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using IqOptionApi.Models;
using ReactiveUI;

// ReSharper disable once CheckNamespace
namespace IqOptionApi.ws {
    public partial class IqWsClient
    {
        private BuyResult _buyResult;
        public BuyResult BuyResult
        {
            get => _buyResult;
            private set => this.RaiseAndSetIfChanged(ref _buyResult, value);
        }
    }
}