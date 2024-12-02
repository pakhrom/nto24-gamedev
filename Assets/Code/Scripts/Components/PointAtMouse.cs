using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Scripts.Components
{
    public class PointAtMouse : MonoBehaviour
    {
        // [SerializeField] private float _rotationSpeed; TODO: Implement this.
        [SerializeField] private string _pointActionName;
        
        private InputAction _pointAction;
        private Camera _mainCamera;

        private void Awake()
        {
            _pointAction = InputSystem.actions.FindAction(_pointActionName);
        }

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (!_mainCamera) return;
            
            Vector2 mouseScreenPosition = _pointAction.ReadValue<Vector2>();
            Vector3 mouseWorldPosition =
                _mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, 0))
                + new Vector3(0, 0, 10);

            transform.right = mouseWorldPosition - transform.position;
        }
    }
}
