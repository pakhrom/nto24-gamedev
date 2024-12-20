using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Code.Scripts.ScriptableObjects;
using UnityEngine;

namespace Code.Scripts
{
    public class SaveManager : MonoBehaviour
    {
        [SerializeField] private Planet _defaultPlanet;
        
        private SaveData _saveData;
        [NonSerialized] public Inventory tempInventory;
        
        private static SaveManager _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(this);
            }
            DontDestroyOnLoad(this);

            _saveData = ScriptableObject.CreateInstance<SaveData>();
            LoadGame();
        }

        public SaveData GetSaveData()
        {
            if (!_saveData && !IsSaveDataExists())
            {
                CreateEmptySave();
            }
            else if (!_saveData)
            {
                LoadGame();
            }
            
            return _saveData;
        }

        private bool IsSaveDataExists()
        {
            return (Directory.Exists(Application.persistentDataPath + "/save") && File.Exists(Application.persistentDataPath +"/save/save_data"));
        }

        private void CreateEmptySave()
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/save");

            _saveData = ScriptableObject.CreateInstance<SaveData>();
            _saveData.currentPlanet = _defaultPlanet;
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/save/save_data");
            var json = JsonUtility.ToJson(_saveData);
            binaryFormatter.Serialize(file, json);
        }

        public void SaveGame()
        {
            if (!IsSaveDataExists())
            {
                CreateEmptySave();
            }

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/save/save_data");
            var json = JsonUtility.ToJson(_saveData);
            binaryFormatter.Serialize(file, json);
            file.Close();
        }

        private void LoadGame()
        {
            if (!IsSaveDataExists())
            {
                CreateEmptySave();
                Debug.Log("New save file created.");
                return;
            }

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/save/save_data", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)binaryFormatter.Deserialize(file), _saveData);
            file.Close();
        }
    }
}
