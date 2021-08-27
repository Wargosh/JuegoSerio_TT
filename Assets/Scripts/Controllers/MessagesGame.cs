using UnityEngine;

public class MessagesGame
{
    public string MessagesGoalCompleted(int rr = -1)
    {
        if (rr == -1)
            rr = Random.Range(5, 9);
        switch (rr)
        {
            case 0: return "¡Excelente!";
               
            case 1: return "¡Genial!";

            case 2: return "¡Perfecto!";

            case 3: return "¡Increíble!";

            case 4: return "¡Impresionante!";

            case 5: return "¡Lo haces muy bien!";

            case 6: return "¡Correcto!";

            case 7: return "¡Bien hecho!";

            case 8: return "¡Wow, sigue así!";

            default: return "¡Excelente!";
        }
    }

    public string MessagesGoalFailed()
    {
        int rr = Random.Range(0, 6);
        switch (rr)
        {
            case 0:
                return "¡Intentalo nuevamente!";
            case 1:
                return "¡Fallaste esta vez, tú puedes!";
            case 2:
                return "\"No importa que tan lento vayas, siempre que no te detengas\"";
            case 3:
                return "\"Todo lo que necesitas para lograr tus metas ya esta en ti\"";
            case 4:
                return "\"Aveces se necesita fallar para aprender\"";
            case 5:
                return "¡No te rindas!";
            default:
                return "¡Intentalo nuevamente!";
        }
    }
}
