using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using model.play;
using model;
using view.gui;
using view;

namespace controller
{
    public class Droppable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IUsabilityObserver
    {
        private Ability ability;
        private Game game;
        private DropZone zone;
        private bool usable = false;
        private AbilityHighlight highlight;

        private Vector3 originalPosition;
        private int originalIndex;

        private CanvasGroup CanvasGroup { get { return GetComponent<CanvasGroup>(); } }

        public void Represent(Ability ability, Game game, DropZone zone)
        {
            this.ability = ability;
            this.game = game;
            this.zone = zone;
            highlight = new AbilityHighlight(gameObject.AddComponent<Highlight>());
            ability.Observe(this, game);
            ability.Observe(highlight, game);
        }

        void IUsabilityObserver.NotifyUsable(bool usable)
        {
            this.usable = usable;
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
                ability.Trigger(game);
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
            ability.Unobserve(this);
            ability.Unobserve(highlight);
        }
    }
}