using UnityEngine;
using UnityEngine.EventSystems;

public class FaceupCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Beggining to drag " + gameObject.name);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging " + gameObject.name);
        transform.position = eventData.position;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Finished dragging " + gameObject.name);
    }
}
