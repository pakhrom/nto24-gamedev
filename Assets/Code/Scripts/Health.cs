using System;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Scripts
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _defaultHealth;
        [SerializeField] private bool _destroyOnDeath;
        [SerializeField] private bool _callEventOnDeath;
        [SerializeField] private UnityEvent _eventOnDeath;
        
        private float _health;

        private void Start()
        {
            _health = _defaultHealth;
        }
        
        public void SetHealth(float health) { _health = health; }

        public void DealDamage(float damage)
        {
            _health -= damage;
            if (_health <= 0f) Die();
        }

        private void Die()
        {
            if (_callEventOnDeath && _eventOnDeath != null)
            {
                _eventOnDeath.Invoke();
            }

            if (_destroyOnDeath)
            {
                Destroy(gameObject);
            }
        }
    }
}