using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google;
using UnityEngine;
using UnityEngine.Networking;

public class MenuSingInController : MonoBehaviour
{
    [Header("Configuracion Firebase")]
    public string webClientId = "<your client id here>";

    private GoogleSignInConfiguration configuration;

    private List<string> messages = new List<string>();

    public static MenuSingInController Instance { set; get; }

    private void Awake()
    {
        Instance = this;
        configuration = new GoogleSignInConfiguration
        {
            WebClientId = webClientId,
            RequestEmail = true,
            RequestIdToken = true
        };
    }

    void Start()
    {
        UIMenuLogin.Instance.ShowPanelLoading();
        StartCoroutine(TimeSliderLoadBar());
    }

    IEnumerator TimeSliderLoadBar()
    {
        bool loading = true;
        AddStatusText("Conectandose al servidor");
        while (loading)
        {
            yield return new WaitForSeconds(0.025f);
            if (ScenesController.Instance.valueLoading >= 0.1f)
            {
                loading = false;
            }
            ScenesController.Instance.valueLoading += 0.01f;
        }

        // verificar si existe una sesion almacenada
        string _username = PlayerPrefsManager.Instance.getUsername();
        string _email = PlayerPrefsManager.Instance.getEmail();
        string _password = PlayerPrefsManager.Instance.getPassword();
        string _methodSingIn = PlayerPrefsManager.Instance.getSignInMethod();
        if (_username != "" && _email != "" && _methodSingIn != "")
        {
            AddStatusText("Cargando tus datos");
            if (_methodSingIn == "Normal")
                LoginAutomatic(_email, _password);
            if (_methodSingIn == "Google")
                SignInWithGoogle();
        }
        else
            UIMenuLogin.Instance.HidePanelLoading();
    }

    IEnumerator OnSingIn_ToServer(WWWForm form)
    {
        UnityWebRequest www = UnityWebRequest.Post(NetworkController.Instance.server + "login/game", form);
        yield return www.SendWebRequest();

        if (!string.IsNullOrEmpty(www.error))
            AddStatusText(www.error);
        else
        {
            if (www.downloadHandler.text.Contains("Email o clave incorrecta."))
                AddStatusText("Email o clave incorrecta.");
            else
            {
                JsonUtility.FromJsonOverwrite(www.downloadHandler.text, NetworkController.Instance._player);

                PlayerPrefsManager.Instance.setUsername(NetworkController.Instance._player.username);
                PlayerPrefsManager.Instance.setEmail(NetworkController.Instance._player.email);

                // Una vez logueado, procede a terminar de cargar la escena
                ScenesController.Instance.LoadSceneAsync_MainMenu();
            }
        }
        UIMenuLogin.Instance.EnableInputsPanelSingIn();
    }

    /* ****************************** Sing In Normal ****************************** */
    private void LoginAutomatic(string email, string pass)
    {
        WWWForm form = new WWWForm();
        form.AddField("_email", email);
        form.AddField("_password", pass);

        StartCoroutine(OnSingIn_ToServer(form));
    }

    /* ****************************** Sing Up ****************************** */
    public void BTN_OnSingUpNormal()
    {
        UIMenuLogin.Instance.ShowPanelLoading();
        WWWForm form = new WWWForm();
        form.AddField("_username", "Wargosh");
        form.AddField("_email", "wargosh3@gmail.com");
        form.AddField("_password", "12345");
        form.AddField("_image", "oso");
        form.AddField("_method", "Google");

        PlayerPrefsManager.Instance.setSignInMethod("Google");
        PlayerPrefsManager.Instance.setPassword("12345");

        StartCoroutine(OnSingUP_ToServer(form));
    }

    IEnumerator OnSingUP_ToServer(WWWForm form)
    {
        UnityWebRequest www = UnityWebRequest.Post(NetworkController.Instance.server + "singup/game", form);
        yield return www.SendWebRequest();

        if (!string.IsNullOrEmpty(www.error))
            AddStatusText(www.error);
        else
        {
            if (www.downloadHandler.text.Contains("Error"))
                AddStatusText(www.downloadHandler.text);
            else
            {
                Debug.Log("Result: " + www.downloadHandler.text);
                // almacenar preguntas en una lista
                //JsonUtility.FromJsonOverwrite(www.downloadHandler.text.ToString(), NetworkController.Instance._player);
                NetworkController.Instance._player = (Player)JsonUtility.FromJson(www.downloadHandler.text.ToString(), typeof(Player));
                //infoPlayer = JsonUtility.FromJson<Player>(www.downloadHandler.text);
                PlayerPrefsManager.Instance.setUsername(NetworkController.Instance._player.username);
                PlayerPrefsManager.Instance.setEmail(NetworkController.Instance._player.email);

                // Una vez registrado, procede a terminar de cargar la escena
                ScenesController.Instance.LoadSceneAsync_MainMenu();
            }
        }
        UIMenuLogin.Instance.EnableInputsPanelSingIn();
    }

    /* ****************************** Sing In With Firebase ****************************** */
    public void SignInWithGoogle()
    {
        UIMenuLogin.Instance.ShowPanelLoading();
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        AddStatusText("Calling SignIn");

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
    }

    public void OnSignOut()
    {
        AddStatusText("Calling SignOut");
        GoogleSignIn.DefaultInstance.SignOut();
    }

    public void OnDisconnect()
    {
        AddStatusText("Calling Disconnect");
        GoogleSignIn.DefaultInstance.Disconnect();
    }

    public void AddStatusText(string text)
    {
        if (messages.Count == 3)
        {
            messages.RemoveAt(0);
        }
        messages.Add(text);
        string txt = "";
        foreach (string s in messages)
        {
            txt += "\n" + s;
        }
        UIMenuLogin.Instance.showMessagesServer(txt);
    }

    internal void OnAuthenticationFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            using (IEnumerator<System.Exception> enumerator =
                    task.Exception.InnerExceptions.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    GoogleSignIn.SignInException error = (GoogleSignIn.SignInException)enumerator.Current;
                    AddStatusText("Got Error: " + error.Status + " " + error.Message);
                }
                else
                    AddStatusText("Got Unexpected Exception?!?" + task.Exception);
            }
        }
        else if (task.IsCanceled)
        {
            AddStatusText("Canceled");
        }
        else
        {
            AddStatusText("Welcome: " + task.Result.DisplayName + "!");
            AddStatusText("Email: " + task.Result.Email + "!");
            string user = task.Result.DisplayName.Replace(" ", ""); // nombres de usuario juntos

            OnSingUp_WithGoogle(user, task.Result.Email, "SeriousGame-Pr1v@t3K3y", task.Result.ImageUrl.ToString());
        }
        UIMenuLogin.Instance.EnableInputsPanelSingIn();
    }

    private void OnSingUp_WithGoogle(string user, string email, string password, string image)
    {
        UIMenuLogin.Instance.DisableInputsPanelSingIn();

        WWWForm form = new WWWForm();
        form.AddField("_username", user);
        form.AddField("_email", email);
        form.AddField("_password", password);
        form.AddField("_image", image);
        form.AddField("_method", "Google");

        // define el metodo de logueo como normal...
        PlayerPrefsManager.Instance.setSignInMethod("Google");
        PlayerPrefsManager.Instance.setPassword(password);

        StartCoroutine(OnSingUP_ToServer(form));
    }
}