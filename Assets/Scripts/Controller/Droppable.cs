using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

namespace controller
{
    public abstract class Droppable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public DropZone zone;

        private Vector3 originalPosition;
        private int originalIndex;

        private CanvasGroup CanvasGroup { get { return GetComponent<CanvasGroup>(); } }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            eventData.selectedObject = gameObject;
            originalPosition = transform.position;
            BringToFront();
            CanvasGroup.blocksRaycasts = false;
            zone.StartDragging();
        }

        private void BringToFront()
        {
            originalIndex = transform.GetSiblingIndex();
            for (Transform t = transform; t.parent != null; t = t.parent)
            {
                t.SetAsLastSibling();
            }
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            eventData.selectedObject = null;
            CanvasGroup.blocksRaycasts = true;
            var raycast = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycast);
            var onDrop = raycast.Where(r => r.gameObject == zone.gameObject).Any();
            if (onDrop)
            {
                Drop();
            }
            PutBack();
            zone.StopDragging();
        }

        protected abstract void Drop();

        private void PutBack()
        {
            transform.SetSiblingIndex(originalIndex);
            transform.position = originalPosition;
        }
    }
}