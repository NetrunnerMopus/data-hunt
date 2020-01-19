using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using model;
using System.Threading.Tasks;

namespace controller
{
    public class DroppableChoice<T> : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private T value;
        public DropZone zone { get; private set; }
        private Game game;
        private Vector3 originalPosition;
        private CanvasGroup canvasGroup;
        private TaskCompletionSource<T> chosen = new TaskCompletionSource<T>();

        void Awake()
        {
            canvasGroup = gameObject.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
            canvasGroup.blocksRaycasts = true;
        }

        public DroppableChoice<T> Represent(T value, DropZone zone, Game game)
        {
            this.value = value;
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
                chosen.SetResult(value);
                Destroy(this);
            }
        }

        public Task<T> AwaitChoice()
        {
            return chosen.Task;
        }
    }
}