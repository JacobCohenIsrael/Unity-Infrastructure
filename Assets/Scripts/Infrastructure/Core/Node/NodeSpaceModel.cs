using System.Collections;
using Infrastructure.Core.Ship;
using System;
using System.Collections.Generic;

namespace Infrastructure.Core.Player
{
    [Serializable]
    public class NodeSpaceModel : Infrastructure.Base.Model.Model
    {
        public Dictionary<int, ShipModel> ships;
    }
}