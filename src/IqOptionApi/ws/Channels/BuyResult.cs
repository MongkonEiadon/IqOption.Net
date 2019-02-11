using System;
using System.Collections.Generic;
using System.Text;
using IqOptionApi.Models;

// ReSharper disable once CheckNamespace
namespace IqOptionApi.ws {
    public partial class IqWsClient
    {
        private BuyResult _buyResult;
        public BuyResult BuyResult
        {
            get => _buyResult;
            set
            {
                _buyResult = value;
                OnPropertyChanged(nameof(BuyResult));
            }
        }

        
    }
}