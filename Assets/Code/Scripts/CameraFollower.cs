using System;
using UnityEngine;

namespace Code.Scripts
{
    public class CameraFollower : MonoBehaviour
    {
        public Transform defaultTarget;
        public float defaultLerpMultiplier;
        public bool followX = true;
        public bool followY = true;

        private float _defaultCameraSize;
        
        [NonSerialized] public Transform target;
        [NonSerialized] public float lerpMultiplier;
        [NonSerialized] public float cameraSize;

        private int _followXFlag = 1;
        private int _followYFlag = 1;
        private Camera _camera;
        private float _cameraZ;
        
        private void Awake()
        {
            _camera = gameObject.GetComponent<Camera>();
            _cameraZ = transform.position.z;

            _defaultCameraSize = _camera.orthographicSize;
            
            target = defaultTarget;
            lerpMultiplier = defaultLerpMultiplier;
            cameraSize = _defaultCameraSize;

            if (!followX) _followXFlag = 0;
            if (!followY) _followYFlag = 0;
        }

        private void FixedUpdate()
        {
            var targetPosition = new Vector3(_followXFlag * target.position.x, _followYFlag * target.position.y,
                target.position.z);
            
            transform.position = Vector3.Lerp(transform.position, targetPosition, lerpMultiplier);
            transform.position = new Vector3(transform.position.x, transform.position.y, _cameraZ);
        }

        public void RestoreDefaults()
        {
            target = defaultTarget;
            lerpMultiplier = defaultLerpMultiplier;
            _camera.orthographicSize = _defaultCameraSize;
        }
    }
}
