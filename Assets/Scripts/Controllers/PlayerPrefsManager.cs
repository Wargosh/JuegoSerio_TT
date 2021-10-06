using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    public static PlayerPrefsManager Instance { get; set; }
    private void Awake()
    {
        Instance = this;
    }

    public string getUsername()
    {
        return PlayerPrefs.GetString("player_username", "");
    }
    public void setUsername(string name)
    {
        PlayerPrefs.SetString("player_username", name);
    }

    public string getEmail()
    {
        return PlayerPrefs.GetString("player_email", "");
    }
    public void setEmail(string email)
    {
        PlayerPrefs.SetString("player_email", email);
    }

    public int getAge()
    {
        return PlayerPrefs.GetInt("player_age", 0);
    }
    public void setAge(int age_class)
    {
        PlayerPrefs.SetInt("player_age", age_class);
    }

    public string getImage()
    {
        return PlayerPrefs.GetString("player_image", "");
    }
    public void setImage(string img)
    {
        PlayerPrefs.SetString("player_image", img);
    }

    public string getPassword()
    {
        return PlayerPrefs.GetString("player_password", "");
    }
    public void setPassword(string pass)
    {
        PlayerPrefs.SetString("player_password", pass);
    }

    public string getSignInMethod()
    {
        return PlayerPrefs.GetString("singin_method", "Normal");
    }
    public void setSignInMethod(string method)
    {
        PlayerPrefs.SetString("singin_method", method);
    }

    public int getStars()
    {
        return PlayerPrefs.GetInt("player_stars", 0);
    }
    public void setStars(int stars)
    {
        PlayerPrefs.SetInt("player_stars", stars);
    }

    public int getLevelMinigame_Mask()
    {
        return PlayerPrefs.GetInt("player_level_minigameMask", 1);
    }
    public void setLevelMinigame_Mask(int level)
    {
        PlayerPrefs.SetInt("player_level_minigameMask", level);
    }

    public void LoadDataPlayer()
    {
        // Información general
        NetworkController.Instance._player.username = PlayerPrefs.GetString("player_username", "");
        NetworkController.Instance._player.email = PlayerPrefs.GetString("player_email", "");
        NetworkController.Instance._player.age_class = PlayerPrefs.GetInt("player_age", 0);
        NetworkController.Instance._player.image = PlayerPrefs.GetString("player_image", "");
        // puntos y estadisticas
        NetworkController.Instance._player._stars = PlayerPrefs.GetInt("player_stars", 0);
        NetworkController.Instance._player._experience = PlayerPrefs.GetInt("player_xp", 0);
        NetworkController.Instance._player._level = PlayerPrefs.GetInt("player_level", 1);
        NetworkController.Instance._player._points = PlayerPrefs.GetInt("player_points", 0);
        NetworkController.Instance._player._totalStars = PlayerPrefs.GetInt("player_totalStars", 0);
        NetworkController.Instance._player._totalFails = PlayerPrefs.GetInt("player_totalFails", 0);
        NetworkController.Instance._player._totalGames = PlayerPrefs.GetInt("player_totalGames", 0);
        NetworkController.Instance._player._totalHits = PlayerPrefs.GetInt("player_totalHits", 0);
        NetworkController.Instance._player._totalProgress = PlayerPrefs.GetInt("player_totalProgress", 0);
        NetworkController.Instance._player._totalReadings = PlayerPrefs.GetInt("player_totalReadings", 0);
        NetworkController.Instance._player._totalTime = PlayerPrefs.GetInt("player_totalTime", 0);
        // progreso en niveles
        NetworkController.Instance._player._level_minigameMask = PlayerPrefs.GetInt("player_level_minigameMask", 1);
    }

    public void RestoreDataPlayerToDefault()
    {
        // puntos y estadisticas
        PlayerPrefs.SetInt("player_stars", 0);
        PlayerPrefs.SetInt("player_xp", 0);
        PlayerPrefs.SetInt("player_level", 1);
        PlayerPrefs.SetInt("player_points", 0);
        PlayerPrefs.SetInt("player_totalStars", 0);
        PlayerPrefs.SetInt("player_totalFails", 0);
        PlayerPrefs.SetInt("player_totalGames", 0);
        PlayerPrefs.SetInt("player_totalHits", 0);
        PlayerPrefs.SetInt("player_totalProgress", 0);
        PlayerPrefs.SetInt("player_totalReadings", 0);
        PlayerPrefs.SetInt("player_totalTime", 0);
        // progreso en niveles
        PlayerPrefs.SetInt("player_level_minigameMask", 1);

        LoadDataPlayer();
    }

    public void RestoreData_LogOut()
    {
        PlayerPrefs.SetString("player_username", "");
        PlayerPrefs.SetString("player_email", "");
        PlayerPrefs.SetString("player_password", "");
        PlayerPrefs.SetString("singin_method", "");
    }
}
