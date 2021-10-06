using UnityEngine;

public class UIMenuOptions : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject panelMenuOptions;
    public GameObject panelGeneralStats;
    public GameObject panelStats;
    public GameObject panelConfirmLogOut;
    public GameObject panelConfirmResetProgress;

    [Header("Botones")]
    public CanvasGroup btnResetProgress;
    public GameObject btnGeneralStats;

    public void Btn_LogOut()
    {
        // borrar datos de sesion actual
        PlayerPrefsManager.Instance.RestoreData_LogOut();

        // regresar a la escena de logueo
        ScenesController.Instance.LoadSceneName("login_menu");
    }

    public void Btn_ResetProgress()
    {
        // borrar datos de minijuegos, puntos, etc...
        PlayerPrefsManager.Instance.RestoreDataPlayerToDefault();
        // regresar al menu de opciones
        Btn_HidePanelConfirmResetProgress();
        // deshabilitar boton de "Restablecer Progreso"
        btnResetProgress.interactable = false;
        btnResetProgress.alpha = 0.5f;
    }

    public void Btn_HidePanelGeneralStats()
    {
        panelGeneralStats.SetActive(false);
    }

    public void Btn_ShowPanelGeneralStats()
    {
        panelGeneralStats.SetActive(true);
    }

    public void Btn_HidePanelMyStats()
    {
        panelStats.SetActive(false);
    }

    public void Btn_ShowPanelMyStats()
    {
        panelStats.SetActive(true);
    }

    public void Btn_HidePanelConfirmLogOut()
    {
        panelConfirmLogOut.SetActive(false);
    }

    public void Btn_ShowPanelConfirmLogOut()
    {
        panelConfirmLogOut.SetActive(true);
    }

    public void Btn_HidePanelConfirmResetProgress()
    {
        panelConfirmResetProgress.SetActive(false);
    }

    public void Btn_ShowPanelConfirmResetProgress()
    {
        panelConfirmResetProgress.SetActive(true);
    }

    public void Btn_HidePanelMenuOptions ()
    {
        panelMenuOptions.SetActive(false);
    }

    public void Btn_ShowPanelMenuOptions()
    {
        panelMenuOptions.SetActive(true);
        if (NetworkController.Instance._player.role != "@dM1ñ")
            btnGeneralStats.SetActive(false);
        if (PlayerPrefsManager.Instance.getStars() == 0)
        {
            btnResetProgress.alpha = 0.5f;
            btnResetProgress.interactable = false;
        }
    }
}
