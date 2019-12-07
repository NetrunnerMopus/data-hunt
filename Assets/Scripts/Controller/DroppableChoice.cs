using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using model;
using System.Threading.Tasks;

namespace controller
{
    public class DroppableChoice : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private DropZone zone;
        private Game game;
        private Vector3 originalPosition;
        private CanvasGroup canvasGroup;
        private TaskCompletionSource<bool> chosen = new TaskCompletionSource<bool>();

        void Awake()
        {
            canvasGroup = gameObject.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
            canvasGroup.blocksRaycasts = true;
        }

        public DroppableChoice Represent(DropZone zone, Game game)
        {
            this.zone = zone;
            this.game = game;
            return this;
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            eventData.selectedObject = gameObject;
            originalPosition = transform.position;
            canvasGroup.blocksRaycasts = false;
            zone.StartDragging();
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
            var onZone = raycast.Any(ray => ray.gameObject == zone.gameObject);
            zone.StopDragging();
            if (onZone)
            {
                chosen.SetResult(true);
                Destroy(this);
            }
        }

        public Task AwaitChoice()
        {
            return chosen.Task;
        }
    }
}