using UnityEngine;

namespace Code.Scripts
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private string _bulletBoundingBoxTag;
        [SerializeField] private string _oreObjectsTag;
    
        private void Update()
        {
            transform.position += transform.right * (_speed * Time.deltaTime);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(_bulletBoundingBoxTag))
            {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(_oreObjectsTag))
            {
                Destroy(gameObject);
            }
        }
    }
}