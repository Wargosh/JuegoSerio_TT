using UnityEngine;

public class CovidioController : MonoBehaviour
{
    public enum Status { Greet, Blink, Running }
    public Status status = Status.Blink;

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();    
    }
    void Start()
    {
        SetStatusAnimation();
    }

    public void SetStatusAnimation ()
    {
        switch (status)
        {
            case Status.Greet:
                anim.SetBool("isGreet", true);
                break;
            case Status.Blink:
                anim.SetBool("isBlink", true);
                break;
            case Status.Running:
                anim.SetBool("isRunning", true);
                break;
            default:
                break;
        }
    }

    private void ResetAnimations() {
        anim.SetBool("isGreet", false);
        anim.SetBool("isBlink", false);
        anim.SetBool("isRunning", false);
    }
}
