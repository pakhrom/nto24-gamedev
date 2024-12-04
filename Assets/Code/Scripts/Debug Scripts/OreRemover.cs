using UnityEngine;

namespace Code.Scripts.Debug_Scripts
{
    public class OreRemover : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        
        public void RemoveOre()
        {
            _inventory.RemoveOre(0, 1);
        }
    }
}
