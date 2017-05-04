using System;
using Infrastructure.Core.Resource;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace Infrastructure.Core.Star.Events
{
    public class UpdateStarsListEvent
    {
        public bool success;

        [JsonProperty("starsList")]
        public Dictionary<string, StarModel> starsList;
    }
}

