using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
