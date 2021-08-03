using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIProfile : MonoBehaviour
{
    [Header("Opciones de paneles")]
    public GameObject panelProfile;
    public GameObject panelSelectAge;

    [Header("Información del perfil")]
    public TextMeshProUGUI txtUsername;
    public TextMeshProUGUI txtAge;

    public LoadImage imgPlayer;

    [Header("Información panel de edad")]
    public Slider sldAgeClass;
    public TextMeshProUGUI txtSldAge;

    public static UIProfile Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    public void ShowInfoProfile ()
    {
        Player infoProfile = NetworkController.Instance._player;

        txtUsername.text = infoProfile.username;
        txtAge.text = ProfileController.Instance.GetAgeClassFromIndex(infoProfile.age_class);

        imgPlayer.ShowImage(infoProfile.image);
    }

    public void Btn_ShowPanelProfile () 
    {
        panelProfile.SetActive(true);

        ShowInfoProfile();
    }

    public void Btn_HidePanelProfile ()
    {
        panelProfile.SetActive(false);
    }

    public void Btn_ShowPanelSelectAge()
    {
        panelSelectAge.SetActive(true);
    }

    public void Btn_HidePanelSelectAge()
    {
        panelSelectAge.SetActive(false);
    }

    public void Btn_SaveAgeClass () {
        ProfileController.Instance.SaveAgeClass(Convert.ToInt16(sldAgeClass.value));

        Btn_HidePanelSelectAge();
    }

    public void OnChangeValueSliderAge () {
        txtSldAge.text = ProfileController.Instance.GetAgeClassFromIndex(Convert.ToInt16(sldAgeClass.value));
    }
}
