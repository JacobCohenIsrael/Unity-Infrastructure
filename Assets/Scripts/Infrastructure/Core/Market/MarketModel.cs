using System;
using System.Collections.Generic;
using Infrastructure.Core.Resource;

namespace Infrastructure.Core.Market
{
    [Serializable]
    public class MarketModel
    {
        public Dictionary<string, ResourceSlotModel> resourceList;
    }
}