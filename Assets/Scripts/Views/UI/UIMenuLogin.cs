using UnityEngine;
using TMPro;

public class UIMenuLogin : MonoBehaviour
{
    public TextMeshProUGUI txtVersion;
    public TextMeshProUGUI txtMsgServer;

    public CanvasGroup canvasGroupSingIn;

    public GameObject panelLoading;

    public static UIMenuLogin Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        txtVersion.text = "Versión: (Alpha) " + Application.version;
    }

    public void showMessagesServer (string msg)
    {
        txtMsgServer.text = msg;
    }

    public void BTN_OnSignInGoogle()
    {
        MenuSingInController.Instance.SignInWithGoogle();
    }

    public void ShowPanelLoading()
    {
        panelLoading.SetActive(true);
        DisableInputsPanelSingIn();
    }

    public void HidePanelLoading()
    {
        panelLoading.SetActive(false);
        EnableInputsPanelSingIn();
    }

    public void EnableInputsPanelSingIn()
    {
        canvasGroupSingIn.interactable = true;
        canvasGroupSingIn.blocksRaycasts = true;
    }

    public void DisableInputsPanelSingIn()
    {
        canvasGroupSingIn.interactable = false;
        canvasGroupSingIn.blocksRaycasts = false;
    }
}
