using System.Collections;
using System.Collections.Generic;

namespace Infrastructure.Core.Resource
{
    public class ResourceSlotModel : Infrastructure.Base.Model.Model
    {
        public ResourceModel resouce;
        public int amount;
        public int buyPrice;
        public int sellPrice;
    }
}