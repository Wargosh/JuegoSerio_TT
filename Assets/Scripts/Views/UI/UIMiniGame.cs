using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UIMiniGame : MonoBehaviour
{
    public int earnedStars = 0;

    public TextMeshProUGUI txtMsgGame;
    public TextMeshProUGUI txtNumStars;

    public GameObject panelGoToMainMenu;
    public Animator panelResumeGame;
    public Animator[] coinStars;
    public Button btnRestart;
    public Button btnContinue;

    MessagesGame msgs = new MessagesGame();

    public static UIMiniGame Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        txtMsgGame.text = "";

        UpdateNumTextStars();
    }

    public void GoalCompleted()
    {
        earnedStars = 3 - MinigameController_Mask.Instance.VerifyFails();

        ShowPanelResumeGame();
    }

    public void UpdateNumTextStars()
    {
        txtNumStars.text = PlayerPrefsManager.Instance.getStars().ToString();
    }

    public void Btn_GoToMainMenu()
    {
        ScenesController.Instance.LoadSceneName("main_menu");
    }

    public void Btn_ShowPanelGoToMainMenu()
    {
        panelGoToMainMenu.SetActive(true);
    }

    public void Btn_HidePanelGoToMainMenu()
    {
        panelGoToMainMenu.SetActive(false);
    }

    public void ShowPanelResumeGame()
    {
        panelResumeGame.SetBool("showPanel", true);
        SaveEarnedStars();
    }

    private void SaveEarnedStars()
    {
        PlayerPrefsManager.Instance.setStars(PlayerPrefsManager.Instance.getStars() + earnedStars);
        PlayerPrefsManager.Instance.LoadDataPlayer();

        if (earnedStars == 3)
        {
            btnContinue.interactable = true;
            btnRestart.interactable = false;
            txtMsgGame.text = msgs.MessagesGoalCompleted(Random.Range(0, 5));
        }
        else if (earnedStars > 0)
        {
            txtMsgGame.text = msgs.MessagesGoalCompleted();
            btnContinue.interactable = true;
            btnRestart.interactable = true;
        }
        else
        {
            btnContinue.interactable = false;
            txtMsgGame.text = msgs.MessagesGoalFailed();
        }
        
        ShowEarnedStars(earnedStars);
        UpdateNumTextStars();
        NetworkController.Instance.SaveInfoPlayerToServer();
    }

    private void HidePanelResumeGame()
    {
        panelResumeGame.SetBool("showPanel", false);
    }

    public void Btn_RestartLevel()
    {
        MinigameController_Mask.Instance.Btn_RestartMinigame();

        HidePanelResumeGame();
        ResetStars();
    }

    public void Btn_NewLevel_Mask()
    {
        int level_mask = PlayerPrefsManager.Instance.getLevelMinigame_Mask();
        //level_mask++;
        ScenesController.Instance.LoadSceneName("level_mask_n");
    }

    public void ShowEarnedStars(int num)
    {
        if (num < 1 || num > 3) return;

        StartCoroutine(ShowAnimEarnedStars(num));
    }
    private IEnumerator ShowAnimEarnedStars(int num)
    {
        for (int i = 0; i < num; i++)
        {
            yield return new WaitForSeconds(.25f);
            coinStars[i].SetBool("winStar", true);
        }
    }

    private void ResetStars()
    {
        earnedStars = 0;
        for (int i = 0; i < coinStars.Length; i++)
            coinStars[i].SetBool("winStar", false);
    }
}