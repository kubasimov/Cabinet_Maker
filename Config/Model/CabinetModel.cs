using Newtonsoft.Json;

namespace Config.Model
{
    internal class CabinetModel
    {
        [JsonProperty]
        internal string name;
        [JsonProperty]
        internal int height;
        [JsonProperty]
        internal int width;
        [JsonProperty]
        internal int depth;
        [JsonProperty]
        internal int sizeElement;
        [JsonProperty]
        internal int back;
        [JsonProperty]
        internal Slots Slots;
        [JsonProperty]
        internal int enumFront;
    }

    internal class Slots
    {
        [JsonProperty]
        internal int Top;
        [JsonProperty]
        internal int Bottom;
        [JsonProperty]
        internal int Left;
        [JsonProperty]
        internal int Right;
        [JsonProperty]
        internal int BetweenVertically;
        [JsonProperty]
        internal int BetweenHorizontally;
        [JsonProperty]
        internal int BetweenCabinet;
    }

}
