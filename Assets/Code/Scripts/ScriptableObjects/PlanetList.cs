using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Planet List", menuName = "Planet List")]
    public class PlanetList : ScriptableObject
    {
        public List<Planet> planets;
    }
}