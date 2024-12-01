using System;
using UnityEngine;

namespace Code.Scripts.Components
{
    public class CameraFollower : MonoBehaviour
    {
        public Transform defaultTarget;
        public float defaultLerpMultiplier;

        private float _defaultCameraSize;
        
        [NonSerialized] public Transform target;
        [NonSerialized] public float lerpMultiplier;
        [NonSerialized] public float cameraSize;

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
        }

        private void FixedUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, target.position, lerpMultiplier);
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
