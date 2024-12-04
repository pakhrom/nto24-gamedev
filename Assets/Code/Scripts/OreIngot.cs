using Code.Scripts.ScriptableObjects;
using UnityEngine;

namespace Code.Scripts
{
    public class OreIngot : MonoBehaviour
    {
        public Ore ore;

        public void Remove()
        {
            Destroy(gameObject);
        }
    }
}
