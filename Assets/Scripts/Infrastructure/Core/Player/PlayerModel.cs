using System.Collections;
using Infrastructure.Core.Ship;

namespace Infrastructure.Core.Player
{
    public class PlayerModel : Infrastructure.Base.Model.Model
    {
        public int id;
        public string firstName;
        public string lastName;
        public string sessionId;

		public int currentNodeId;
        public int activeShipIndex;
        public ShipModel[] ships;
        public bool isLanded;
        public int homePlanetId;
        public int credits;

        public ShipModel getActiveShip()
        {
            return ships[activeShipIndex];
        }


        public bool hasActiveShip()
        {
            if (null == ships[activeShipIndex])
            {
                return false;
            }
            return true;
        }

        public float getJumpDistance()
        {
            return getActiveShip().shipStats[ShipStats.JumpDistance];
        }
    }
}

