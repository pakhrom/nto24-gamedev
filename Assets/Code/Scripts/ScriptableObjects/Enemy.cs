using UnityEngine;

namespace Code.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
    public class Enemy : ScriptableObject
    {
        public new string name;
        public float moveSpeed;
        public Sprite sprite;
        
        [Header("Attacking properties")]
        public float damage;
        public float shootInterval;
        public float bulletSpeed;
    }
}