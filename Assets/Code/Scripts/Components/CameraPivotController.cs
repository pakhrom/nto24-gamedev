using UnityEngine;

namespace Code.Scripts.Components
{
    public class CameraPivotController : MonoBehaviour
    {
        [Header("READ SOURCE SCRIPT")]
        [SerializeField] private bool _isArea; /* BUG: DO NOT place two area triggers near each other! 
                                                  OnTriggerEnter2D of the first area trigger can be called earlier than
                                                  OnTriggerExit2D of the second area trigger, which results in 
                                                  CameraFollower being reset to default properties.
                                               */
        
        [Header("Settings")]
        [SerializeField] private Transform _newTarget;
        [SerializeField] private float _newLerpMultiplier;
        [SerializeField] private float _newCameraSize;
        [SerializeField] private string _playerTag;
        
        private CameraFollower _cameraFollower;
        private Camera _camera;
        
        private const int IgnoreRaycastLayer = 2;

        private void Awake()
        {
            gameObject.layer = IgnoreRaycastLayer;
            
            if (Camera.main == null) return;
            _camera = Camera.main.GetComponent<Camera>();
            _cameraFollower = Camera.main.GetComponent<CameraFollower>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag(_playerTag)) return;
            
            _cameraFollower.target = _newTarget;
            _cameraFollower.lerpMultiplier = _newLerpMultiplier;
            _camera.orthographicSize = _newCameraSize; // TODO: Implement smoothing of camera's size change
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag(_playerTag) || !_isArea) return;
            _cameraFollower.RestoreDefaults();
        }
    }
}