using System;
using System.Collections.Generic;
using System.Linq;
using Code.Scripts.ScriptableObjects;
using UnityEngine;

namespace Code.Scripts
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private bool _isLocal;

        public OreList oreList;

        [Header("Local Inventory properties")] 
        [SerializeField] private int _maxCapacity;

        public Dictionary<string, int> inventory;
        private int _currentFullness;
        
        public int GetCurrentFullness() { return _currentFullness; }

        private void Start()
        {
            inventory = new Dictionary<string, int>();

            foreach (var ore in oreList.ores)
            {
                if (_isLocal)
                {
                    inventory.Add(ore.name, 0);
                }
                else
                {
                    // Load from save
                    inventory.Add(ore.name, 0);
                }
            }
        }

        public void AddOre(OreIngot oreIngot, int amount)
        {
            int amountToAdd;
            if (_isLocal)
            {
                if (_currentFullness == _maxCapacity) return;
                amountToAdd = Math.Min(amount, _maxCapacity - _currentFullness);
                inventory[oreIngot.ore.name] += amountToAdd;
                _currentFullness += amountToAdd;
                oreIngot.Remove();
            }
            else
            {
                amountToAdd = amount;
                inventory[oreIngot.ore.name] += amountToAdd;
            }
            
            Debug.Log($"Added {amountToAdd} {oreIngot.ore.name} ore to Inventory. Local: {_isLocal}");
        }
        
        public void AddOre(Ore ore, int amount)
        {
            int amountToAdd;
            if (_isLocal)
            {
                if (_currentFullness == _maxCapacity) return;
                amountToAdd = Math.Min(amount, _maxCapacity - _currentFullness);
                inventory[ore.name] += amountToAdd;
                _currentFullness += amountToAdd;
            }
            else
            {
                amountToAdd = amount;
                inventory[ore.name] += amountToAdd;
            }
            
            Debug.Log($"Added {amountToAdd} {ore.name} ore to Inventory. Local: {_isLocal}");
        }

        public void RemoveOre(OreIngot oreIngot, int amount)
        {
            if (inventory[oreIngot.ore.name] - amount < 0)
            {
                Debug.LogError($"Tried to remove more ore than there is in the Inventory. Local: {_isLocal}");
                return;
            }
            
            inventory[oreIngot.ore.name] -= amount;
            Debug.Log($"Removed {amount} {oreIngot.ore.name} ore from Inventory. Local: {_isLocal}");
        }
        
        public void RemoveOre(Ore ore, int amount)
        {
            if (inventory[ore.name] - amount < 0)
            {
                Debug.LogError($"Tried to remove more ore than there is in the Inventory. Local: {_isLocal}");
                return;
            }
            
            inventory[ore.name] -= amount;
            Debug.Log($"Removed {amount} {ore.name} ore from Inventory. Local: {_isLocal}");
        }
        
        public void RemoveOre(int oreID, int amount)
        {
            if (inventory[oreList.ores[oreID].name] - amount < 0)
            {
                Debug.LogError($"Tried to remove more ore than there is in the Inventory. Local: {_isLocal}");
                return;
            }
            
            inventory[oreList.ores[oreID].name] -= amount;
            Debug.Log($"Removed {amount} {oreList.ores[oreID].name} ore from Inventory. Local: {_isLocal}");
        }
    }
}
