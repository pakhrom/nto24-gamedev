using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Debug Satellite", menuName = "Debug Satellite")]
    public class Satellite : ScriptableObject
    {
        [NonSerialized] public float radius;
        [NonSerialized] public float gravity;
        [NonSerialized] public int oreCount;

        [SerializeField] private bool _debug;
        
        private bool _isInitialized;

        public bool IsInitialized()
        {
            return _isInitialized;
        }
        
        public void Init(Planet planet)
        {
            if (_isInitialized && !_debug) return;
            
            radius = Random.Range(planet.satelliteMinRadius, planet.satelliteMaxRadius);
            gravity = Random.Range(planet.satelliteMinGravity, planet.satelliteMaxGravity);
            oreCount = Random.Range(planet.satelliteMinOreCount, planet.satelliteMaxOreCount + 1);

            _isInitialized = true;
        }
    }
}