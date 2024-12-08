using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using Code.Scripts;
using UnityEditor.PackageManager.Requests;

public class Api : MonoBehaviour
{
    [SerializeField] private SaveManager _saveManager;
    
    private string _playerName;
    private string _lastRequest;
    
    readonly private string _uuid = "47d85299-f909-4ab5-9b5f-ca5fce2f597d";
    void Start()
    {
        if (!_saveManager) _saveManager = GameObject.Find("SaveManager").GetComponent<SaveManager>();

        _playerName = _saveManager.GetSaveData().playerName;
        GetAllPlayers();
        
    }

    public string GetLastRequest()
    {
        return !string.IsNullOrWhiteSpace(_lastRequest) ? _lastRequest : null;
    }
    
    public void CreatePlayer(string request)
    {
        StartCoroutine(Post("https://2025.nti-gamedev.ru/api/games/" + _uuid + "/players/", request));
    }
    public void GetAllPlayers()
    {
        StartCoroutine(Get("https://2025.nti-gamedev.ru/api/games/" + _uuid + "/players/"));
    }
    public void GetPlayerResources(string userName)
    {
        StartCoroutine(Get("https://2025.nti-gamedev.ru/api/games/" + _uuid + "/players/" + userName + "/"));
    }
    public void ReloadPlayerResources(string userName)
    {
        StartCoroutine(Put("https://2025.nti-gamedev.ru/api/games/" + _uuid + "/players/" + userName + "/", "{\r\n   \"resources\": {\r\n       \"apples\": 3,\r\n       \"wheat\": \"34\"\r\n   }\r\n}"));
    }
    public void CreateLogs(string request)
    {
        StartCoroutine(Post("https://2025.nti-gamedev.ru/api/games/" + _uuid + "/logs/", request));
    }
    public void GetPlayerLogs(string userName)
    {
        StartCoroutine(Get("https://2025.nti-gamedev.ru/api/games/" + _uuid + "/players/" + userName + "/"));
    }
    public void GetShopLogs(string userName, string shopName)
    {
        StartCoroutine(Get("https://2025.nti-gamedev.ru/api/games/" + _uuid + "/players/" + userName + "/shops/" + shopName + "/logs/"));
    }
    public void CreatePlayerShop(string userName, string request)
    {
        StartCoroutine(Post("https://2025.nti-gamedev.ru/api/games/" + _uuid + "/players/" + userName + "/shops/", request));
    }
    public void GetPlayerShops(string userName)
    {
        StartCoroutine(Get("https://2025.nti-gamedev.ru/api/games/" + _uuid + "/players/" + userName + "/shops/"));
    }
    public void GetPlayerShopResources(string userName, string shopName)
    {
        StartCoroutine(Get("https://2025.nti-gamedev.ru/api/games/" + _uuid + "/players/" + userName + "/shops/" + shopName + "/"));
    }
    public void ReloadPlayerShopResources(string userName, string shopName, string request)
    {
        StartCoroutine(Put("https://2025.nti-gamedev.ru/api/games/" + _uuid + "/players/" + userName + "/shops/" + shopName + "/", request));
    }
    IEnumerator Post(string url, string bodyJsonString)
    {
        var www = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
    }
    IEnumerator Get(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();
            string[] pages = url.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                break;
                case UnityWebRequest.Result.Success:
                    _lastRequest = webRequest.downloadHandler.text;
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                break;
            }
        }
    }
    IEnumerator Put(string url, string bodyJsonString)
    {
        UnityWebRequest www = UnityWebRequest.Put(url, bodyJsonString);
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
    }
}
