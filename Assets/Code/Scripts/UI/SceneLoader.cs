using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Scripts.UI
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private Animator _loadingScreenAnimator;
        [SerializeField] private bool _disableLoadingScreenOnStart = true;

        private int _targetSceneBuildIndex;
        private static readonly int ActivateLoadingScreen = Animator.StringToHash("ActivateLoadingScreen");
        private static readonly int DisableLoadingScreen = Animator.StringToHash("DisableLoadingScreen");

        private void Start()
        {
            if (_disableLoadingScreenOnStart) HideLoadingScene();
        }

        public void StartLoadingScene(int buildIndex)
        {
            _targetSceneBuildIndex = buildIndex;
            _loadingScreenAnimator.SetTrigger(ActivateLoadingScreen);
        }

        public void ChangeSceneToTarget()
        {
            SceneManager.LoadScene(_targetSceneBuildIndex);
            _targetSceneBuildIndex = -1;
        }

        public void HideLoadingScene()
        {
            _loadingScreenAnimator.SetTrigger(DisableLoadingScreen);
        }
    }
}
