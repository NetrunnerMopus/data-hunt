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
        originalPosition = this.transform.position;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        var raycast = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycast);
        var onGrip = raycast.Where(r => r.gameObject.tag == "Grip").Any();
        if (onGrip)
        {
            Debug.Log("Drawing from stack");
            stack.Draw();
        }
        else
        {
            this.transform.position = originalPosition;
        }
    }
}
