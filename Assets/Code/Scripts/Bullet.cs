using System;
using UnityEngine;

namespace Code.Scripts
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private bool _fromEnemy;
        [SerializeField] private float _speed;
        [SerializeField] private GameObject _bulletBoundingBoxPrefab;

        private Rigidbody2D _rigidbody;
        private GameObject _bulletBoundingBox;

        [NonSerialized] public float damage;
        [NonSerialized] public bool fromRocket;

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }
        
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _bulletBoundingBox = Instantiate(_bulletBoundingBoxPrefab, transform.position, Quaternion.Euler(0f, 0f, 0f));
        }

        private void FixedUpdate()
        {
            Vector3 up = transform.up;
            if (fromRocket) up = -transform.right;
            var movePosition3 = transform.position - up * (_speed * Time.fixedDeltaTime);
            var movePosition = new Vector2(movePosition3.x, movePosition3.y);
            _rigidbody.MovePosition(movePosition);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Health health))
            {
                if (!_fromEnemy && other.gameObject.TryGetComponent(out Enemy enemy))
                {
                    health.DealDamage(damage);
                    Destroy(gameObject);
                }

                if (_fromEnemy && other.gameObject.TryGetComponent(out Controller2D controller))
                {
                    health.DealDamage(damage);
                    Destroy(gameObject);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.GetInstanceID() == _bulletBoundingBox.GetInstanceID()) // remove bullet if out of range
            {
                Destroy(_bulletBoundingBox);
                Destroy(gameObject);
            }
        }
    }
}