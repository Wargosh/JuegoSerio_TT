using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkController : MonoBehaviour
{
    public string server = "https://servidor-tt.herokuapp.com/";
    public Player _player; // mantiene la info del jugador actualizada
    public static NetworkController Instance { get; set; }

    private void Awake()
    {
        Instance = this;
        // evitar que este componente (y objeto contenedor) se destruya al cargar una nueva escena
        DontDestroyOnLoad(this.gameObject);

        _player = new Player();
    }

    public void SaveInfoPlayerToServer()
    {
        WWWForm form = new WWWForm();
        form.AddField("_username", _player.username);
        form.AddField("_image", _player.image);
        form.AddField("_age_class", _player.age_class);
        form.AddField("_experience", _player._experience);
        form.AddField("_level", _player._level);
        form.AddField("_points", _player._points);
        form.AddField("_totalGames", _player._totalGames);
        form.AddField("_totalProgress", _player._totalProgress);
        form.AddField("_totalReadings", _player._totalReadings);
        form.AddField("_totalTime", _player._totalTime);
        form.AddField("_totalHits", _player._totalHits);
        form.AddField("_totalFails", _player._totalFails);

        StartCoroutine(OnSingUP_ToServer(form));
    }

    IEnumerator OnSingUP_ToServer(WWWForm form)
    {
        UnityWebRequest www = UnityWebRequest.Post(server + "player/update_info/" + _player._id, form);
        yield return www.SendWebRequest();

        if (!string.IsNullOrEmpty(www.error))
            Debug.LogError("ERROR: " + www.error);
        else
        {
            if (www.downloadHandler.text.Contains("Error"))
                Debug.LogError("ERROR: " + www.downloadHandler.text);
            else
            {
                Debug.Log("Result: " + www.downloadHandler.text);
                _player = (Player)JsonUtility.FromJson(www.downloadHandler.text.ToString(), typeof(Player));

                Debug.Log("Datos guardados.");
                // mostrar los datos actualizados
                UIProfile.Instance.ShowInfoProfile();
            }
        }
    }
}
