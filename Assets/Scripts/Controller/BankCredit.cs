using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

namespace controller
{
    public class BankCredit : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public CreditDropZone drop;

        private Vector3 originalPosition;
        private int originalIndex;

        private CanvasGroup CanvasGroup { get { return GetComponent<CanvasGroup>(); } }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            eventData.selectedObject = this.gameObject;
            originalPosition = transform.position;
            BringToFront();
            CanvasGroup.blocksRaycasts = false;
            drop.UpdateHighlights(eventData);
        }

        private void BringToFront()
        {
            originalIndex = transform.GetSiblingIndex();
            transform.SetAsLastSibling();
            transform.parent.SetAsLastSibling();
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            this.transform.position = eventData.position;
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            eventData.selectedObject = null;
            CanvasGroup.blocksRaycasts = true;
            var raycast = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycast);
            var onDrop = raycast.Where(r => r.gameObject == drop.gameObject).Any();
            if (onDrop)
            {
                Netrunner.game.runner.creditPool.Gain(1);
            }
            PutBack();
            drop.UpdateHighlights(eventData);
        }

        private void PutBack()
        {
            transform.SetSiblingIndex(originalIndex);
            transform.position = originalPosition;
        }
    }
}