using UnityEngine;

namespace Code.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
    public class EnemyType : ScriptableObject
    {
        public new string name;
        public float moveSpeed;
        
        [Header("Attacking properties")]
        public float damage;
        public float minShootDistance;
        public float shootInterval;
        public float bulletSpeed;
    }
}