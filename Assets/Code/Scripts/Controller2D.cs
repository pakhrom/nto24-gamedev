using System.Collections;
using Code.Scripts.Components;
using UnityEngine;

// ReSharper disable InconsistentNaming

namespace Code.Scripts
{
    public class Controller2D : MonoBehaviour
    {
        [SerializeField] private Controller2DInput _input;
        [Header("Controller Settings")]
        [SerializeField] private bool _isSideOn; 
        [SerializeField] private bool _canDash;
        [SerializeField] private bool _canFloat;
        [SerializeField] private bool _canShoot;

        [Header("Movement Settings")]
        [SerializeField] private GroundCheck _groundCheck;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _jumpCancelMultiplier;
        [SerializeField] private float _coyoteTime;
        [SerializeField] private float _jumpBufferTime;
        [SerializeField] private float _floatSpeed;
        [SerializeField] private float _dashForce;
        [SerializeField] private float _dashTime;
        [SerializeField] private float _dashCooldown;

        [Header("Shooting Settings")] 
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _shootPosition;
        [SerializeField] private float _shootDelay;

        private float _shootTimer = 0f;
        
        private bool _isDashing;
        private bool _dashAvailable;

        private Rigidbody2D _rigidbody;
        private float _defaultGravityScale;
        
        private Vector2 _movementDirection;

        private float _coyoteTimeCounter;
        private float _jumpBufferCounter;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            if (!_isSideOn)
            {
                _rigidbody.gravityScale = 0f;
            }
            _defaultGravityScale = _rigidbody.gravityScale;

            _isDashing = false;
            _dashAvailable = true;
        }

        private void Update()
        {
            if (_groundCheck.IsGrounded())
            {
                _coyoteTimeCounter = _coyoteTime;
            }
            else
            {
                _coyoteTimeCounter -= Time.deltaTime;
            }

            _jumpBufferCounter -= Time.deltaTime;
            _shootTimer += Time.deltaTime;
        }

        private void FixedUpdate()
        {
            ProcessMovement();
            FloatMidAir();

            if (_input.IsJumpButtonPressed && _coyoteTimeCounter > 0f && _jumpBufferCounter > 0f)
            {
                Jump();
            }
        }

        private void ProcessMovement()
        {
            if (_isDashing) return;
            
            var velocity = _movementDirection;
            _rigidbody.linearVelocity = _isSideOn
                ? new Vector2(velocity.x * _movementSpeed, _rigidbody.linearVelocity.y)
                : velocity.normalized * _movementSpeed;
        }

        public void SetMovementDirection(Vector2 direction)
        {
            _movementDirection = direction;
        }

        public void Jump()
        {
            if (!_isSideOn) return;
    
            _jumpBufferCounter = _jumpBufferTime;
            if (!(_coyoteTimeCounter > 0f)) return;
            
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, 0);
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _jumpBufferCounter = 0f;
        }

        public void JumpCancel()
        {
            _coyoteTimeCounter = 0f;
            if (!_isSideOn) return;
            
            if (_groundCheck.IsGrounded() || !(_rigidbody.linearVelocity.y > 0f)) return;
            Vector2 velocity = _rigidbody.linearVelocity;
            _rigidbody.linearVelocity = new Vector2(velocity.x, velocity.y * _jumpCancelMultiplier);
        }

        private void FloatMidAir()
        {
            if (_canFloat && _input.IsJumpButtonPressed && !_groundCheck.IsGrounded() && _rigidbody.linearVelocity.y < 0)
            {
                _rigidbody.linearVelocity = _movementDirection * _floatSpeed;
            }
        }

        public void Shoot()
        {
            if (!_canShoot) return;
            
            if (!(_shootTimer >= _shootDelay) || !_input.IsShootingButtonPressed) return;
            Instantiate(_bulletPrefab, _shootPosition.position, _shootPosition.rotation);
            _shootTimer = 0;
        }
        
        // public IEnumerator Shoot()
        // {
        //     if (!_canShoot) yield break;
        //     while (_input.IsShootingButtonPressed)
        //     {
        //         Instantiate(_bulletPrefab, _shootPosition.position, _shootPosition.rotation);
        //         yield return new WaitForSeconds(_shootDelay);
        //     }
        // }

        public IEnumerator Dash()
        {
            if (!_canDash || !_dashAvailable || _movementDirection == Vector2.zero) yield break;

            _isDashing = true;

            var dashingDirection = _isSideOn ? new Vector2(_movementDirection.x, 0f) : _movementDirection;

            _rigidbody.gravityScale = 0f;
            _rigidbody.linearVelocity = Vector2.zero;

            _rigidbody.AddForce(dashingDirection * _dashForce, ForceMode2D.Impulse);

            _dashAvailable = false;
            yield return new WaitForSeconds(_dashTime);

            _rigidbody.gravityScale = _defaultGravityScale;
            _isDashing = false;

            yield return new WaitForSeconds(_dashCooldown);
            _dashAvailable = true;
        }
    }
}
