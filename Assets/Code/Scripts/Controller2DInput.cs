using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Scripts
{
    public class Controller2DInput : MonoBehaviour
    {
        private Controller2D _controller;
        private bool _isJumpButtonPressed;
        private bool _isShootingButtonPressed;

        public bool IsJumpButtonPressed => _isJumpButtonPressed;

        public bool IsShootingButtonPressed => _isShootingButtonPressed;

        private void Awake()
        {
            _controller = GetComponent<Controller2D>();
        }

        private void Update()
        {
            if (_isShootingButtonPressed) _controller.Shoot();
        }

        public void Movement(InputAction.CallbackContext context)
        {
            var movementDirection = context.ReadValue<Vector2>();
            _controller.SetMovementDirection(movementDirection);
        }
        
        public void Jump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _controller.Jump();
                _isJumpButtonPressed = true;
            }
            if (context.canceled)
            {
                _controller.JumpCancel();
                _isJumpButtonPressed = false;
            }
        }
        public void Shoot(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _isShootingButtonPressed = true;
            }
            if (context.canceled)
            {
                _isShootingButtonPressed = false;
            }
        }
        public void Dash(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                StartCoroutine(_controller.Dash());
            }
        }
    }
}

