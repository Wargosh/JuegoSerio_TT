using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{
    [HideInInspector] public float valueLoading = 0f;
    public static ScenesController Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    public void LoadSceneName(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
        // evita la redundancia de controladores simultaneos y posibles errores
        if (nameScene == "login_menu")
            Destroy(this.gameObject);
    }

    public void LoadSceneAsync_MainMenu()
    {
        UIMenuLogin.Instance.ShowPanelLoading();
        StartCoroutine(LoadSceneAsync("main_menu"));
    }

    IEnumerator LoadSceneAsync(string nameScene)
    {
        yield return null;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nameScene);
        asyncOperation.allowSceneActivation = false;

        if (nameScene == "main_menu")
            MenuSingInController.Instance.AddStatusText("Cargando Juego");

        while (!asyncOperation.isDone)
        {
            valueLoading = asyncOperation.progress;
            // Comprobar si la carga finalizo
            if (asyncOperation.progress >= 0.9f)
            {
                // Activar la escena
                asyncOperation.allowSceneActivation = true;
                break;
            }
            yield return null;
        }
    }
}
