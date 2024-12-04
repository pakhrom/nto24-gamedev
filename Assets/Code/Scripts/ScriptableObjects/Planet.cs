using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Planet", menuName = "Planet")]
    public class Planet : ScriptableObject
    {
        public new string name;
        public float size;
        public Sprite sprite;
        public List<Ore> ores;

        [Header("Satellite properties")]
        public float satelliteMinRadius;
        public float satelliteMaxRadius;
        public int satelliteMinOreCount;
        public int satelliteMaxOreCount;
    }
}
