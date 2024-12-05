using System;
using Code.Scripts.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Scripts
{
    public class SatelliteManager : MonoBehaviour
    {
        [SerializeField] private Planet _planet;
        [SerializeField] private Satellite _satellite;
        [SerializeField] private Controller2D _controller;

        [SerializeField] private Transform _ground;

        private bool _isLoadingDone;

        private void Start()
        {
            _controller.enabled = false;
            _satellite.Init(_planet);
        }

        private void Update()
        {
            if (_satellite.IsInitialized() && _isLoadingDone) return;
            _ground.localScale = new Vector3(2f * Mathf.PI * _satellite.radius + 18f, 1f, 1f); // Периметр спутника
            Physics2D.gravity = new Vector2(0f, _satellite.gravity); // Гравитация на спутнике
            
            _isLoadingDone = true;
            _controller.enabled = true;
            // TODO: Disable loading screen
        }
    }
}
