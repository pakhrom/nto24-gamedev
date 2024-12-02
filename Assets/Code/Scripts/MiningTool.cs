using Code.Scripts.Components;
using UnityEngine;

namespace Code.Scripts
{
    public class MiningTool : MonoBehaviour
    {
        [SerializeField] private Controller2DInput _input;
        [SerializeField] private string _oreTag;
        
        [Header("Tool types")]
        [SerializeField] private GameObject _toolShortRange;
        [SerializeField] private GameObject _toolLongRange;

        [Header("Short-range tool properties")] 
        [SerializeField] private float _damage;
        // TODO: Implement this
        
        [Header("Animations")]
        [SerializeField] private string _toolSwingTrigger;

        [Header("Debug flags")]
        [SerializeField] private bool _isLongRangeActive = false;

        private Animator _animator;
        private BoxCollider2D _toolShortRangeArea;

        private OreComponent _ore;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _toolShortRangeArea = GetComponent<BoxCollider2D>();
            
            if (!_isLongRangeActive) return; // When tool is long-range
            _toolShortRange.SetActive(false);
            _toolLongRange.SetActive(true);
            _toolShortRangeArea.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag(_oreTag)) return;
            
            _ore = other.gameObject.GetComponent<OreComponent>();
            _ore.SwitchSlider();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag(_oreTag)) return;
            
            _ore.SwitchSlider();
            _ore = null;
        }

        public void Mine()
        {
            if (!_isLongRangeActive)
            {
                _animator.SetTrigger(_toolSwingTrigger);
            }
        }

        public void ToolSwingEnd()
        {
            // TODO: Deal damage

            if (_ore)
            {
                _ore.DealDamage(_damage);
            }
        }
    }
}
