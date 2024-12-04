using UnityEngine;

namespace Code.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Ore", menuName = "Ore")]
    public class Ore : ScriptableObject
    {
        public new string name;
        public int value;
        public GameObject ingotPrefab;
        
        [Tooltip("How many hits until drop")]
        public float minOreDropThreshold;
        public float maxOreDropThreshold;
        public int minDropCount;
        public int maxDropCount;
        public int minOrePerDrop;
        public int maxOrePerDrop;
    }
}
