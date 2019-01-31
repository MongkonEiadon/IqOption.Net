using NUnit.Framework;
using System;
using Newtonsoft.Json;
using TestsFor;

namespace IqOptionApi.unit.JsonTest {

    [TestFixture]
    public abstract class LoadJsonFileTest<TType> : Test {

        public abstract string JsonSourceFileName { get; }

        private Lazy<TType> _LazySource => new Lazy<TType>(() => JsonConvert.DeserializeObject<TType>(LoadJson()));

        public TType ReadFileSource() => _LazySource.Value;

        [SetUp]
        public void SetUp()
        {

        }

        public string LoadJson() {

            var JSON = System.IO.File.ReadAllText($"Json\\{JsonSourceFileName}");
            return JSON;
        }
        
    }
}