using UnityEngine;

namespace Code.Scripts.Components
{
    public class GroundCheck : MonoBehaviour
    {
        [SerializeField] private LayerMask _layer;
        [SerializeField] private float _threshold;

        private Collider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>(); 
        }
        public bool IsGrounded()
        {
            RaycastHit2D boxCast = Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size * 1,0f,Vector2.down,  _threshold, _layer);
            return boxCast.collider;
        }
    }
}
