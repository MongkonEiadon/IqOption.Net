using System;
using System.Collections.Generic;
using System.Text;
using iqoptionapi.converters.JsonConverters;
using iqoptionapi.models;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Shouldly;

namespace iqoptionapi.unit.converters.JsonConverters
{
    [TestFixture()]
    public class ActivePairJsonConverterTest : BaseUnitTest
    {

        #region [WriteJsonTest]

        [Test]
        public void JsonReader_ReadValueAsString_ActivePairCanBeConverted() {

            //arrange
            var jReader = new JsonReaderTest<ActivePair>();
            jReader.SetValue(() => ActivePair.EURUSD_OTC);

            //act
            var result = new ActivePairJsonConverter()
                .ReadJson(jReader, typeof(ActivePair), "76", null);

            //assert
            result.ShouldNotBeNull();
            result.ShouldBe(ActivePair.EURUSD_OTC);

        }
        [Test]
        public void JsonReader_ReadValueAsInt_ActivePairCanBeConverted()
        {

            //arrange
            var jReader = new JsonReaderTest<ActivePair>();
            jReader.SetValue(() => ActivePair.EURUSD_OTC);

            //act
            var result = new ActivePairJsonConverter()
                .ReadJson(jReader, typeof(ActivePair), 76, null);

            //assert
            result.ShouldNotBeNull();
            result.ShouldBe(ActivePair.EURUSD_OTC);

        }



        #endregion

        [Test]
        public void ConvertToJson_WithEnumeratorSet_ValueAsIntMustSet() {
           
            //arrange
            var pair = new TestClass() {Pair = ActivePair.EURUSD_OTC};

            //act
            var result = JsonConvert.SerializeObject(pair);

            //assert
            result.ShouldBe("{\"pair\":76}");

        }

        [Test]
        public void ConvertFromJson_With76ValueSet_EurUsdOtdMustSet() {
        
            var json = "{\"pair\":76}";

            //act
            var result = JsonConvert.DeserializeObject<TestClass>(json);

            //assert
            result.ShouldNotBeNull();
            result.Pair.ShouldBe(ActivePair.EURUSD_OTC);
        }

    }

    internal class TestClass {

        [JsonProperty("pair")]
        [JsonConverter(typeof(ActivePairJsonConverter))]
        public ActivePair Pair { get; set; }
    }
}
