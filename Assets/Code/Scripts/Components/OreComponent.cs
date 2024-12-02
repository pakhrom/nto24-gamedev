using UnityEngine;

namespace Code.Scripts.Components
{
    public class OreComponent : MonoBehaviour
    {
        [Tooltip("How many hits until ore drop")]
        [SerializeField] private float _toughness;
        [Tooltip("How many drops can this ore give")]
        [SerializeField] private int _dropCount;
    }
}
