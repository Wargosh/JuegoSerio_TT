using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using echo17.EndlessBook;

public class BookController : MonoBehaviour
{
    public float timeOpenCloseBook = .7f;
    public float timeChangePage = .6f;

    public EndlessBook book;

    public static BookController Instance { get; set; }
    private void Awake () {
        Instance = this;
    }

    private void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (book.CurrentState == EndlessBook.StateEnum.ClosedFront)
                book.SetState(EndlessBook.StateEnum.OpenMiddle);
            else
                book.SetState(EndlessBook.StateEnum.ClosedFront);
        }
        if (book.CurrentState == EndlessBook.StateEnum.OpenMiddle)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && !book.IsFirstPageGroup)
            {
                //book.SetPageNumber(book.CurrentLeftPageNumber - 2);
                book.TurnToPage(book.CurrentLeftPageNumber - 2, EndlessBook.PageTurnTimeTypeEnum.TimePerPage, timeChangePage);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && !book.IsLastPageGroup)
                book.TurnToPage(book.CurrentLeftPageNumber + 2, EndlessBook.PageTurnTimeTypeEnum.TimePerPage, timeChangePage);
        }
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
            book.SetState(EndlessBook.StateEnum.OpenFront, timeOpenCloseBook);
        else if (Input.GetKeyDown(KeyCode.KeypadMinus))
            book.SetState(EndlessBook.StateEnum.OpenBack, timeOpenCloseBook);
    }

    // Abrir o cerrar el libro
    public void open_close_book () {
        if (book.CurrentState == EndlessBook.StateEnum.ClosedFront)
            book.SetState(EndlessBook.StateEnum.OpenFront, timeOpenCloseBook);
        else
            book.SetState(EndlessBook.StateEnum.ClosedFront, timeOpenCloseBook);
    }

    // Cambiar a siguiente pagina
    public void next_page () {
        if (book.CurrentState == EndlessBook.StateEnum.OpenMiddle)
            if (!book.IsLastPageGroup)
                book.TurnToPage(book.CurrentLeftPageNumber + 2, EndlessBook.PageTurnTimeTypeEnum.TimePerPage, timeChangePage);
            else
                book.SetState(EndlessBook.StateEnum.OpenBack, timeOpenCloseBook);

        if (book.CurrentState == EndlessBook.StateEnum.OpenFront)
            book.SetState(EndlessBook.StateEnum.OpenMiddle, timeOpenCloseBook);
    }

    // Cambiar a pagina anterior
    public void back_page () {
        if (book.CurrentState == EndlessBook.StateEnum.OpenMiddle)
            if (!book.IsFirstPageGroup)
                book.TurnToPage(book.CurrentLeftPageNumber - 2, EndlessBook.PageTurnTimeTypeEnum.TimePerPage, timeChangePage);
            else
                book.SetState(EndlessBook.StateEnum.OpenFront);

        if (book.CurrentState == EndlessBook.StateEnum.OpenBack)
            book.SetState(EndlessBook.StateEnum.OpenMiddle, timeOpenCloseBook);
    }
}
