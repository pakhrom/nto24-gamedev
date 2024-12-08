using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts
{
    public class Ground : MonoBehaviour
    {
        [SerializeField] private SatelliteManager _satelliteManager;
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _player;
        [SerializeField] private Transform _rocket;
        [SerializeField] private Transform _ground1;
        [SerializeField] private Transform _ground2;
        [SerializeField] private Transform _ground3;
        [SerializeField] private float _groundWidth;
        
        private float _cameraWidth;
        private float _satellitePerimeter;
        private bool _isPerimeterSet;
        
        private void Start()
        {
            _camera = Camera.main;
            
            _ground1.localScale = new Vector3(_groundWidth, _ground1.localScale.y, _ground1.localScale.z);
            _ground2.localScale = new Vector3(_groundWidth, _ground2.localScale.y, _ground2.localScale.z);
            _ground3.localScale = new Vector3(_groundWidth, _ground3.localScale.y, _ground3.localScale.z);
            
            _ground2.position = _ground1.position - new Vector3(_groundWidth, 0f, 0f);
            _ground3.position = _ground1.position + new Vector3(_groundWidth, 0f, 0f);
        }

        private void Update()
        {
            if (!_camera || !_satelliteManager.isLoadingDone) return;

            if (!_isPerimeterSet)
            {
                _satellitePerimeter = _satelliteManager.GetSatellite().perimeter;

                foreach (var oreObject in _satelliteManager.oreObjects)
                {
                    if (_player.position.x - oreObject.position.x >= _satellitePerimeter / 2f)
                        oreObject.position += new Vector3(_satellitePerimeter, 0f, 0f);
                    else if (oreObject.position.x - _player.position.x >= _satellitePerimeter / 2f)
                        oreObject.position -= new Vector3(_satellitePerimeter, 0f, 0f);
                }
                
                _isPerimeterSet = true;
            }
            
            _cameraWidth = _camera.aspect * _camera.orthographicSize * 2;
            
            // ground placement
            if (_ground1.position.x - _groundWidth / 2f <= _player.position.x &&
                _player.position.x <= _ground1.position.x + _groundWidth / 2f)
            {
                if (_camera.transform.position.x + _cameraWidth / 2f >= _ground1.position.x + _groundWidth / 2f)
                {
                    _ground3.position = _ground1.position + new Vector3(_groundWidth, 0f, 0f);
                }
                else if (_camera.transform.position.x - _cameraWidth / 2f <= _ground1.position.x - _groundWidth / 2f)
                {
                    _ground2.position = _ground1.position - new Vector3(_groundWidth, 0f, 0f);
                }
            }
            else if (_ground2.position.x - _groundWidth / 2f <= _player.position.x &&
                     _player.position.x <= _ground2.position.x + _groundWidth / 2f)
            {
                if (_camera.transform.position.x + _cameraWidth / 2f >= _ground2.position.x + _groundWidth / 2f)
                {
                    _ground3.position = _ground2.position + new Vector3(_groundWidth, 0f, 0f);
                }
                else if (_camera.transform.position.x - _cameraWidth / 2f <= _ground2.position.x - _groundWidth / 2f)
                {
                    _ground1.position = _ground2.position - new Vector3(_groundWidth, 0f, 0f);
                }
            }
            else if (_ground3.position.x - _groundWidth / 2f <= _player.position.x &&
                     _player.position.x <= _ground3.position.x + _groundWidth / 2f)
            {
                if (_camera.transform.position.x + _cameraWidth / 2f >= _ground2.position.x + _groundWidth / 2f)
                {
                    _ground1.position = _ground2.position + new Vector3(_groundWidth, 0f, 0f);
                }
                else if (_camera.transform.position.x - _cameraWidth / 2f <= _ground2.position.x - _groundWidth / 2f)
                {
                    _ground2.position = _ground2.position - new Vector3(_groundWidth, 0f, 0f);
                }
            }
            
            // rocket and ore placement
            if (_player.position.x - _rocket.position.x >= _satellitePerimeter / 2f)
            {
                _rocket.position += new Vector3(_satellitePerimeter, 0f, 0f);
            }
            else if (_rocket.position.x - _player.position.x >= _satellitePerimeter / 2f)
            {
                _rocket.position -= new Vector3(_satellitePerimeter, 0f, 0f);
            }
            
            List<Transform> oreObjects = _satelliteManager.oreObjects;
            foreach (var oreObject in oreObjects)
            {
                if (_player.position.x - oreObject.position.x >= _satellitePerimeter / 2f)
                    oreObject.position += new Vector3(_satellitePerimeter, 0f, 0f);
                else if (oreObject.position.x - _player.position.x >= _satellitePerimeter / 2f)
                    oreObject.position -= new Vector3(_satellitePerimeter, 0f, 0f);
            }
        }
    }
}