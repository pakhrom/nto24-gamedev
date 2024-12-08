using UnityEngine;

namespace Code.Scripts.UI
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private SceneLoader _sceneLoader;
        
        public void OnLoadingScreenAnimationEnd()
        {
            if (!_sceneLoader) _sceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
            _sceneLoader.ChangeSceneToTarget();
        }
    }
}