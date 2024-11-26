using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    private List<string> allNames = new();
    private List<string> allMessages = new();
    private Text message;
    private new Text name;
    private int click;
    private string stringOfText;
    private int dialogLength;
    [SerializeField] private float textSpeed;
    [SerializeField] private GameObject dialogMessage;
    [SerializeField] private GameObject dialogName;
    [SerializeField] private GameObject dialogPanel;
    void Start()
    {
        message = dialogMessage.GetComponent<Text>();
        name = dialogName.GetComponent<Text>();
    }
    public void StartDialog((string, string)[] data)
    {
        click = 0;
        dialogLength = data.Length;
        dialogMessage.transform.parent.gameObject.SetActive(true);
        foreach ((string, string) e in data)
        {
            allNames.Add(e.Item1.ToString());
            allMessages.Add(e.Item2.ToString());
        }
        name.text = allNames[0];
        stringOfText = allMessages[0];
        StartCoroutine(TextWriter());
    }
    private void OnMouseDown()
    {
        click++;
        StopAllCoroutines();
        message.text = "";
        if (click < dialogLength)
        {
            name.text = allNames[click];
            stringOfText = allMessages[click];
            StartCoroutine(TextWriter());
        }
        else
            dialogMessage.transform.parent.gameObject.SetActive(false);
    }
    IEnumerator TextWriter()
    {
        string part = "";
        foreach (char character in stringOfText)
        {
            part += character;
            message.text = part;
            yield return new WaitForSeconds(textSpeed / 1000);
        }
    }
}
