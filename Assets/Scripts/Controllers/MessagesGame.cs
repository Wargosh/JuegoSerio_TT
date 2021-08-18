using UnityEngine;

public class MessagesGame
{
    public string MessagesGoalCompleted()
    {
        int rr = Random.Range(0, 6);
        switch (rr)
        {
            case 0:
                return "¡Excelente!";
            case 1:
                return "¡Bien hecho!";
            case 2:
                return "¡Correcto!";
            case 3:
                return "¡Lo haces muy bien!";
            case 4:
                return "¡Perfecto!";
            case 5:
                return "¡Genial!";
            default:
                return "¡Excelente!";
        }
    }
}
