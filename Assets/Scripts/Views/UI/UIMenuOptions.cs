using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuOptions : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject panelGeneralStats;
    public GameObject panelStats;
    public GameObject panelConfirmLogOut;
    public GameObject panelConfirmResetProgress;

    public void Btn_LogOut()
    {
        // borrar datos de sesion actual
        // regresar a la escena de logueo
    }

    public void Btn_ResetProgress()
    {
        // borrar datos de minijuegos, puntos, etc...
        // regresar al menu de opciones
        Btn_HidePanelConfirmResetProgress();
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
}
