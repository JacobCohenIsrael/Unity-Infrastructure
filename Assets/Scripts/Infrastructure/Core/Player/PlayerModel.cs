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

        public ShipModel getActiveShip()
        {
            return ships[activeShipIndex];
        }
    }
}

