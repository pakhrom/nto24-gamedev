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
        public float health;
        public float inRocketDamageMultiplier;
        public float movementSpeed;
        public float miningToolDamage;
        public Planet currentPlanet;

        [Header("Rocket Stats")] 
        public float shootDelay;
        public float fuelCapacity;
    }
}