using System;
using Code.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Code.Scripts
{
    [RequireComponent(typeof(Collider2D))]
    
    public class OreComponent : MonoBehaviour
    {
        [SerializeField] private Canvas _miningProgressCanvas;
        [SerializeField] private Slider _miningProgressSlider;

        [Header("Ore properties")] 
        [SerializeField] private Ore _ore;
        [SerializeField] private Transform _oreDropPosition;

        private float _oreDropThreshold;
        
        private float _damageCounter;
        private int _dropCount;
        
        private bool _sliderActive;

        private void Start()
        {
            _miningProgressCanvas.enabled = false;

            _oreDropThreshold = Random.Range(_ore.minOreDropThreshold, _ore.maxOreDropThreshold);
            _dropCount = Random.Range(_ore.minDropCount, _ore.maxDropCount + 1);
        }

        public void DealDamage(float damage)
        {
            _damageCounter += damage;

            if (_damageCounter >= _oreDropThreshold)
            {
                int drops = (int)Mathf.Floor(_damageCounter / _oreDropThreshold);
                DropOre(drops);
                _damageCounter %= _oreDropThreshold;
            }
            
            UpdateSlider();
        }

        private void DropOre(int drops)
        {
            for (int i = 0; i < Math.Min(drops, _dropCount); ++i)
            {
                int oreAmount = Random.Range(_ore.minOrePerDrop, _ore.maxOrePerDrop + 1);
                for (int j = 0; j < oreAmount; ++j)
                {
                    Instantiate(_ore.ingotPrefab, _oreDropPosition);
                }
            }

            _dropCount -= Math.Min(drops, _dropCount);
        }

        private void UpdateSlider()
        {
            if (_dropCount != 0)
            {
                _miningProgressSlider.value = _damageCounter / _oreDropThreshold;
            }
            else
            {
                _miningProgressSlider.interactable = false;
                _miningProgressSlider.value = 1;
            }
        }

        public void SwitchSlider()
        {
            _miningProgressCanvas.enabled = !_sliderActive;
            _sliderActive = _miningProgressCanvas.enabled;
        }
    }
}
