using System;
using Infrastructure.Core.Resource;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Infrastructure.Core.Star.Events
{
    public class UpdateResourceAmountEvent
    {
        public string starName;

        public string resourceName;

        public int newAmount;
    }
}

