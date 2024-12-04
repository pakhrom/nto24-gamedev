using System;
using Code.Scripts.ScriptableObjects;
using UnityEngine;

namespace Code.Scripts
{
    public class SatelliteManager : MonoBehaviour
    {
        [SerializeField] private Planet _planet;
        [SerializeField] private Satellite _satellite;
        [SerializeField] private Controller2D _controller;

        [SerializeField] private Transform _ground;

        private void Start()
        {
            _controller.enabled = false;
            _satellite.Init(_planet);
        }

        private void Update()
        {
            if (!_satellite.IsInitialized()) return;
            _ground.localScale = new Vector3(2f * Mathf.PI * _satellite.radius, 1f, 1f);
            _controller.enabled = true;
            // TODO: Disable loading screen
            return;
        }
    }
}
