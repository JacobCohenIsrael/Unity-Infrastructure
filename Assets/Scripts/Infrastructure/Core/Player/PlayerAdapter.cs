using System.Collections;
using Infrastructure.Base.Service;
using Infrastructure.Base.Service.Contracts;
using Infrastructure.Core.Star;
using Infrastructure.Core.Ship.Part;
using Infrastructure.Core.Resource;

namespace Infrastructure.Core.Player
{
    public class PlayerAdapter : IServiceProvider
    {
        protected PlayerModel player;

        public PlayerAdapter(ServiceManager serviceManager)
        {
            PlayerModel player = new PlayerModel();
            player.id = 123;
            player.currentNodeId = 1;
            player.sessionId = "test";
            player.ships = new Ship.ShipModel[3];
            Ship.ShipModel ship = new Ship.ShipModel();
            ship.AddPart(Infrastructure.Core.Ship.ShipParts.Engine, new BasicEngine());
            ship.AddPart(Infrastructure.Core.Ship.ShipParts.CargoCapacitor, new BasicCargoCapacitor());
            ship.currentHullAmount = 1;
            player.activeShipIndex = 0;
            player.ships[player.activeShipIndex] = ship;
            player.isLanded = true;
            player.homePlanetId = 1;
            player.credits = 50000;
            this.player = player;
        }

        public PlayerModel getById(int id)
        {
            return player;
        }

        public bool jumpPlayerToStar(PlayerModel player, StarModel star)
        {
            if (this.player.currentNodeId == star.id)
            {
                return false;
            }
            this.player.currentNodeId = star.id;
            this.player.getActiveShip().currentEnergyAmount -= 10;
            return true;
        }

        public bool landPlayerOnStar(PlayerModel player)
        {
            this.player.isLanded = true;
            return true;
        }

        public bool departPlayerFromStar(PlayerModel player)
        {
            if (this.player.getActiveShip() == null)
            {
                return false;
            }
            
            this.player.isLanded = false;
            return true;
        }

        public bool BuyResource(PlayerModel player, ResourceSlotModel resourceSlot)
        {
            if (resourceSlot.amount > 0)
            {
                player.credits -= resourceSlot.buyPrice;
                if (player.getActiveShip().AddResource(resourceSlot.resouce.name, 1))
                {
                    resourceSlot.amount--;
                    return true;
                }
            }
            return false;
        }

        public bool SellResource(PlayerModel player, ResourceSlotModel resourceSlot)
        {
            player.credits += resourceSlot.sellPrice;
            resourceSlot.amount++;
            return true;
        }
    }
}

