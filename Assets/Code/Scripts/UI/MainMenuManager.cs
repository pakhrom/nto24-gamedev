using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Newtonsoft.Json;

namespace Code.Scripts.UI
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private SaveManager _saveManager;

        [SerializeField] private GameObject _loginMenu;
        [SerializeField] private TMP_InputField _loginInput;
        [SerializeField] private Animator _settingsPanelAnimator;
        
        private static readonly int ShowSettingsPanel = Animator.StringToHash("ShowSettingsPanel");
        private static readonly int HideSettingsPanel = Animator.StringToHash("HideSettingsPanel");

        private void Start()
        {
            if (string.IsNullOrWhiteSpace(_saveManager.GetSaveData().playerName)) _loginMenu.SetActive(true);
        }

        public void Login()
        {
            if (!string.IsNullOrWhiteSpace(_loginInput.text))
            {
                
                // TODO: if player exists on server, load his inventory, if not, create one
                _saveManager.GetSaveData().playerName = _loginInput.text;
                _saveManager.SaveGame();
                _loginMenu.SetActive(false);
            }
        }

        public void ShowSettings()
        {
            _settingsPanelAnimator.SetTrigger(ShowSettingsPanel);
        }

        public void HideSettings()
        {
            _settingsPanelAnimator.SetTrigger(HideSettingsPanel);
        }
    }

    public class AllPlayers
    {
        // [{"name":"user_with_apples","resources":{"wheat":"34","apples":3}}]
        // public List<Dictionary<>>;
    }
}
