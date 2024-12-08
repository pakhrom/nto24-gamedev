using UnityEngine;

namespace Code.Scripts.UI
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private SceneLoader _sceneLoader;
        
        public void OnLoadingScreenAnimationEnd()
        {
            _sceneLoader.ChangeSceneToTarget();
        }
    }
}