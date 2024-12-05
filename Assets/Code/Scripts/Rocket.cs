using System;
using System.Linq;
using UnityEngine;

namespace Code.Scripts
{
    public class Rocket : MonoBehaviour
    {
        [SerializeField] private SaveManager _saveManager;
        [SerializeField] private GameObject _player;
        [SerializeField] private Weapon _weapon;
        [SerializeField] private PointAtMouse _weaponPointer;
        [SerializeField] private Inventory _playerInventory;

        [Header("Animation properties")] 
        [SerializeField] private GameObject _buttonTooltip;

        [NonSerialized] public bool isPlayerNearby;
        [NonSerialized] public bool isPlayerInRocket;
        [NonSerialized] public float shootTimer;
        [NonSerialized] public float shootDelay;

        private Inventory _inventory;

        private void Start()
        {
            _inventory = GetComponent<Inventory>();
            
            _buttonTooltip.SetActive(false);

            shootDelay = _weapon.ShootDelay();
            shootTimer = shootDelay;
        }

        private void Update()
        {
            shootTimer += Time.deltaTime;
            shootTimer = Mathf.Min(shootTimer, shootDelay + 10f);
        }

        public void Enter()
        {
            if (_playerInventory.GetCurrentFullness() != 0)
            {
                foreach (var ore in (_playerInventory.oreList.ores).Where(ore => _playerInventory.inventory[ore.name] != 0)) // If there is ore in the player's inventory
                {
                    _inventory.AddOre(ore, _playerInventory.inventory[ore.name]);
                    _playerInventory.RemoveOre(ore, _playerInventory.inventory[ore.name]);
                }
            }
            
            _weapon.enabled = true;
            _weaponPointer.enabled = true;

            isPlayerInRocket = true;
        }

        public void Exit()
        {
            _weapon.enabled = false;
            _weaponPointer.enabled = false;

            isPlayerInRocket = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetInstanceID() == _player.GetInstanceID())
            {
                isPlayerNearby = true;
                _buttonTooltip.SetActive(true);
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.GetInstanceID() == _player.GetInstanceID())
            {
                isPlayerNearby = false;
                _buttonTooltip.SetActive(false);
            }
        }

        public void Shoot()
        {
            _weapon.Shoot();
            shootTimer = 0f;
        }
    }
}
