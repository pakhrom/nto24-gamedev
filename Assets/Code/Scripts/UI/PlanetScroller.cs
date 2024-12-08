using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts.UI
{
    public class PlanetScroller : MonoBehaviour
    {
        [SerializeField] private SaveManager _saveManager;
        
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;

        [SerializeField] private Button _ferrumButton;
        [SerializeField] private float _ferrumFuelThreshold;
        [SerializeField] private Button _auroraButton;
        [SerializeField] private float _auroraFuelThreshold;
        
        private Animator _animator;
        private static readonly int Right = Animator.StringToHash("Right");
        private static readonly int Left = Animator.StringToHash("Left");

        private float _fuelUpgradeAmount;

        private void Start()
        {
            if (!_saveManager) _saveManager = GameObject.Find("SaveManager").GetComponent<SaveManager>();
            _fuelUpgradeAmount = _saveManager.GetSaveData().upgrade5Amount;
            _animator = GetComponent<Animator>();

            if (_fuelUpgradeAmount < _ferrumFuelThreshold)
            {
                _ferrumButton.interactable = false;
                _ferrumButton.GetComponentInChildren<TMP_Text>().text = "Недостаточно топлива";
            }
            if (_fuelUpgradeAmount < _auroraFuelThreshold)
            {
                _auroraButton.interactable = false;
                _auroraButton.GetComponentInChildren<TMP_Text>().text = "Недостаточно топлива";
            }
        }

        public void ScrollRight()
        {
            _leftButton.interactable = true;
            _animator.SetTrigger(Right);
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("FerrumActive"))
            {
                _rightButton.interactable = false;
            }
        }
        
        public void ScrollLeft()
        {
            _rightButton.interactable = true;
            _animator.SetTrigger(Left);
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("FerrumActive"))
            {
                _leftButton.interactable = false;
            }
        }
    }
}
