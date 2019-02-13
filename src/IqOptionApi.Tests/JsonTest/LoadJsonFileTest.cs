using NUnit.Framework;
using System;
using Newtonsoft.Json;

namespace IqOptionApi.Tests.JsonTest {

    [TestFixture]
    public abstract class LoadJsonFileTest<TType> {

        public abstract string JsonSourceFileName { get; }

        private Lazy<TType> _LazySource => new Lazy<TType>(() => JsonConvert.DeserializeObject<TType>(LoadJson()));

        public TType ReadFileSource() => _LazySource.Value;
        
        public string LoadJson() {

            var JSON = System.IO.File.ReadAllText($"Json\\{JsonSourceFileName}");
            return JSON;
        }
        
    }
}