using System;
using System.Collections;
using UnityEngine;

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

        [Header("Animations")]
        [SerializeField] private Transform _miningToolPivot;
        [SerializeField] private Transform _weaponPivot;
        [SerializeField] private string _beginRunTrigger;
        [SerializeField] private string _changeItemTrigger;
        [SerializeField] private string _activeItemIntProperty;
        [Tooltip("Must be equal to the ChangeItem animation duration")]
        [SerializeField] private float _changeItemDuration;

        [Header("Shooting Settings")] 
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _shootPosition;
        [SerializeField] private float _shootDelay;

        [Header("Mining tools Settings")] 
        [SerializeField] private MiningTool _miningTool;
        [SerializeField] private float _mineDelay;

        [Header("Weapon Settings")] 
        [SerializeField] private Weapon _weapon;

        private enum Items
        {
            MiningTool,
            Weapon
        }

        private Items _activeItem;

        private float _changeItemTimer;
        private float _shootTimer;
        private float _mineTimer;
        
        private bool _isDashing;
        private bool _dashAvailable;

        private Rigidbody2D _rigidbody;
        private float _defaultGravityScale;

        private Animator _animator;
        
        private Vector2 _movementDirection;

        private float _coyoteTimeCounter;
        private float _jumpBufferCounter;

        private Inventory _inventory;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _inventory = GetComponent<Inventory>();

            if (!_isSideOn)
            {
                _rigidbody.gravityScale = 0f;
            }
            _defaultGravityScale = _rigidbody.gravityScale;

            _isDashing = false;
            _dashAvailable = true;

            _shootTimer = _shootDelay;
            _mineTimer = _mineDelay;
            _changeItemTimer = _changeItemDuration;

            _animator.SetInteger(_activeItemIntProperty, 0);
            _animator.SetTrigger(_beginRunTrigger);
        }

        private void Start()
        {
            _rigidbody.simulated = true;
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
            _changeItemTimer += Time.deltaTime;
            _shootTimer += Time.deltaTime;
            _mineTimer += Time.deltaTime;

            _jumpBufferCounter = Mathf.Max(_jumpBufferCounter, -10f);
            _changeItemTimer = Mathf.Min(_changeItemTimer, _changeItemDuration + 10f);
            _shootTimer = Mathf.Min(_shootTimer, _shootDelay + 10f);
            _mineTimer = Mathf.Min(_mineTimer, _mineDelay + 10f);
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

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.TryGetComponent(out OreIngot oreIngot)) return;
            _inventory.AddOre(oreIngot, 1);
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
            _miningTool.transform.localScale = _movementDirection.x switch
            {
                > 0f => new Vector3(1f, transform.localScale.y, transform.localScale.z),
                < 0f => new Vector3(-1f, transform.localScale.y, transform.localScale.z),
                _ => _miningTool.transform.localScale
            };
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

        private void ChangeActiveItem(Items item)
        {
            if (_activeItem == item || _changeItemTimer < _changeItemDuration) return;
            
            switch (item)
            {
                case Items.MiningTool:
                    _activeItem = Items.MiningTool;
                    
                    _animator.SetInteger(_activeItemIntProperty, 0);
                    _animator.SetTrigger(_changeItemTrigger);
                    break;
                case Items.Weapon:
                    _activeItem = Items.Weapon;
                    
                    _animator.SetInteger(_activeItemIntProperty, 1);
                    _animator.SetTrigger(_changeItemTrigger);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _changeItemTimer = 0f;
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
            if (_activeItem != Items.Weapon)
            {
                ChangeActiveItem(Items.Weapon);
                return;
            }
            
            if (!_canShoot) return;
            
            if (!(_changeItemTimer >= _changeItemDuration) || !(_shootTimer >= _shootDelay) || !_input.IsShootingButtonPressed) return;
            Instantiate(_bulletPrefab, _shootPosition.position,
                Quaternion.Euler(_shootPosition.eulerAngles +
                                 new Vector3(0, 0, 90))); // BUG: Change this when changing rotation of ShootingPosition
            _shootTimer = 0f;
        }

        public void Mine()
        {
            if (_activeItem != Items.MiningTool)
            {
                ChangeActiveItem(Items.MiningTool);
                return;
            }
            
            if (!(_changeItemTimer >= _changeItemDuration) || !(_mineTimer >= _mineDelay) || !_input.IsMiningButtonPressed) return;
            _miningTool.Mine();
            _mineTimer = 0f;
        }

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
