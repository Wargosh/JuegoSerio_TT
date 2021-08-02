using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnBackController : MonoBehaviour
{
	void Update()
	{
		//si se presiona el boton de 'Atras'
		if (Input.GetButtonUp("Cancel") || Input.GetKeyUp(KeyCode.Escape))
			VerifyCurrentScene();
	}

	private void VerifyCurrentScene()
	{
		// Escena de Logeo
		Scene scene = SceneManager.GetActiveScene();
		switch (scene.name)
		{
			case "login_menu":

				break;
			case "main_menu":
				// pantalla modos de juego
				/*if (MainMenuController.Instance.panelGameModes.activeSelf)
				{
					MainMenuController.Instance.BTN_HidePanelGameModes();
				}
				// pantalla de perfil
				else */if (UIProfile.Instance.panelProfile.activeSelf)
				{
					/*if (ProfileController.Instance.panelViewImageProfile.activeSelf)
						ProfileController.Instance.BTN_HideImageProfile();
					else
						ProfileController.Instance.BTN_HidePanelProfile();*/
					UIProfile.Instance.Btn_HidePanelProfile();
				}
				break;
			case "storybook":
				/*// pantalla de carga (para evitar salir o desabilitar algo sin querer)
				if (!PanelLoadingGame.Instance.panelLoading.activeSelf)
				{
					// pantalla de reportar pregunta
					if (ReportController.Instance.contentReportQuestion.activeSelf)
					{
						ReportController.Instance.DisableContentReportQuestion();
					}
					// pantalla de advertencia 'salir al menu' mientras esta en una partida
					else if (PanelMenuQuitGame.Instance.contentQuitGame.activeSelf)
					{
						PanelMenuQuitGame.Instance.BTN_DisablePanelQuitGame();
					}
					else
					{
						PanelMenuQuitGame.Instance.BTN_EnablePanelQuitGame();
					}
				}*/
				break;
		}
	}
}
