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

        [SerializeField] private OreList _oreList;

        [Header("Local Inventory properties")] 
        [SerializeField] private int _maxCapacity;

        private Dictionary<string, int> _inventory;
        private int _currentFullness;

        private void Start()
        {
            _inventory = new Dictionary<string, int>();

            foreach (var ore in _oreList.ores.Where(ore => _isLocal))
            {
                if (_isLocal)
                {
                    _inventory.Add(ore.name, 0);
                }
                else
                {
                    // Load from save
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
                _inventory[oreIngot.ore.name] += amountToAdd;
                _currentFullness += amountToAdd;
                oreIngot.Remove();
            }
            else
            {
                amountToAdd = amount;
                _inventory[oreIngot.ore.name] += amountToAdd;
            }
            
            Debug.Log($"Added {amountToAdd} {oreIngot.ore.name} ore to Inventory. Local: {_isLocal}");
        }

        public void RemoveOre(OreIngot oreIngot, int amount)
        {
            if (_inventory[oreIngot.ore.name] - amount < 0)
            {
                Debug.LogError($"Tried to remove more ore than there is in the Inventory. Local: {_isLocal}");
                return;
            }

            _inventory[oreIngot.ore.name] -= amount;
            Debug.Log($"Removed {amount} {oreIngot.ore.name} ore from Inventory. Local: {_isLocal}");
        }
        
        public void RemoveOre(int oreID, int amount)
        {
            if (_inventory[_oreList.ores[oreID].name] - amount < 0)
            {
                Debug.LogError($"Tried to remove more ore than there is in the Inventory. Local: {_isLocal}");
                return;
            }
            
            _inventory[_oreList.ores[oreID].name] -= amount;
            Debug.Log($"Removed {amount} {_oreList.ores[oreID].name} ore from Inventory. Local: {_isLocal}");
        }
    }
}
