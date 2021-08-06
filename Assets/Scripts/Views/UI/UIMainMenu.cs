using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIMainMenu : MonoBehaviour
{
    public TextMeshProUGUI txtUsername;
    public Image imgPlayer;

    public static UIMainMenu Instance { get; set; }
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ShowInfoPlayer(NetworkController.Instance._player);
    }

    public void ShowInfoPlayer (Player infoPlayer)
    {
        txtUsername.text = infoPlayer.username;
        imgPlayer.gameObject.GetComponent<LoadImage>().ShowImage(infoPlayer.image);
    }

    public void Btn_GoToStoryBook()
    {
        SceneManager.LoadScene("storybook");
    }
}
