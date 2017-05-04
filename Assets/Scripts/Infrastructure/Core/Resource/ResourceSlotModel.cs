using System.Collections;
using System.Collections.Generic;
using System;

namespace Infrastructure.Core.Resource
{
    [Serializable]
    public class ResourceSlotModel : Infrastructure.Base.Model.Model
    {
        public string name;
        public int amount;
        public int buyPrice;
        public int sellPrice;
    }
}