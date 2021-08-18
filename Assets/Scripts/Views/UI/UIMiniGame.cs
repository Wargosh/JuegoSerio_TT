using UnityEngine;
using TMPro;

public class UIMiniGame : MonoBehaviour
{
    public TextMeshProUGUI txtMsgGame;

    MessagesGame msgs = new MessagesGame();

    public static UIMiniGame Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        txtMsgGame.text = "";
    }

    public void GoalCompleted()
    {
        txtMsgGame.text = msgs.MessagesGoalCompleted();
    }
}
