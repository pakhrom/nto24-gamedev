using System;
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
        [SerializeField] private Transform _oreDropPosition;
        [SerializeField] private GameObject _oreIngotPrefab;
        [Tooltip("How many hits until drop")]
        [SerializeField] private float _oreDropThreshold;
        [SerializeField] private int _minDropCount;
        [SerializeField] private int _maxDropCount;
        [SerializeField] private int _minOrePerDrop;
        [SerializeField] private int _maxOrePerDrop;

        private float _damageCounter;
        private int _dropCount;
        
        private bool _sliderActive;

        private void Start()
        {
            _miningProgressCanvas.enabled = false;

            _dropCount = Random.Range(_minDropCount, _maxDropCount + 1);
        }

        public void DealDamage(float damage)
        {
            _damageCounter += damage;

            if (_damageCounter >= _oreDropThreshold)
            {
                int drops = (int)(_damageCounter / _oreDropThreshold);
                DropOre(drops);
                _damageCounter %= _oreDropThreshold;
            }
            
            UpdateSlider();
        }

        private void DropOre(int drops)
        {
            for (int i = 0; i < Math.Min(drops, _dropCount); ++i)
            {
                int oreAmount = Random.Range(_minOrePerDrop, _maxOrePerDrop + 1);
                for (int j = 0; j < oreAmount; ++j)
                {
                    Instantiate(_oreIngotPrefab, _oreDropPosition);
                }
                _dropCount -= 1;
            }
        }

        private void UpdateSlider()
        {
            if (_dropCount != 0)
            {
                _miningProgressSlider.value = _damageCounter / _oreDropThreshold;
            }
            else
            {
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
