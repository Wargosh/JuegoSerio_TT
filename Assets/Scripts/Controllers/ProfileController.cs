using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileController : MonoBehaviour
{
    public static ProfileController Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    public void SaveAgeClass(int age_class)
    {
        PlayerPrefs.SetInt("Player:Age", age_class);
        NetworkController.Instance._player.age_class = age_class;

        NetworkController.Instance.SaveInfoPlayerToServer();
    }

    public string GetAgeClassFromIndex(int i)
    {
        print(i);
        switch (i)
        {
            case 0:
                return "Menos de 7 a�os";
            case 1:
                return "Entre 7 a 9 a�os";
            case 2:
                return "Entre 10 a 12 a�os";
            case 3:
                return "M�s de 12 a�os";
            default:
                return "Menos de 7 a�os";
        }
    }
}
