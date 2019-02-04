using Newtonsoft.Json;

namespace IqOptionApi.Unit.converters.JsonConverters {
    internal class JsonWriterTest : JsonWriter
    {
        public string AssertionObject { get; private set; }


        public JsonWriterTest()
        {
        }


        public override void Flush()
        {
            // throw new NotImplementedException();
        }

        public override void WriteRawValue(string json)
        {
            AssertionObject = json;
            base.WriteRawValue(json);
        }

        public void Received(object obj)
        {
        }
    }
}