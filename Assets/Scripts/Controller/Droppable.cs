using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using view.gui;

namespace controller
{
    public class Droppable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private IInteractive interactive;
        private DropZone zone;
        private bool active = false;
        private Highlight highlight;
        private Vector3 originalPosition;
        private int originalIndex;
        private CanvasGroup canvasGroup;

        void Awake()
        {
            canvasGroup = gameObject.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
            canvasGroup.blocksRaycasts = true;
        }

        public void Represent(IInteractive interactive, DropZone zone)
        {
            this.interactive = interactive;
            this.zone = zone;
            highlight = gameObject.AddComponent<Highlight>();
            interactive.Observe(Toggle);
        }

        private void Toggle(bool active)
        {
            this.active = active;
            if (active)
            {
                highlight.TurnOn();
            }
            else
            {
                highlight.TurnOff();
            }
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            originalPosition = transform.position;
            BringToFront(eventData);
            canvasGroup.blocksRaycasts = false;
            if (active)
            {
                zone.StartDragging();
            }
        }

        private void BringToFront(PointerEventData eventData)
        {
            eventData.selectedObject = gameObject;
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

        async void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.blocksRaycasts = true;
            if (active)
            {
                var raycast = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventData, raycast);
                var onDrop = raycast.Where(r => r.gameObject == zone.gameObject).Any();
                if (onDrop)
                {
                    await interactive.Interact();
                }
            }
            PutBack(eventData);
            zone.StopDragging();
        }

        private void PutBack(PointerEventData eventData)
        {
            if (eventData.selectedObject == null) // avoid multiple Droppables on the same GO resetting the index: https://github.com/dagguh/data-hunt/pull/184
            {
                return;
            }
            eventData.selectedObject = null;
            transform.SetSiblingIndex(originalIndex);
            transform.position = originalPosition;
        }

        void OnDestroy()
        {
            interactive.UnobserveAll();
        }
    }
}
