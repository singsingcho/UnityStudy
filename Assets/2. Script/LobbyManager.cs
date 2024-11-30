using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button startButton;
    [SerializeField] private TMP_Text warningMessage;

    private void Start()
    {
        startButton.onClick.AddListener(StartGame);

        warningMessage.text = "";
    }

    void StartGame()
    {
        if (string.IsNullOrEmpty(inputField.text))
        {
            warningMessage.text = "Input Your Name!!!!";

            if (warningCor == null)
            {
                warningCor = StartCoroutine(Warning());
            }
        }
        else
        {
            GameManager.Instance.SetUserName(inputField.text);

            SceneManager.LoadScene(1);
        }
    }

    Coroutine warningCor = null;
    WaitForSeconds wfs = new WaitForSeconds(3f);
    IEnumerator Warning()
    {
        yield return wfs;

        warningMessage.text = "";

        warningCor = null;
    }
}
