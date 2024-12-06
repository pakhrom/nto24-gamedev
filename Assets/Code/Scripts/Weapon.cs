using UnityEngine;

namespace Code.Scripts
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private bool _rocketWeapon;
        public SpriteRenderer spriteRenderer;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _shootPosition;
        [SerializeField] private float _shootDelay;
        [SerializeField] private float _damage;
        [SerializeField] private Vector3 _rotationOffset;
        
        public float ShootDelay() {return _shootDelay;}

        public Bullet Shoot()
        {
            var bullet = Instantiate(_bulletPrefab, _shootPosition.position,
                Quaternion.Euler(_shootPosition.eulerAngles + _rotationOffset));
            var bulletComponent = bullet.GetComponent<Bullet>();
            bulletComponent.damage = _damage;
            if (_rocketWeapon) bulletComponent.fromRocket = true;
            return bulletComponent;
        }
    }
}
