using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text playTimeText;
    [SerializeField] private Button toLobbyButton;

    private void Start()
    {
        nameText.text = GameManager.Instance.userName.ToString();
        playTimeText.text = string.Format("Play Time : {0:N2} second", GameManager.Instance.playTime);
        toLobbyButton.onClick.AddListener(ToLobby);
    }

    void ToLobby()
    {
        SceneManager.LoadScene(0);
    }
}
