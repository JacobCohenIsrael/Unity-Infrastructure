using System;
using Infrastructure.Core.Market;
using Infrastructure.Core.Star;
using Newtonsoft.Json;

namespace Infrastructure.Core.Node
{
    [Serializable]
    public class NodeModel : Base.Model.Model
    {
        [JsonProperty("name")]
        public string name;
        public float coordX;
        public float coordY;

        [JsonProperty("sprite")]
        public string Sprite;

        [JsonProperty("star")]
        public StarModel star;

        public MarketModel market;

        public bool HasStar()
        {
            return star != null;
        }

        public bool HasMarket()
        {
            return market != null;
        }
    }
}