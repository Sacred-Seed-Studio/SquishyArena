using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageController : MonoBehaviour
{

    public static MessageController controller;
    public GameObject messagePanel;
    public Text messageText;

    void Awake()
    {
        if (controller == null)
        {
            DontDestroyOnLoad(gameObject);
            controller = this;
        }
        else if (controller != this)
        {
            Destroy(gameObject);
        }

    }

    public void DisplayMessage(string message, float delay = 5f)
    {
        StartCoroutine(StartDisplayMessage(message, delay));
    }

    public IEnumerator StartDisplayMessage(string message, float delay)
    {
        messagePanel.SetActive(true);
        messageText.text = message;
        yield return new WaitForSeconds(delay);
        messagePanel.SetActive(false);
        yield return null;
    }
}
