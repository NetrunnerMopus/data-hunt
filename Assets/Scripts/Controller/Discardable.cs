using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using view.gui;
using model.zones.runner;
using model.cards;

namespace controller
{
    public class Discardable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IGripDiscardObserver
    {
        private Card card;
        private Grip grip;
        private Heap heap;
        private DropZone zone;
        private bool usable = false;
        private Highlight highlight;

        private Vector3 originalPosition;
        private int originalIndex;

        private CanvasGroup CanvasGroup { get { return GetComponent<CanvasGroup>(); } }

        public void Represent(Card card, Grip grip, Heap heap, DropZone zone)
        {
            this.card = card;
            this.grip = grip;
            this.heap = heap;
            this.zone = zone;
            grip.ObserveDiscarding(this);
            highlight = gameObject.AddComponent<Highlight>();
        }

        void IGripDiscardObserver.NotifyDiscarding(bool discarding)
        {
            usable = discarding;
            if (discarding)
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
            eventData.selectedObject = gameObject;
            originalPosition = transform.position;
            BringToFront();
            CanvasGroup.blocksRaycasts = false;
            if (usable)
            {
                zone.StartDragging();
            }
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
            if (onDrop && usable)
            {
                grip.Discard(card, heap);
            }
            PutBack();
            zone.StopDragging();
        }

        private void PutBack()
        {
            transform.SetSiblingIndex(originalIndex);
            transform.position = originalPosition;
        }

        void OnDestroy()
        {
            grip.UnobserveDiscarding(this);
        }
    }
}