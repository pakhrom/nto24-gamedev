using UnityEngine;

namespace Code.Scripts.UI
{
    public class LaunchButton : MonoBehaviour
    {
        [SerializeField] private Animator _rocketAnimator;

        [Header("Objects to disable on launch")] 
        [SerializeField] private Controller2DInput _controllerInput;
        [SerializeField] private Rigidbody2D _playerRigidbody;

        public void Launch()
        {
            _controllerInput.enabled = false;
            _playerRigidbody.simulated = false;
        }
    }
}
