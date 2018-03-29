using System;
using Newtonsoft.Json;

namespace iqoptionapi.unit.converters.JsonConverters {
    internal class JsonReaderTest<T> : JsonReader {

        private Func<T> _func;
        public void SetValue(Func<T> func) {
            _func = func;
        }

        public override object Value => _func();

        public override bool Read()
        {
            return true;
        }

    }
}