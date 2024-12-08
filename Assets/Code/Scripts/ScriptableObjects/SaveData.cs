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
        public float movementSpeed = 3f;
        public float miningToolDamage = 1f;
        public Planet currentPlanet;

        [Header("Rocket Stats")] 
        public float shootDelay = 0.35f;
        public float fuelCapacity = 200f;
    }
}