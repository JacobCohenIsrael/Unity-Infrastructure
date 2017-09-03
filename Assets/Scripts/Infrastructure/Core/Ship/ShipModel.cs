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
        [JsonProperty("shipType")]
        private string _shipType;
        
        [JsonProperty("shipClass")]
        private string _shipClass;

        [JsonProperty("currentStats")]
        private CurrentStats _currentStats;

        [JsonProperty("baseStats")]
        private Stats _baseStats;
        
        [JsonProperty("maxStats")]
        private Stats _maxStats;

        [JsonProperty("shipParts")]
        private ShipPart[] _shipParts;

        [JsonProperty("shipCargo")]
        private Dictionary<string, int> _shipCargo;

        [JsonProperty("shipSlots")]
        private Dictionary<string, int> _shipSlots;

        public bool AddResource(string resourceName, int amount)
        {
            var cargoHold = _shipCargo.Sum (x => x.Value);
            if (cargoHold >= _maxStats.CargoCapacity) return false;
            if (!_shipCargo.ContainsKey(resourceName))
            {
                _shipCargo.Add(resourceName, amount);
            }
            else
            {
                _shipCargo[resourceName] += amount;
            }
            return true;
        }

        public bool RemoveResource(string resourceName, int amount)
        {
            if (!_shipCargo.ContainsKey(resourceName) || _shipCargo[resourceName] < amount) return false;
            _shipCargo[resourceName]--;
            return true;
        }

        public int GetMaxEnergyCapacity()
        {
            return _maxStats.EnergyCapacity;
        }

        public int GetMaxEnergyRegen()
        {
            return _maxStats.EnergyRegen;
        }

        public int GetShipJumpDistance()
        {
            return  _maxStats.JumpDistance;
        }

        public int GetCurrentHullAmount()
        {
            return _currentStats.Hull;
        }

        public int GetCurrentEnergy()
        {
            return _currentStats.Energy;
        }

        public int GetCurrentShield()
        {
            return _currentStats.Shield;
        }
        
        public int GetCurrentCargo()
        {
            return _currentStats.Cargo;
        }

        public string GetShipType()
        {
            return _shipType;
        }

        public string GetShipClass()
        {
            return _shipClass;
        }

        public int GetMaxCargoCapacity()
        {
            return _maxStats.CargoCapacity;
        }

        public Dictionary<string, int> GetCargoHold()
        {
            return _shipCargo;
        }

        public int GetResourceAmount(string resourceName)
        {
            return _shipCargo.ContainsKey(resourceName) ? _shipCargo[resourceName] : 0;
        }

        public void SetCurrentEnergy(int newEnergy)
        {
            _currentStats.Energy = newEnergy;
        }
    }
}

