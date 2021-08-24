using UnityEngine;
using TMPro;

public class UIMiniGame : MonoBehaviour
{
    public TextMeshProUGUI txtMsgGame;
    public TextMeshProUGUI txtNumStars;

    MessagesGame msgs = new MessagesGame();

    private bool isPaused = false;

    public static UIMiniGame Instance { get; set; }

    private void Awake()
    {
        Instance = this;
        isPaused = false;
    }

    void Start()
    {
        txtMsgGame.text = "";

        UpdateNumTextStars();
    }

    public void GoalCompleted()
    {
        txtMsgGame.text = msgs.MessagesGoalCompleted();
    }

    public void UpdateNumTextStars()
    {
        txtNumStars.text = PlayerPrefs.GetInt("player_stars", 0).ToString();
    }
}
