using System;
using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;

namespace IqOptionApi.Tests.JsonTest {
    [TestFixture]
    public abstract class LoadJsonFileTest<TType> {
        public abstract string JsonSourceFileName { get; }

        private Lazy<TType> _LazySource => new Lazy<TType>(() => JsonConvert.DeserializeObject<TType>(LoadJson()));

        public TType ReadFileSource() {
            return _LazySource.Value;
        }

        public string LoadJson() {
            var JSON = File.ReadAllText($"Json\\{JsonSourceFileName}");
            return JSON;
        }
    }
}