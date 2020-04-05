using Newtonsoft.Json;

namespace iqoptionapi.models
{
    public class ClosePosition
    {
        [JsonProperty("position_id")]
        public string PositionId { get; set; }
    }

    public class PositionClosed
    {
        [JsonProperty("id")]
        public string PositionId { get; set; }
    }

    public class ClosePositionBatch
    {
        [JsonProperty("position_ids")]
        public int[] PositionIds { get; set; }
    }
}
