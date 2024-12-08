using UnityEngine;

namespace Code.Scripts
{
    public class Parallax : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _xMultiplier;

        private void Update()
        {
            var targetPosition = new Vector3(_target.position.x * _xMultiplier, transform.position.y, transform.position.z);
            transform.position = targetPosition;
        }
    }
}
