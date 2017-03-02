using System.Collections;
using System.Collections.Generic;
using System;

namespace Infrastructure.Core.Resource
{
    [Serializable]
    public class BuyResourceModel : Infrastructure.Base.Model.Model
    {
        public string name;
        public int amount;
    }
}