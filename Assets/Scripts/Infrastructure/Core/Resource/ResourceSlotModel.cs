using System.Collections;
using System.Collections.Generic;
using System;

namespace Infrastructure.Core.Resource
{
    [Serializable]
    public class ResourceSlotModel : Base.Model.Model
    {
        public string Name;
        public int BuyPrice;
        public int SellPrice;
    }
}