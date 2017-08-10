using Newtonsoft.Json;
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

