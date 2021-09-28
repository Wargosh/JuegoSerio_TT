using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIProfile : MonoBehaviour
{
    [Header("Opciones de paneles")]
    public GameObject panelProfile;
    public GameObject panelSelectAge;
    public GameObject panelSelectImage;
    public GameObject panelChangeUsername;

    [Header("Información del perfil")]
    public TextMeshProUGUI txtUsername;
    public TextMeshProUGUI txtAge;
    public TMP_InputField inputUsername;

    public LoadImage imgPlayer;

    [Header("Información panel de edad")]
    public Slider sldAgeClass;
    public TextMeshProUGUI txtSldAge;

    [Header("Panel Seleccionar Imagen")]
    public GameObject ContentPanelSelectImage;
    public GameObject imgPrefab;

    public static UIProfile Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LoadSelectableImagesProfile();
    }

    private void LoadSelectableImagesProfile()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Images/User/");

        for (int i = 0; i < sprites.Length; i++)
        {
            GameObject obj = Instantiate(imgPrefab);
            obj.GetComponent<SelectableImage>().img.sprite = sprites[i];
            obj.transform.SetParent(this.ContentPanelSelectImage.transform);
            obj.transform.localScale = Vector3.one;
            obj.gameObject.name = sprites[i].name;
        }
    }

    public void ShowInfoProfile()
    {
        Player infoProfile = NetworkController.Instance._player;

        txtUsername.text = infoProfile.username;
        txtSldAge.text = txtAge.text = ProfileController.Instance.GetAgeClassFromIndex(infoProfile.age_class);
        sldAgeClass.value = infoProfile.age_class;

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

    public void Btn_ShowPanelSelectAge()
    {
        panelSelectAge.SetActive(true);
    }

    public void Btn_HidePanelSelectAge()
    {
        panelSelectAge.SetActive(false);
    }

    public void Btn_ShowPanelSelectImage()
    {
        panelSelectImage.SetActive(true);
    }

    public void Btn_HidePanelSelectImage()
    {
        panelSelectImage.SetActive(false);
    }

    public void Btn_ShowPanelChangeUsername()
    {
        panelChangeUsername.SetActive(true);

        inputUsername.text = NetworkController.Instance._player.username;
    }

    public void Btn_HidePanelChangeUsername()
    {
        panelChangeUsername.SetActive(false);
    }

    public void Btn_SaveNewUsername()
    {
        ProfileController.Instance.SaveNewUsername(inputUsername.text);

        Btn_HidePanelChangeUsername();
    }

    public void Btn_SaveAgeClass()
    {
        ProfileController.Instance.SaveAgeClass(Convert.ToInt16(sldAgeClass.value));

        Btn_HidePanelSelectAge();
    }

    public void OnChangeValueSliderAge()
    {
        txtSldAge.text = ProfileController.Instance.GetAgeClassFromIndex(Convert.ToInt16(sldAgeClass.value));
    }
}
