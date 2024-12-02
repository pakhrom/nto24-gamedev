using System;
using UnityEngine;

namespace Code.Scripts
{
    public class GroundCheck : MonoBehaviour
    {
        // [SerializeField] private LayerMask _layer;
        // [SerializeField] private float _threshold;

        public bool isGrounded;
        
        // private Collider2D _collider;

        // private void Awake()
        // {
            // _collider = GetComponent<Collider2D>(); 
        // }
        public bool IsGrounded()
        {
            // RaycastHit2D boxCast = Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size * 1,0f,Vector2.down,  _threshold, _layer);
            // return boxCast.collider;
            return isGrounded;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            isGrounded = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            isGrounded = false;
        }
    }
}
