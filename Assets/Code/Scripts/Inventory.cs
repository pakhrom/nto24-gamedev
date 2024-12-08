using System;
using System.Collections.Generic;
using Code.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Scripts
{
    public class Inventory : MonoBehaviour
    {
        [FormerlySerializedAs("_isLocal")] [SerializeField] private bool _isRocketInventory;

        public OreList oreList;

        [Header("Local Inventory properties")] 
        [SerializeField] private int _maxCapacity;
        [SerializeField] private GameObject _inventoryFullMessage;

        public Dictionary<string, int> inventory;
        private int _currentFullness;
        
        public int GetCurrentFullness() { return _currentFullness; }

        private void Start()
        {
            inventory = new Dictionary<string, int>();

            foreach (var ore in oreList.ores)
            {
                inventory.Add(ore.name, 0);
            }
        }

        public void AddOre(OreIngot oreIngot, int amount)
        {
            int amountToAdd = 0;
            if (!_isRocketInventory)
            {
                if (_currentFullness == _maxCapacity) return;
                amountToAdd = Math.Min(amount, _maxCapacity - _currentFullness);
                inventory[oreIngot.ore.name] += amountToAdd;
                _currentFullness += amountToAdd;
                oreIngot.Remove();
            }
            else if (_isRocketInventory)
            {
                amountToAdd = amount;
                inventory[oreIngot.ore.name] += amountToAdd;
            }

            if (_inventoryFullMessage && _currentFullness == _maxCapacity) _inventoryFullMessage.SetActive(true);
            
            Debug.Log($"Added {amountToAdd} {oreIngot.ore.name} ore to Inventory. Rocket: {_isRocketInventory}.");
        }
        
        public void AddOre(Ore ore, int amount)
        {
            int amountToAdd = 0;
            if (!_isRocketInventory)
            {
                if (_currentFullness == _maxCapacity) return;
                amountToAdd = Math.Min(amount, _maxCapacity - _currentFullness);
                inventory[ore.name] += amountToAdd;
                _currentFullness += amountToAdd;
            }
            else if (_isRocketInventory)
            {
                amountToAdd = amount;
                inventory[ore.name] += amountToAdd;
            }
            
            if (_inventoryFullMessage && _currentFullness == _maxCapacity) _inventoryFullMessage.SetActive(true);
            
            Debug.Log($"Added {amountToAdd} {ore.name} ore to Inventory. Rocket: {_isRocketInventory}.");
        }

        public void RemoveOre(OreIngot oreIngot, int amount)
        {
            if (inventory[oreIngot.ore.name] - amount < 0)
            {
                Debug.LogError($"Tried to remove more ore than there is in the Inventory. Rocket: {_isRocketInventory}.");
                return;
            }
            
            if (_inventoryFullMessage && _currentFullness != _maxCapacity) _inventoryFullMessage.SetActive(false);
            
            inventory[oreIngot.ore.name] -= amount;
            Debug.Log($"Removed {amount} {oreIngot.ore.name} ore from Inventory. Rocket: {_isRocketInventory}.");
        }
        
        public void RemoveOre(Ore ore, int amount)
        {
            if (inventory[ore.name] - amount < 0)
            {
                Debug.LogError($"Tried to remove more ore than there is in the Inventory. Rocket: {_isRocketInventory}.");
                return;
            }
            
            if (_inventoryFullMessage && _currentFullness != _maxCapacity) _inventoryFullMessage.SetActive(false);
            
            inventory[ore.name] -= amount;
            if (!_isRocketInventory) _currentFullness -= amount;
            Debug.Log($"Removed {amount} {ore.name} ore from Inventory. Rocket: {_isRocketInventory}.");
        }
        
        public void RemoveOre(int oreID, int amount)
        {
            if (inventory[oreList.ores[oreID].name] - amount < 0)
            {
                Debug.LogError($"Tried to remove more ore than there is in the Inventory. Local: {_isRocketInventory}");
                return;
            }
            
            if (_inventoryFullMessage && _currentFullness != _maxCapacity) _inventoryFullMessage.SetActive(false);
            
            inventory[oreList.ores[oreID].name] -= amount;
            Debug.Log($"Removed {amount} {oreList.ores[oreID].name} ore from Inventory. Local: {_isRocketInventory}");
        }
    }
}
