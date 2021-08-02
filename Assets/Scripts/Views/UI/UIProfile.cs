using UnityEngine;
using TMPro;

public class UIProfile : MonoBehaviour
{
    [Header("Opciones del panel")]
    public GameObject panelProfile;

    [Header("Información del jugador")]
    public TextMeshProUGUI txtUsername;
    public TextMeshProUGUI txtAge;

    public LoadImage imgPlayer;

    public static UIProfile Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    private void ShowInfoProfile()
    {
        Player infoProfile = NetworkController.Instance._player;

        txtUsername.text = infoProfile.username;
        txtAge.text = infoProfile.age_class;

        imgPlayer.ShowImage(infoProfile.image);
    }

    public void Btn_ShowPanelProfile() 
    {
        panelProfile.SetActive(true);

        ShowInfoProfile();
    }

    public void Btn_HidePanelProfile()
    {
        panelProfile.SetActive(false);
    }
}
