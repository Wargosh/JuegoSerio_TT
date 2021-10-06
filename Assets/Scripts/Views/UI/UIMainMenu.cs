using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMainMenu : MonoBehaviour
{
    public TextMeshProUGUI txtUsername;
    public TextMeshProUGUI txtNumStars;
    public Image imgPlayer;

    public GameObject panelSelectMinigames;

    public static UIMainMenu Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ShowInfoPlayer(NetworkController.Instance._player);
    }

    public void ShowInfoPlayer(Player infoPlayer)
    {
        txtUsername.text = infoPlayer.username;
        txtNumStars.text = infoPlayer._stars.ToString();
        imgPlayer.gameObject.GetComponent<LoadImage>().ShowImage(infoPlayer.image);
    }

    public void Btn_ShowPanelSelectMinigames()
    {
        panelSelectMinigames.SetActive(true);
    }

    public void Btn_HidePanelSelectMinigames()
    {
        panelSelectMinigames.SetActive(false);
    }

    public void Btn_GoToStoryBook()
    {
        ScenesController.Instance.LoadSceneName("storybook");
    }

    public void Btn_GoToLevel_UseOfMask()
    {
        ScenesController.Instance.LoadSceneName("level_mask_n");
    }

    public void Btn_GoToLevel_UseOfAlcohol()
    {
        //SceneManager.LoadScene("storybook");
    }

    public void Btn_GoToLevel_SafeDistance()
    {
        //SceneManager.LoadScene("storybook");
    }
    public void Btn_GoToLevel_HandWashing()
    {
        //SceneManager.LoadScene("storybook");
    }
}
