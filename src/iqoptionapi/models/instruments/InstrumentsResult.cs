using System;
using System.Collections;
using System.Collections.Generic;
using iqoptionapi.converters.JsonConverters;
using Newtonsoft.Json;

namespace iqoptionapi.models {
    public partial class InstrumentsResult {
        [JsonProperty("user_group_id")]
        public int UserGroupId { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(InstrumentTypeJsonConverter))]
        public InstrumentType Type { get; set; }

        [JsonProperty("instruments")]
        public Instrument[] Instruments { get; set; }
    }
}