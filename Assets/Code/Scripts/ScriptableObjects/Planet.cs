using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Planet", menuName = "Planet")]
    public class Planet : ScriptableObject
    {
        public new string name;
        public int id;
        public float size;
        public Sprite sprite;
        public List<Ore> ores;
        public List<EnemyType> enemies;

        [Header("Satellite properties")]
        public float satelliteMinRadius;
        public float satelliteMaxRadius;
        public float satelliteMinGravity;
        public float satelliteMaxGravity;
        public int satelliteMinOreCount;
        public int satelliteMaxOreCount;
        public float satelliteMinEnemyWaveInterval;
        public float satelliteMaxEnemyWaveInterval;
    }
}
