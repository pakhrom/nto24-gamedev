using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Code.Scripts
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _defaultHealth;
        [SerializeField] private bool _destroyOnDeath;
        [SerializeField] private bool _callEventOnDeath;
        [SerializeField] private UnityEvent _eventOnDeath;
        [SerializeField] private Slider _healthSlider;
        
        private float _health;
        private float _damageMultiplier = 1f;

        private void Start()
        {
            _health = _defaultHealth;
        }
        
        public void SetHealth(float health) { _health = health; }

        public void SetDamageMultiplier(float multiplier) { _damageMultiplier = multiplier; }

        public void DealDamage(float damage)
        {
            _health -= damage * _damageMultiplier;
            if (_healthSlider) _healthSlider.value = _health / _defaultHealth;
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