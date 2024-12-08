using System;
using System.Collections.Generic;
using Code.Scripts.ScriptableObjects;
using Code.Scripts.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Scripts
{
    public class SatelliteManager : MonoBehaviour
    {
        [SerializeField] private Planet _planet;
        [SerializeField] private Satellite _satellite;
        [SerializeField] private Controller2D _controller;
        [SerializeField] private SceneLoader _sceneLoader;

        [SerializeField] private Ground _ground;

        [SerializeField] private Vector3 _startingPoint;
        
        [NonSerialized] public bool isLoadingDone;

        [NonSerialized] public List<Transform> oreObjects;
        
        public Planet GetPlanet() { return _planet; }
        public Satellite GetSatellite() { return _satellite; }
        
        public Controller2D GetController() { return _controller; }

        private void Start()
        {
            oreObjects = new List<Transform>();
            _controller.enabled = false;
            _satellite.Init(_planet);
        }

        private void Update()
        {
            if (isLoadingDone) return;
            if (!_satellite.IsInitialized()) return;
            Physics2D.gravity = new Vector2(0f, _satellite.gravity); // Гравитация на спутнике

            var oreToSpawnCount = _satellite.oreCount;
            var distanceBetweenOres = _satellite.perimeter / (oreToSpawnCount + 1);
            for (int i = 0; i < oreToSpawnCount; ++i)
            { 
                if (i * distanceBetweenOres <= 0f && 0f <= i * distanceBetweenOres + distanceBetweenOres - 1f) continue;
                Ore oreToSpawn = _planet.ores[Random.Range(0, _planet.ores.Count)];
                var ore = Instantiate(oreToSpawn.orePrefab,
                    _startingPoint + new Vector3(i * distanceBetweenOres + Random.Range(0f, distanceBetweenOres - 1f),
                        0f, 0f), Quaternion.identity);
                oreObjects.Add(ore.transform);
            }
            
            isLoadingDone = true;
            _controller.enabled = true;
            
            _sceneLoader.HideLoadingScene();
        }

        public void PlayerDie()
        {
            Debug.Log("Player died");
        }
    }
}
