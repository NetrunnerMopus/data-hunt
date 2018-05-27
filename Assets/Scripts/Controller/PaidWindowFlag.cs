using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using model.timing.runner;
using view.gui;

namespace controller
{
    public class PaidWindowFlag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private PaidWindow window;
        private DropZone zone;

        private Vector3 originalPosition;
        private int originalIndex;

        private CanvasGroup canvasGroup;

        public void Represent(PaidWindow window, DropZone zone)
        {
            this.window = window;
            this.zone = zone;
            gameObject.AddComponent<Highlight>().TurnOn();
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            eventData.selectedObject = gameObject;
            originalPosition = transform.position;
            BringToFront();
            canvasGroup.blocksRaycasts = false;
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
            canvasGroup.blocksRaycasts = true;
            var raycast = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycast);
            var onDrop = raycast.Where(r => r.gameObject == zone.gameObject).Any();
            if (onDrop)
            {
                window.Pass();
            }
            PutBack();
            zone.StopDragging();
        }

        private void PutBack()
        {
            transform.SetSiblingIndex(originalIndex);
            transform.position = originalPosition;
        }
    }
}