using System;
using Code.Scripts.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Scripts
{
    public class Controller2DInput : MonoBehaviour
    {
        [SerializeField] private Animator _exitPromptAnimator;
        [SerializeField] private SceneLoader _sceneLoader;
        
        private Controller2D _controller;
        private bool _isJumpButtonPressed;
        private bool _isShootingButtonPressed;
        private bool _isMiningButtonPressed;

        [NonSerialized] public bool canInteract = true;
        private static readonly int ShowExitPrompt = Animator.StringToHash("ShowExitPrompt");
        private static readonly int HideExitPrompt = Animator.StringToHash("HideExitPrompt");

        public bool IsJumpButtonPressed => _isJumpButtonPressed;

        public bool IsShootingButtonPressed => _isShootingButtonPressed;

        public bool IsMiningButtonPressed => _isMiningButtonPressed;

        private void Awake()
        {
            _controller = GetComponent<Controller2D>();
        }

        private void Update()
        {
            if (_isShootingButtonPressed) _controller.Shoot();
            if (_isMiningButtonPressed) _controller.Mine();
        }

        public void Movement(InputAction.CallbackContext context)
        {
            if (!canInteract) return;
            var movementDirection = context.ReadValue<Vector2>();
            _controller.SetMovementDirection(movementDirection);
        }
        
        public void Jump(InputAction.CallbackContext context)
        {
            if (!canInteract) return;
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

        public void Mine(InputAction.CallbackContext context)
        {
            if (!canInteract) return;
            if (context.performed)
            {
                _isMiningButtonPressed = true;
            }

            if (context.canceled)
            {
                _isMiningButtonPressed = false;
            }
        }

        public void Action(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _controller.Action();
            }
        }

        public void Exit(InputAction.CallbackContext context)
        {
            if (context.started) _exitPromptAnimator.SetTrigger(ShowExitPrompt);
            if (context.performed)
            {
                _exitPromptAnimator.SetTrigger(HideExitPrompt);
                _sceneLoader.StartLoadingScene(0);
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

