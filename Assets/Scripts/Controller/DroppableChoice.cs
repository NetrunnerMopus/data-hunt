using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using model.choices;

namespace controller
{
    public class DroppableChoice : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private IDictionary<DropZone, IChoice> choiceZones;
        private Vector3 originalPosition;
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

        public void Represent(IDictionary<DropZone, IChoice> choiceZones)
        {
            this.choiceZones = choiceZones;
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            eventData.selectedObject = gameObject;
            originalPosition = transform.position;
            canvasGroup.blocksRaycasts = false;
            choiceZones
                 .Keys
                 .ToList()
                 .ForEach(zone => zone.StartDragging());
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
            var choices = choiceZones
                .Keys
                .Where(zone => raycast.Any(ray => ray.gameObject == zone.gameObject))
                .Select(zone => choiceZones[zone])
                .Where(choice => choice.IsLegal());
            int choiceCount = choices.Count();
            if (choiceCount == 1)
            {
                choices.Single().Make();
            }
            if (choiceCount > 1)
            {
                throw new System.Exception("Player made " + choiceCount + " choices at once. Should all of them resolve or just one?");
            }
            choiceZones
                .Keys
                .ToList()
                .ForEach(zone => zone.StopDragging());
        }
    }
}