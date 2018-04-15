using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using model.cards;

namespace controller
{
    public class CardInGrip : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public ICard Card;

        public PlayZone playZone;
        public RigZone rigZone;

        private Vector3 originalPosition;
        private int originalIndex;

        private CanvasGroup CanvasGroup { get { return GetComponent<CanvasGroup>(); } }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            eventData.selectedObject = gameObject;
            originalPosition = transform.position;
            BringToFront();
            CanvasGroup.blocksRaycasts = false;
            UpdateHighlights(eventData);
        }

        private void BringToFront()
        {
            originalIndex = transform.GetSiblingIndex();
            transform.SetAsLastSibling();
            transform.parent.SetAsLastSibling();
        }

        private void UpdateHighlights(PointerEventData eventData)
        {
            if (Card.Type.Playable)
            {
                playZone.UpdateHighlights(eventData);
            }
            if (Card.Type.Installable)
            {
                rigZone.UpdateHighlights(eventData);
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
            Play(raycast);
            Install(raycast);
            PutBack();
            UpdateHighlights(eventData);
        }

        private void Play(List<RaycastResult> raycast)
        {
            var onPlay = raycast.Where(r => r.gameObject == playZone.gameObject).Any();
            if (onPlay && Card.Type.Playable)
            {
                if (Netrunner.game.runner.Play(Card))
                {
                    Destroy(gameObject);
                }

            }
        }

        private void Install(List<RaycastResult> raycast)
        {
            var onRig = raycast.Where(r => r.gameObject == rigZone.gameObject).Any();
            if (onRig && Card.Type.Installable)
            {
                if (Netrunner.game.runner.Install(Card))
                {
                    Destroy(gameObject);
                }

            }
        }

        private void PutBack()
        {
            transform.SetSiblingIndex(originalIndex);
            transform.position = originalPosition;
        }
    }
}