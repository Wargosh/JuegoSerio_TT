using UnityEngine;

public class ProfileController : MonoBehaviour
{
    public static ProfileController Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    public void SaveNewUsername(string name)
    {
        PlayerPrefs.SetString("player_username", name);

        NetworkController.Instance._player.username = name;
        NetworkController.Instance.SaveInfoPlayerToServer();

        UIMainMenu.Instance.ShowInfoPlayer(NetworkController.Instance._player);
    }

    public void SaveAgeClass(int age_class)
    {
        PlayerPrefs.SetInt("player_age", age_class);

        NetworkController.Instance._player.age_class = age_class;
        NetworkController.Instance.SaveInfoPlayerToServer();
    }

    public void SaveNewImage(string newImg)
    {
        PlayerPrefs.SetString("player_image", newImg);

        NetworkController.Instance._player.image = newImg;
        NetworkController.Instance.SaveInfoPlayerToServer();

        UIMainMenu.Instance.ShowInfoPlayer(NetworkController.Instance._player);
    }

    public string GetAgeClassFromIndex(int i)
    {
        switch (i)
        {
            case 0:
                return "Menos de 7 años";
            case 1:
                return "Entre 7 a 9 años";
            case 2:
                return "Entre 10 a 12 años";
            case 3:
                return "Más de 12 años";
            default:
                return "Menos de 7 años";
        }
    }
}
