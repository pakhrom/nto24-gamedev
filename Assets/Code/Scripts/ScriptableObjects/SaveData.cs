using UnityEngine;

namespace Code.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Default Save Data", menuName = "Default Save Data")]
    public class SaveData : ScriptableObject
    {
        public string playerName;
        public bool tutorialCompleted;
        
        [Header("Player Stats")]
        public int money;
        public float health = 100f;
        public float inRocketDamageMultiplier = 0.9f;
        public int upgrade1Amount = 0;
        public float movementSpeed = 3f;
        public float miningToolDamage = 1f;
        public int upgrade2Amount = 0;
        public int inventoryMaxCapacity = 10;
        public int upgrade3Amount = 0;
        public Planet currentPlanet;

        [Header("Rocket Stats")] 
        public float shootDelay = 0.35f;
        public int upgrade4Amount = 0;
        public float fuelCapacity = 200f;
        public int upgrade5Amount = 0;
    }
}