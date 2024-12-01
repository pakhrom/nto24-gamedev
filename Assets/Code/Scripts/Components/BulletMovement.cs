using UnityEngine;

namespace Code.Scripts.Components
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private string _bulletBoundingBoxTag;
    
        private void Update()
        {
            transform.position += transform.right * (_speed * Time.deltaTime);
            // TODO: Implement Destroy method when outside the camera's viewport
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Debug.Log(other);
            if (other.gameObject.CompareTag(_bulletBoundingBoxTag))
            {
                Destroy(gameObject);
            }
        }
    }
}