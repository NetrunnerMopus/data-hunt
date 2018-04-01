using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using model;

namespace controller
{
    public class CardInGrip : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public ICard Card { private get; set; }

        public PlayZone playZone;

        private Vector3 originalPosition;
        private int originalIndex;

        private CanvasGroup CanvasGroup { get { return GetComponent<CanvasGroup>(); } }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            eventData.selectedObject = this.gameObject;
            originalPosition = transform.position;
            BringToFront();
            CanvasGroup.blocksRaycasts = false;
            playZone.UpdateHighlights(eventData);
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
            var onGrip = raycast.Where(r => r.gameObject == playZone.gameObject).Any();
            if (onGrip)
            {
                var game = Netrunner.game;
                if (Card.PlayCost.Pay(game))
                {
                    Card.PlayEffect.Resolve(game);
                    Object.Destroy(gameObject);
                }
                else
                {
                    PutBack();
                }
            }
            else
            {
                PutBack();
            }
            playZone.UpdateHighlights(eventData);
        }

        private void PutBack()
        {
            transform.SetSiblingIndex(originalIndex);
            transform.position = originalPosition;
        }
    }
}