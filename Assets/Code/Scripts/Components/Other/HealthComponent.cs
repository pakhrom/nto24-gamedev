using UnityEngine;
using UnityEngine.Events;

namespace Code.Scripts.Components
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onDie;
        [SerializeField] private bool _notifyUI;

        public delegate void HealthEvent(int vl);

        public static event HealthEvent OnHealthChanged;
        private int _initialHealth;

        public void ModifyHealth(int damage)
        {
            _health += damage;
            if (_health <= 0)
                _onDie?.Invoke();

            if (damage < 0)
                _onDamage?.Invoke();
            if (_notifyUI)
                OnHealthChanged?.Invoke(_health);
        }

        private void ResetHealth()
        {
            _health = 3;
        }

        private void IncreaseHealth()
        {
            if (_notifyUI)
            {
                _health++;
                OnHealthChanged?.Invoke(_health);
            }
        }
    }
}