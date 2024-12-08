using System;
using TMPro;
using UnityEngine;

namespace Code.Scripts.UI
{
    public class LaunchButton : MonoBehaviour
    {
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private Inventory _rocketInventory;
        [SerializeField] private Animator _runEndScreenAnimator;
        [SerializeField] private TMP_Text _collectedOreAmount;
        [SerializeField] private TMP_Text _rocketInventoryCost;

        [Header("Objects to disable on launch")] 
        [SerializeField] private Controller2DInput _controllerInput;
        [SerializeField] private Rigidbody2D _playerRigidbody;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private Rocket _rocket;

        private SaveManager _saveManager;
        
        private static readonly int RunEnd = Animator.StringToHash("EndRun");

        private void Start()
        {
            if (!_saveManager) _saveManager = GameObject.Find("SaveManager").GetComponent<SaveManager>();
        }

        public void Launch()
        {
            _rocket.enabled = false;
            _controllerInput.enabled = false;
            _playerRigidbody.simulated = false;
            _enemySpawner.enabled = false;

            var oreAmount = 0;
            var oreCost = 0;
            var inventory = _rocketInventory.inventory;
            var oreList = _rocketInventory.oreList;
            for (int i = 0; i < inventory.Count; ++i)
            {
                oreAmount += inventory[oreList.ores[i].name];
                oreCost += inventory[oreList.ores[i].name] * oreList.ores[i].value;
                Debug.Log($"Counted {inventory[oreList.ores[i].name]} {oreList.ores[i].name} ore ingots for {inventory[oreList.ores[i].name] * oreList.ores[i].value} money.");
            }

            _collectedOreAmount.text = $"Собрано {oreAmount} минералов";
            _rocketInventoryCost.text = $"общей стоимостью в {oreCost}\nмедовиков.";

            _saveManager.GetSaveData().money += oreCost;
            _saveManager.SaveGame();
            
            _runEndScreenAnimator.SetTrigger(RunEnd);
        }

        public void Continue()
        {
            _sceneLoader.StartLoadingScene(1);
        }
    }
}
