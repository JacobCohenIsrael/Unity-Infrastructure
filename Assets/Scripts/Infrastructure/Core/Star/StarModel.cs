﻿using System.Collections;
using System.Collections.Generic;
using Infrastructure.Core.Resource;
using System;
using Infrastructure.Core.Node;

namespace Infrastructure.Core.Star
{
    [Serializable]
    public class StarModel : NodeModel
	{
        public Dictionary<string, ResourceSlotModel> resourceList;

        public StarModel()
        {
            resourceList = new Dictionary<string, ResourceSlotModel>();
        }
	}
}