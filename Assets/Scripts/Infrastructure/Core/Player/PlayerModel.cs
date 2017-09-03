using System.Collections;
using Infrastructure.Core.Ship;
using System;

namespace Infrastructure.Core.Player
{
    [Serializable]
    public class PlayerModel : Infrastructure.Base.Model.Model
    {
        public int id;
        public string firstName;
        public string lastName;
        public string token;

		public string currentNodeName;
        public int activeShipIndex;
        public ShipModel[] ships;
        public bool isLanded;
        public string homePlanetName;
        public int credits;

        public ShipModel getActiveShip()
        {
            return ships[activeShipIndex];
        }


        public bool HasActiveShip()
        {
            if (null == ships[activeShipIndex])
            {
                return false;
            }
            return true;
        }

        public float GetJumpDistance()
        {
            return getActiveShip().GetShipJumpDistance();
        }

        public bool hasHangerInNode(string nodeName)
        {
            return homePlanetName == nodeName;
        }
    }
}