using System;
using Code.Scripts.Components;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Scripts
{
    public class MiningTool : MonoBehaviour
    {
        [SerializeField] private Controller2DInput _input;
        public SpriteRenderer spriteRenderer;
        [SerializeField] private string _oreTag;
        
        [Header("Tool types")]
        [SerializeField] private GameObject _toolShortRange;
        [SerializeField] private GameObject _toolLongRange;

        [Header("Short-range tool properties")] 
        public float damage;

        [SerializeField] private float _mineDelay;
        // TODO: Implement this
        
        [Header("Animations")]
        [SerializeField] private string _toolSwingTrigger;

        [Header("Debug flags")]
        [SerializeField] private bool _isLongRangeActive = false;
        
        [NonSerialized] public Animator animator;
        private BoxCollider2D _toolShortRangeArea;

        private OreComponent _ore;

        [NonSerialized] public bool canHit;

        public float MineDelay() {return _mineDelay;}
        
        private void Start()
        {
            animator = GetComponent<Animator>();
            _toolShortRangeArea = GetComponent<BoxCollider2D>();
            canHit = true;
            
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

            if (!_ore) return;
            _ore.SwitchSlider();
            _ore = null;
        }

        public void Mine()
        {
            if (!_isLongRangeActive && canHit)
            {
                canHit = false;
                animator.SetTrigger(_toolSwingTrigger);
            }
        }

        public void ToolSwingEnd()
        {
            if (_ore)
            {
                _ore.DealDamage(damage);
            }

            canHit = true;
        }
    }
}
