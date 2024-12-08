using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts.UI
{
    public class PlanetScroller : MonoBehaviour
    {
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;
        
        private Animator _animator;
        private static readonly int Right = Animator.StringToHash("Right");
        private static readonly int Left = Animator.StringToHash("Left");

        private void Start()
        {
            _animator = GetComponent<Animator>();
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
