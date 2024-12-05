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
        
        public float ShootDelay() {return _shootDelay;}

        public void Shoot()
        {
            var bullet = Instantiate(_bulletPrefab, _shootPosition.position,
                Quaternion.Euler(_shootPosition.eulerAngles +
                                 new Vector3(0, 0, 90))); // BUG: Change this when changing rotation of ShootingPosition
            if (_rocketWeapon) bullet.GetComponent<Bullet>().fromRocket = true;
        }
    }
}
