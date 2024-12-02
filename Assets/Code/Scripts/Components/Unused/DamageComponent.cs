using UnityEngine;

namespace Code.Scripts.Components.Unused
{
    public class DamageComponent : MonoBehaviour
    {
        [SerializeField] private int _damage;

        public void ApplyDamage(GameObject gameObject)
        {
            var healthComponent =  gameObject.GetComponent<HealthComponent>();
            if (healthComponent != null&& healthComponent.enabled)
            {
                healthComponent.ModifyHealth(_damage);
            }
        }
    }
}
