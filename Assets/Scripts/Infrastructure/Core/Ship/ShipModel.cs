using System;
using System.Collections.Generic;
using Infrastructure.Core.Resource;
using System.Linq;
using System.Collections;
using Newtonsoft.Json;

namespace Infrastructure.Core.Ship
{
    [Serializable]
    public class ShipModel : Infrastructure.Base.Model.Model
    {
        public int id;

        [JsonProperty("shipType")]
        protected string shipType;
        
        [JsonProperty("shipClass")]
        protected string shipClass;

        [JsonProperty("currentHullAmount")]
        private int currentHullAmount;
        public int currentShieldAmount;
        public float currentEnergyAmount;

        public ShipStats cachedShipStats;

        public ShipPart[] shipParts;

        public Dictionary<string, int> shipCargo;

        public ShipModel()
        {
            cachedShipStats = new ShipStats();
        }

        public bool AddResource(string resourceName, int amount)
        {
            int cargoHold = shipCargo.Sum (x => x.Value);
            if (cargoHold < cachedShipStats.CargoCapacity)
            {
                if (!shipCargo.ContainsKey(resourceName))
                {
                    shipCargo.Add(resourceName, amount);
                }
                else
                {
                    shipCargo[resourceName] += amount;
                }
                return true;
            }
            return false;
        }

        public bool RemoveResource(string resourceName, int amount)
        {
            if (shipCargo.ContainsKey(resourceName) && shipCargo[resourceName] >= amount )
            {
                shipCargo[resourceName]--;
                return true;
            }
            return false;
        }

        public int getMaxEnergyCapacity()
        {
            return cachedShipStats.EnergyCapacity;
        }

        public int getMaxEnergyRegen()
        {
            return cachedShipStats.EnergyRegen;
        }

        public int getShipJumpDistance()
        {
            return  cachedShipStats.JumpDistance;
        }

        public int GetCurrentHullAmount()
        {
            return currentHullAmount;
        }

        public string GetShipType()
        {
            return shipType;
        }

        public string GetShipClass()
        {
            return shipClass;
        }

        public int GetShipCargoCapacity()
        {
            return cachedShipStats.CargoCapacity;
        }

        public int GetShipCargoHold()
        {
            return shipCargo.Sum(x => x.Value);
        }
    }
}

