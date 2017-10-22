using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class TopOfTheStack : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 originalPosition;
    public Stack stack;

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        eventData.selectedObject = gameObject;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        originalPosition = this.transform.position;
        BringToFront();
    }

    private void BringToFront()
    {
        transform.parent.SetAsLastSibling();
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        eventData.selectedObject = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        var raycast = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycast);
        var onGrip = raycast.Where(r => r.gameObject.tag == "Grip").Any();
        if (onGrip)
        {
            stack.Draw();
        }
        else
        {
            this.transform.position = originalPosition;
        }
    }
}
