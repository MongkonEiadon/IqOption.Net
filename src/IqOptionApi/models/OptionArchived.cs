using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IqOptionApi.Models
{
    /// <summary>
    ///     The result after position (Options) closed
    /// </summary>
    public class OptionArchived
    {
        /// <summary>
        ///     The result of closed position
        /// </summary>
        [JsonProperty("win")]
        public string Win { get; set; }

        /// <summary>
        ///     The identifiers
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        ///     Indicate if the archived was practice mode
        /// </summary>
        [JsonProperty("is_demo")]
        public bool IsDemo { get; set; }

        /// <summary>
        ///     The created archived
        /// </summary>
        [JsonProperty("created")]
        public DateTimeOffset Created { get; set; }

        /// <summary>
        ///     The Active pair of the archived
        /// </summary>
        [JsonProperty("active")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ActivePair Active { get; set; }

        /// <summary>
        ///     The currency
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        ///     The result direction
        /// </summary>
        [JsonProperty("direction")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderDirection Direction { get; set; }
    }
}