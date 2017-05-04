using System.Collections;
using System.Collections.Generic;
using Infrastructure.Core.Resource;
using System;
using Newtonsoft.Json;

namespace Infrastructure.Core.Star
{
    [Serializable]
    public class NodeModel : Infrastructure.Base.Model.Model
    {
        [JsonProperty("name")]
        public string name;
        public float coordX;
        public float coordY;
    }
}