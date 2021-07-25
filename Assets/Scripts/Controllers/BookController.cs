using System.Linq;
using UnityEngine;
using echo17.EndlessBook;

public enum BookActionTypeEnum
{
    ChangeState,
    TurnPage
}

/// <summary>
/// Delegate that handles the action taken by a page view
/// </summary>
/// <param name="actionType">The type of action to perform</param>
/// <param name="actionValue">The value of the action (state or page number)</param>
public delegate void BookActionDelegate(BookActionTypeEnum actionType, int actionValue);

public class BookController : MonoBehaviour
{
    public float timeOpenCloseBook = .7f;
    public float timeChangePage = .6f;

    public EndlessBook book;

    protected bool audioOn = false;

    protected bool flipping = false;

    public EndlessBook.PageTurnTimeTypeEnum groupPageTurnType;

    public float singlePageTurnTime;

    public float groupPageTurnTime;

    public int tableOfContentsPageNumber;

    public AudioSource bookOpenSound;

    public AudioSource bookCloseSound;

    public AudioSource pageTurnSound;

    public AudioSource pagesFlippingSound;

    public float pagesFlippingSoundDelay;

    public TouchPad touchPad;

    public PageViewController[] pageViews;
    public static BookController Instance { get; set; }
    private void Awake () {
        Instance = this;
    }

    void Start()
    {
        // turn off all the mini-scenes since no pages are visible
        TurnOffAllPageViews();

        // set up touch pad handlers
        touchPad.touchDownDetected = TouchPadTouchDownDetected;
        touchPad.touchUpDetected = TouchPadTouchUpDetected;
        touchPad.tableOfContentsDetected = TableOfContentsDetected;

        // set the book closed
        OnBookStateChanged(EndlessBook.StateEnum.ClosedFront, EndlessBook.StateEnum.ClosedFront, -1);

        // turn on the audio now that the book state is set the first time,
        // otherwise we'd hear a noise and no change would occur
        audioOn = true;
    }

    // Abrir o cerrar el libro
    public void open_close_book () {
        if (book.CurrentState == EndlessBook.StateEnum.ClosedFront)
            OpenFront();
        else
            ClosedFront();
    }

    // Cambiar a siguiente pagina
    public void next_page () {
        if (book.CurrentState == EndlessBook.StateEnum.OpenMiddle)
            if (book.CurrentRightPageNumber == book.LastPageNumber)
                OpenBack();
            else
                book.TurnForward(singlePageTurnTime, onCompleted: OnBookStateChanged, onPageTurnStart: OnPageTurnStart, onPageTurnEnd: OnPageTurnEnd);

        if (book.CurrentState == EndlessBook.StateEnum.OpenBack)
            ClosedBack();

        if (book.CurrentState == EndlessBook.StateEnum.OpenFront)
            OpenMiddle();

        if (book.CurrentState == EndlessBook.StateEnum.ClosedFront)
            OpenFront();
    }

    // Cambiar a pagina anterior
    public void back_page () {
        if (book.CurrentState == EndlessBook.StateEnum.OpenMiddle)
            if (book.CurrentLeftPageNumber == 1)
                OpenFront();
            else
                book.TurnBackward(singlePageTurnTime, onCompleted: OnBookStateChanged, onPageTurnStart: OnPageTurnStart, onPageTurnEnd: OnPageTurnEnd);

        if (book.CurrentState == EndlessBook.StateEnum.OpenBack)
            OpenMiddle();

        if (book.CurrentState == EndlessBook.StateEnum.OpenFront)
            ClosedFront();

        if (book.CurrentState == EndlessBook.StateEnum.ClosedBack)
            OpenBack();
    }
    
    private void PlayAnim_ZoomIn() {
        UIButtonsOfBook.Instance.anim.SetBool("openBook", true);
    }

    private void PlayAnim_ZoomOut()
    {
        UIButtonsOfBook.Instance.anim.SetBool("openBook", false);
    }

    private void PlayAnim_BookSelected(bool isSelected)
    {
        UIButtonsOfBook.Instance.anim.SetBool("bookSelected", isSelected);
    }




    /// <summary>
    /// Called when the book's state changes
    /// </summary>
    /// <param name="fromState">Previous state</param>
    /// <param name="toState">Current state</param>
    /// <param name="pageNumber">Current page number</param>
    protected virtual void OnBookStateChanged(EndlessBook.StateEnum fromState, EndlessBook.StateEnum toState, int pageNumber)
    {
        switch (toState)
        {
            case EndlessBook.StateEnum.ClosedFront:
            case EndlessBook.StateEnum.ClosedBack:
                // Reproducir sonido al cerrar el libro
                if (audioOn)
                    bookCloseSound.Play();
                // deshabilita la renderizacion de las páginas mientras el libro esta cerrado
                TurnOffAllPageViews();
                break;
            case EndlessBook.StateEnum.OpenMiddle:
                // Reproducir sonido al abrir el libro al medio
                if (fromState != EndlessBook.StateEnum.OpenMiddle)
                    bookOpenSound.Play();
                else
                {
                    // stop the flipping sound
                    flipping = false;
                    pagesFlippingSound.Stop();
                }
                // Mientras el libro esta en "medio" puede deshabilitarse la primera y útima pagina
                TogglePageView(0, false);
                TogglePageView(999, false);
                break;
            case EndlessBook.StateEnum.OpenFront:
            case EndlessBook.StateEnum.OpenBack:
                // Reproducir sonido al abrir el libro
                bookOpenSound.Play();
                break;
        }
        // Habilita el touchpad
        ToggleTouchPad(true);
    }

    /// <summary>
    /// Habilita o deshabilita el tochpad
    /// </summary>
    /// <param name="on">recibe un booleano para determinar la acción</param>
    protected virtual void ToggleTouchPad(bool on)
    {
        // Solo se habilita la anterior página si el libro no esta cerrado por delante
        touchPad.Toggle(TouchPad.PageEnum.Left, on && book.CurrentState != EndlessBook.StateEnum.ClosedFront);
        // Solo se habilita la siguiente página si el libro no esta cerrado por detrás
        touchPad.Toggle(TouchPad.PageEnum.Right, on && book.CurrentState != EndlessBook.StateEnum.ClosedBack);
        // Solo permitir usar la tabla de contenidos si consta de más de una página
        touchPad.ToggleTableOfContents(on && book.CurrentLeftPageNumber > 1);
    }

    /// <summary>
    /// Deactivates all the page mini-scenes
    /// </summary>
    protected virtual void TurnOffAllPageViews()
    {
        for (var i = 0; i < pageViews.Length; i++)
        {
            if (pageViews[i] != null)
                pageViews[i].Deactivate();
        }
    }

    /// <summary>
    /// Turns a page mini-scene on or off
    /// </summary>
    /// <param name="pageNumber">The page number</param>
    /// <param name="on">Whether the mini-scene is on or off</param>
    protected virtual void TogglePageView(int pageNumber, bool on)
    {
        var pageView = GetPageView(pageNumber);

        if (pageView != null)
        {
            if (pageView != null)
            {
                if (on)
                    pageView.Activate();
                else
                    pageView.Deactivate();
            }
        }
    }

    /// <summary>
    /// Handler for when a page starts to turn.
    /// We play a sound, turn of the touchpad, and toggle
    /// page view mini-scenes.
    /// </summary>
    /// <param name="page">The page the starting turning</param>
    /// <param name="pageNumberFront">The page number of the front of the page</param>
    /// <param name="pageNumberBack">The page number of the back of hte page</param>
    /// <param name="pageNumberFirstVisible">The page number of the first visible page in the book</param>
    /// <param name="pageNumberLastVisible">The page number of the last visible page in the book</param>
    /// <param name="turnDirection">The direction the page is turning</param>
    protected virtual void OnPageTurnStart(Page page, int pageNumberFront, int pageNumberBack, int pageNumberFirstVisible, int pageNumberLastVisible, Page.TurnDirectionEnum turnDirection)
    {
        // play page turn sound if not flipping through multiple pages
        if (!flipping)
            pageTurnSound.Play();

        // turn off the touch pad
        ToggleTouchPad(false);

        // Controla la páginas actuales en la que se encuentra (para ahorrar recursos)
        TogglePageView(pageNumberFront, true);
        TogglePageView(pageNumberBack, true);

        switch (turnDirection)
        {
            case Page.TurnDirectionEnum.TurnForward:
                // turn on the last visible page view if necessary
                TogglePageView(pageNumberLastVisible, true);
                break;
            case Page.TurnDirectionEnum.TurnBackward:
                // turn on the first visible page view if necessary
                TogglePageView(pageNumberFirstVisible, true);
                break;
        }
    }

    /// <summary>
    /// Handler for when a page stops turning.
    /// We toggle the page views for the mini-scenes off for the relevent pages
    /// </summary>
    /// <param name="page">The page the starting turning</param>
    /// <param name="pageNumberFront">The page number of the front of the page</param>
    /// <param name="pageNumberBack">The page number of the back of hte page</param>
    /// <param name="pageNumberFirstVisible">The page number of the first visible page in the book</param>
    /// <param name="pageNumberLastVisible">The page number of the last visible page in the book</param>
    /// <param name="turnDirection">The direction the page is turning</param>
    protected virtual void OnPageTurnEnd(Page page, int pageNumberFront, int pageNumberBack, int pageNumberFirstVisible, int pageNumberLastVisible, Page.TurnDirectionEnum turnDirection)
    {
        switch (turnDirection)
        {
            case Page.TurnDirectionEnum.TurnForward:
                // turn off the two pages that are now hidden by this page
                TogglePageView(pageNumberFirstVisible - 1, false);
                TogglePageView(pageNumberFirstVisible - 2, false);
                break;
            case Page.TurnDirectionEnum.TurnBackward:
                // turn off the two pages that are now hidden by this page
                TogglePageView(pageNumberLastVisible + 1, false);
                TogglePageView(pageNumberLastVisible + 2, false);
                break;
        }
    }

    /// <summary>
    /// Turns to the table of contents
    /// </summary>
    protected virtual void TableOfContentsDetected()
    {
        TurnToPage(tableOfContentsPageNumber);
    }

    /// <summary>
    /// Handles whether a mouse down was detected on the touchpad
    /// </summary>
    /// <param name="page">The page that was hit</param>
    /// <param name="hitPointNormalized">The normalized hit point on the page</param>
    protected virtual void TouchPadTouchDownDetected(TouchPad.PageEnum page, Vector2 hitPointNormalized)
    {
        if (book.CurrentState == EndlessBook.StateEnum.OpenMiddle)
        {
            PageViewController pageView;

            switch (page)
            {
                case TouchPad.PageEnum.Left:
                    // get the left page view if available
                    pageView = GetPageView(book.CurrentLeftPageNumber);
                    // call touchdown on the page view
                    if (pageView != null)
                        pageView.TouchDown();
                    break;
                case TouchPad.PageEnum.Right:
                    // get the right page view if available
                    pageView = GetPageView(book.CurrentRightPageNumber);
                    // call the touchdown on the page view
                    if (pageView != null)
                        pageView.TouchDown();
                    break;
            }
        }
    }

    /// <summary>
    /// Handles the touch up event from the touchpad
    /// </summary>
    /// <param name="page">The page that was hit</param>
    /// <param name="hitPointNormalized">The normalized hit point on the page</param>
    /// <param name="dragging">Whether we were dragging</param>
    protected virtual void TouchPadTouchUpDetected(TouchPad.PageEnum page, Vector2 hitPointNormalized, bool dragging)
    {
        switch (book.CurrentState)
        {
            case EndlessBook.StateEnum.ClosedFront:
                if (TouchPad.PageEnum.Right == page)
                    OpenFront();
                break;
            case EndlessBook.StateEnum.OpenFront:
                if (TouchPad.PageEnum.Left == page)
                    ClosedFront();

                if (TouchPad.PageEnum.Left == page)
                    OpenMiddle();
                break;
            case EndlessBook.StateEnum.OpenMiddle:
                PageViewController pageView;

                if (dragging)
                {
                    // get the left page view if available.
                    // in this demo we only have one group of pages that handle the drag: the map.
                    // instead of having logic for dragging on both pages, we'll just handle it on the left
                    pageView = GetPageView(book.CurrentLeftPageNumber);
                    // call the drag method on the page view
                    if (pageView != null)
                        pageView.Drag(Vector2.zero, true);
                    return;
                }

                switch (page)
                {
                    case TouchPad.PageEnum.Left:
                        // get the left page view if available
                        pageView = GetPageView(book.CurrentLeftPageNumber);

                        if (pageView != null)
                        {
                            // cast a ray into the page and exit if we hit something (don't turn the page)
                            if (pageView.RayCast(hitPointNormalized, BookAction))
                                return;
                        }
                        break;
                    case TouchPad.PageEnum.Right:
                        // get the right page view if available
                        pageView = GetPageView(book.CurrentRightPageNumber);

                        if (pageView != null)
                        {
                            // cast a ray into the page and exit if we hit something (don't turn the page)
                            if (pageView.RayCast(hitPointNormalized, BookAction))
                                return;
                        }
                        break;
                }
                break;
            case EndlessBook.StateEnum.OpenBack:
                switch (page)
                {
                    case TouchPad.PageEnum.Left:
                        OpenMiddle();
                        break;
                    case TouchPad.PageEnum.Right:
                        ClosedBack();
                        break;
                }
                break;
            case EndlessBook.StateEnum.ClosedBack:
                // transición del libro cerrado por detras a abrirse en la última página
                if (TouchPad.PageEnum.Left == page)
                    OpenBack();
                break;
        }

        switch (page)
        {
            case TouchPad.PageEnum.Left:
                if (book.CurrentLeftPageNumber == 1)
                    OpenFront();
                else
                    book.TurnBackward(singlePageTurnTime, onCompleted: OnBookStateChanged, onPageTurnStart: OnPageTurnStart, onPageTurnEnd: OnPageTurnEnd);
                break;

            case TouchPad.PageEnum.Right:
                if (book.CurrentRightPageNumber == book.LastPageNumber)
                    OpenBack();
                else
                    book.TurnForward(singlePageTurnTime, onCompleted: OnBookStateChanged, onPageTurnStart: OnPageTurnStart, onPageTurnEnd: OnPageTurnEnd);
                break;
        }
    }

    /// <summary>
    /// Handler for a raycast hit on a page view
    /// </summary>
    /// <param name="actionType">The type of action to perform</param>
    /// <param name="actionValue">The value of the action (state or page number)</param>
    protected virtual void BookAction(BookActionTypeEnum actionType, int actionValue)
    {
        switch (actionType)
        {
            case BookActionTypeEnum.ChangeState:
                SetState((EndlessBook.StateEnum)System.Convert.ToInt16(actionValue));
                break;
            case BookActionTypeEnum.TurnPage:
                if (actionValue == 999)
                    OpenBack();
                else
                    TurnToPage(System.Convert.ToInt16(actionValue));
                break;
        }
    }

    /// <summary>
    /// Obtiene información de página solicitada
    /// </summary>
    /// <param name="pageNumber">Recibe el numero de la página</param>
    /// <returns></returns>
    protected virtual PageViewController GetPageView(int pageNumber)
    {
        return pageViews.Where(x => x.name == string.Format("PageView_{0}", 
            (pageNumber == 0 ? "Front" : (pageNumber == 999 ? "Back" : pageNumber.ToString("00"))))).FirstOrDefault();
    }

    protected virtual void ClosedFront()
    {
        PlayAnim_ZoomOut();
        SetState(EndlessBook.StateEnum.ClosedFront);
    }

    protected virtual void OpenFront()
    {
        PlayAnim_ZoomIn();
        TogglePageView(0, true);

        SetState(EndlessBook.StateEnum.OpenFront);
    }

    protected virtual void OpenMiddle()
    {
        // toggle the left and right page views
        TogglePageView(book.CurrentLeftPageNumber, true);
        TogglePageView(book.CurrentRightPageNumber, true);

        SetState(EndlessBook.StateEnum.OpenMiddle);
    }

    protected virtual void OpenBack()
    {
        PlayAnim_ZoomIn();
        TogglePageView(999, true);

        SetState(EndlessBook.StateEnum.OpenBack);
    }

    protected virtual void ClosedBack()
    {
        PlayAnim_ZoomOut();
        SetState(EndlessBook.StateEnum.ClosedBack);
    }

    protected virtual void SetState(EndlessBook.StateEnum state)
    {
        // Deshabilita el touch
        ToggleTouchPad(false);
        // Establece el nuevo estado
        book.SetState(state, timeOpenCloseBook, OnBookStateChanged);
    }

    protected virtual void TurnToPage(int pageNumber)
    {
        var newLeftPageNumber = pageNumber % 2 == 0 ? pageNumber - 1 : pageNumber;
        // Reproduce el sonido de cambio de multiples páginas si es más de una página
        if (Mathf.Abs(newLeftPageNumber - book.CurrentLeftPageNumber) > 2)
        {
            flipping = true;
            pagesFlippingSound.PlayDelayed(pagesFlippingSoundDelay);
        }
        // turn to page
        book.TurnToPage(pageNumber, groupPageTurnType, groupPageTurnTime,
                        openTime: timeOpenCloseBook,
                        onCompleted: OnBookStateChanged,
                        onPageTurnStart: OnPageTurnStart,
                        onPageTurnEnd: OnPageTurnEnd);
    }
}
