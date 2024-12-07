using System;
using Code.Scripts.ScriptableObjects;
using UnityEngine;

namespace Code.Scripts
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private Weapon _weapon;
        [SerializeField] private Transform _weaponPivot;

        private Rigidbody2D _rigidbody;

        private float _moveSpeed;
        private float _shootTimer;
        
        public Transform moveTarget;
        public EnemySpawner enemySpawner;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();

            _moveSpeed = _enemyType.moveSpeed;
        }

        private void Update()
        {
            _weaponPivot.transform.up = moveTarget.position - _weaponPivot.transform.position;
            
            _shootTimer += Time.deltaTime;

            if (Vector2.Distance(transform.position, moveTarget.position) >= _enemyType.minShootDistance)
            {
                _rigidbody.MovePosition(Vector2.MoveTowards(transform.position, moveTarget.position, _moveSpeed * Time.deltaTime));
            }
            else
            {
                if (_shootTimer >= _enemyType.shootInterval)
                {
                    Bullet bullet = _weapon.Shoot();
                    bullet.SetSpeed(_enemyType.bulletSpeed);
                    bullet.damage = _enemyType.damage;
                    _shootTimer = 0f;
                }
            }
        }

        public void Die()
        {
            enemySpawner.EnemyDied();
            Destroy(gameObject);
        }
    }
}