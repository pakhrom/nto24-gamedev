using System;
using System.Linq;
using UnityEngine;

namespace Code.Scripts
{
    public class Rocket : MonoBehaviour
    {
        [SerializeField] private SaveManager _saveManager;
        [SerializeField] private Controller2D _player;
        [SerializeField] private Weapon _weapon;
        [SerializeField] private PointAtMouse _weaponPointer;
        [SerializeField] private Inventory _playerInventory;

        [Header("Animation properties")] 
        [SerializeField] private GameObject _buttonTooltip;
        [SerializeField] private Animator _launchButton;

        [NonSerialized] public bool isPlayerNearby;
        [NonSerialized] public bool isPlayerInRocket;
        [NonSerialized] public float shootTimer;
        [NonSerialized] public float shootDelay;

        private Inventory _inventory;
        private static readonly int ShowLaunchButton = Animator.StringToHash("ShowLaunchButton");
        private static readonly int HideLaunchButton = Animator.StringToHash("HideLaunchButton");

        private void Start()
        {
            _inventory = GetComponent<Inventory>();
            
            _buttonTooltip.SetActive(false);

            shootDelay = _saveManager.GetSaveData().shootDelay;
            shootTimer = shootDelay;
            
            if (!_saveManager) _saveManager = GameObject.Find("SaveManager").GetComponent<SaveManager>();
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
            
            _launchButton.SetTrigger(ShowLaunchButton);
            
            _weapon.enabled = true;
            _weaponPointer.enabled = true;
            
            _player.health.SetDamageMultiplier(_saveManager.GetSaveData().inRocketDamageMultiplier);

            isPlayerInRocket = true;
        }

        public void Exit()
        {
            _launchButton.SetTrigger(HideLaunchButton);
            
            _weapon.enabled = false;
            _weaponPointer.enabled = false;
            
            _player.health.SetDamageMultiplier(1f);

            isPlayerInRocket = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetInstanceID() == _player.gameObject.GetInstanceID())
            {
                isPlayerNearby = true;
                _buttonTooltip.SetActive(true);
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!_player.gameObject) return;
            if (other.gameObject.GetInstanceID() == _player.gameObject.GetInstanceID())
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
