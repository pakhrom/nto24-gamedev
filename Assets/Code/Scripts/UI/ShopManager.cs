using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts.UI
{
    public class ShopManager : MonoBehaviour
    {
        [SerializeField] private bool _isDebug;
        [SerializeField] private SaveManager _saveManager;
        [SerializeField] private TMP_Text _moneyCounter;
        [SerializeField] private Button _upgrade1Button;
        [SerializeField] private int upgrade1Cost;
        [SerializeField] private int maxUpgrade1Amount;
        [SerializeField] private Button _upgrade2Button;
        [SerializeField] private int upgrade2Cost;
        [SerializeField] private int maxUpgrade2Amount;
        [SerializeField] private Button _upgrade3Button;
        [SerializeField] private int upgrade3Cost;
        [SerializeField] private int maxUpgrade3Amount;
        [SerializeField] private Button _upgrade4Button;
        [SerializeField] private int upgrade4Cost;
        [SerializeField] private int maxUpgrade4Amount;
        [SerializeField] private Button _upgrade5Button;
        [SerializeField] private int upgrade5Cost;
        [SerializeField] private int maxUpgrade5Amount;

        private int _money;
        
        private void Start()
        {
            if (!_saveManager) _saveManager = GameObject.Find("SaveManager").GetComponent<SaveManager>();

            if (_isDebug)
            {
                _saveManager.GetSaveData().money = 99999;
                _saveManager.SaveGame();
            }
            _money = _saveManager.GetSaveData().money;

            _upgrade1Button.GetComponentInChildren<TMP_Text>().text =
                (upgrade1Cost * (_saveManager.GetSaveData().upgrade1Amount + 1)).ToString();
            _upgrade2Button.GetComponentInChildren<TMP_Text>().text =
                (upgrade1Cost * (_saveManager.GetSaveData().upgrade2Amount + 1)).ToString();
            _upgrade3Button.GetComponentInChildren<TMP_Text>().text =
                (upgrade1Cost * (_saveManager.GetSaveData().upgrade3Amount + 1)).ToString();
            _upgrade4Button.GetComponentInChildren<TMP_Text>().text =
                (upgrade1Cost * (_saveManager.GetSaveData().upgrade4Amount + 1)).ToString();
            _upgrade5Button.GetComponentInChildren<TMP_Text>().text =
                (upgrade1Cost * (_saveManager.GetSaveData().upgrade5Amount + 1)).ToString();
            
            if (_saveManager.GetSaveData().upgrade1Amount >= maxUpgrade1Amount)
                _upgrade1Button.gameObject.SetActive(false);
            if (_saveManager.GetSaveData().upgrade2Amount >= maxUpgrade2Amount)
                _upgrade2Button.gameObject.SetActive(false);
            if (_saveManager.GetSaveData().upgrade3Amount >= maxUpgrade3Amount)
                _upgrade3Button.gameObject.SetActive(false);
            if (_saveManager.GetSaveData().upgrade4Amount >= maxUpgrade4Amount)
                _upgrade4Button.gameObject.SetActive(false);
            if (_saveManager.GetSaveData().upgrade5Amount >= maxUpgrade5Amount)
                _upgrade5Button.gameObject.SetActive(false);

            _moneyCounter.text = _money.ToString();
        }

        public void Upgrade1()
        {
            if (_saveManager.GetSaveData().money >= upgrade1Cost * (_saveManager.GetSaveData().upgrade1Amount + 1))
            {
                _saveManager.GetSaveData().health += 25;
                
                _money -= upgrade1Cost * (_saveManager.GetSaveData().upgrade1Amount + 1);
                _saveManager.GetSaveData().money = _money;
                _saveManager.GetSaveData().upgrade1Amount += 1;
                _saveManager.SaveGame();
                
                _moneyCounter.text = _money.ToString();
                _upgrade1Button.GetComponentInChildren<TMP_Text>().text =
                    (upgrade1Cost * (_saveManager.GetSaveData().upgrade1Amount + 1)).ToString();
                
                if (_saveManager.GetSaveData().upgrade1Amount >= maxUpgrade1Amount)
                    _upgrade1Button.gameObject.SetActive(false);
            }
        }
        
        public void Upgrade2()
        {
            if (_saveManager.GetSaveData().money >= upgrade2Cost * (_saveManager.GetSaveData().upgrade2Amount + 1))
            {
                _saveManager.GetSaveData().miningToolDamage += 1f;
                
                _money -= upgrade2Cost * (_saveManager.GetSaveData().upgrade2Amount + 1);
                _saveManager.GetSaveData().money = _money;
                _saveManager.GetSaveData().upgrade2Amount += 1;
                _saveManager.SaveGame();
                
                _moneyCounter.text = _money.ToString();
                _upgrade2Button.GetComponentInChildren<TMP_Text>().text =
                    (upgrade2Cost * (_saveManager.GetSaveData().upgrade2Amount + 1)).ToString();

                if (_saveManager.GetSaveData().upgrade2Amount >= maxUpgrade2Amount)
                    _upgrade2Button.gameObject.SetActive(false);
            }
        }
        
        public void Upgrade3()
        {
            if (_saveManager.GetSaveData().money >= upgrade3Cost * (_saveManager.GetSaveData().upgrade3Amount + 1))
            {
                _saveManager.GetSaveData().inventoryMaxCapacity += 5;
                
                _money -= upgrade3Cost * (_saveManager.GetSaveData().upgrade3Amount + 1);
                _saveManager.GetSaveData().money = _money;
                _saveManager.GetSaveData().upgrade3Amount += 1;
                _saveManager.SaveGame();
                
                _moneyCounter.text = _money.ToString();
                _upgrade3Button.GetComponentInChildren<TMP_Text>().text =
                    (upgrade3Cost * (_saveManager.GetSaveData().upgrade3Amount + 1)).ToString();

                if (_saveManager.GetSaveData().upgrade3Amount >= maxUpgrade3Amount)
                    _upgrade3Button.gameObject.SetActive(false);
            }
        }
        
        public void Upgrade4()
        {
            if (_saveManager.GetSaveData().money >= upgrade4Cost * (_saveManager.GetSaveData().upgrade4Amount + 1))
            {
                _saveManager.GetSaveData().shootDelay -= 0.5f;
                
                _money -= upgrade4Cost * (_saveManager.GetSaveData().upgrade4Amount + 1);
                _saveManager.GetSaveData().money = _money;
                _saveManager.GetSaveData().upgrade4Amount += 1;
                _saveManager.SaveGame();
                
                _moneyCounter.text = _money.ToString();
                _upgrade4Button.GetComponentInChildren<TMP_Text>().text =
                    (upgrade4Cost * (_saveManager.GetSaveData().upgrade4Amount + 1)).ToString();

                if (_saveManager.GetSaveData().upgrade4Amount >= maxUpgrade4Amount)
                    _upgrade4Button.gameObject.SetActive(false);
            }
        }
        
        public void Upgrade5()
        {
            if (_saveManager.GetSaveData().money >= upgrade5Cost * (_saveManager.GetSaveData().upgrade5Amount + 1))
            {
                _saveManager.GetSaveData().fuelCapacity += 200f;
                
                _money -= upgrade5Cost * (_saveManager.GetSaveData().upgrade5Amount + 1);
                _saveManager.GetSaveData().money = _money;
                _saveManager.GetSaveData().upgrade5Amount += 1;
                _saveManager.SaveGame();
                
                _moneyCounter.text = _money.ToString();
                _upgrade5Button.GetComponentInChildren<TMP_Text>().text =
                    (upgrade5Cost * (_saveManager.GetSaveData().upgrade5Amount + 1)).ToString();

                if (_saveManager.GetSaveData().upgrade5Amount >= maxUpgrade5Amount)
                    _upgrade5Button.gameObject.SetActive(false);
            }
        }
    }
}
