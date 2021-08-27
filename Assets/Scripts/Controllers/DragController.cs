using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragController : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [HideInInspector] public Transform parentToReturnTo = null;

    private Image image;
    private CanvasGroup canvasGroup;
    private Color currentColor;
    private MaskController maskController;

    public bool pressed;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();
        maskController = GetComponent<MaskController>();
        currentColor = image.color;
        parentToReturnTo = MinigameController_Mask.Instance.objParent;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0.5f);

        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.75f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.color = currentColor;

        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        this.transform.SetParent(parentToReturnTo);

        // si es arrastrado a la zona donde va la mascarilla
        if (parentToReturnTo.name == "zoneMask")
        {
            this.transform.localPosition = Vector3.zero;
            if (maskController.isNewMask)
                UIMiniGame.Instance.GoalCompleted();
            else
                MinigameController_Mask.Instance.AddFail();
        }
        else
            this.transform.SetParent(MinigameController_Mask.Instance.objParent);
    }
}
