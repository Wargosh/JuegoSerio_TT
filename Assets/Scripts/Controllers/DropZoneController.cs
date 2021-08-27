using UnityEngine;
using UnityEngine.EventSystems;

public class DropZoneController : MonoBehaviour, IDropHandler/*, IPointerEnterHandler, IPointerExitHandler*/
{
    public Transform targetParent;
    public enum TypeSlot { origin, destiny }

    public void OnDrop(PointerEventData eventData) {
        if (eventData.pointerDrag != null) {
            DragController item = eventData.pointerDrag.GetComponent<DragController>();
            item.parentToReturnTo = this.transform;
        }
    }

    /*public void OnPointerExit(PointerEventData eventData) {
        //Debug.Log("OnPointerExit");
    }

    public void OnPointerEnter(PointerEventData eventData) {
        //Debug.Log("OnPointerEnter");
    }*/
}
