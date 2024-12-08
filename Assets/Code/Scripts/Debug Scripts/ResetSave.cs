using System.IO;
using UnityEngine;

namespace Code.Scripts.Debug_Scripts
{
    public class ResetSave : MonoBehaviour
    {
        [SerializeField] private SaveManager _saveManager;

        private void Start()
        {
            if (!_saveManager) _saveManager = GameObject.Find("SaveManager").GetComponent<SaveManager>();
        }

        public void Reset()
        {
            File.Delete(Application.persistentDataPath + "/save/save_data");
            Application.Quit();
        }
    }
}
