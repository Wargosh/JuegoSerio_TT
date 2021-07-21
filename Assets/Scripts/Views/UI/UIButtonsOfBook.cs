using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonsOfBook : MonoBehaviour
{
    public void btn_Action_Left() {
        BookController.Instance.back_page();
    }

    public void btn_Action_Right () {
        BookController.Instance.next_page();
    }

    public void btn_Action_Center () {
        BookController.Instance.open_close_book();
    }
}
