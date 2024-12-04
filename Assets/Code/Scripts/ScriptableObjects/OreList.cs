using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Ore List", menuName = "Ore List")]
    public class OreList : ScriptableObject
    {
        public List<Ore> ores;
    }
}