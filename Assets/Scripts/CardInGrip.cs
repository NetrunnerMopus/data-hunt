using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardInGrip : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 originalPosition;

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
        var onGrip = raycast.Where(r => r.gameObject.tag == "Play").Any();
        if (onGrip)
        {
            Debug.Log("Playing " + this.name);
        }
        else
        {
            this.transform.position = originalPosition;
        }
    }
}
