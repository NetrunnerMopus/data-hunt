using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using view.gui;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace controller
{
    public class Droppable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private IList<IInteractive> interactives = new List<IInteractive>();
        private Highlight highlight;
        private Vector3 originalPosition;
        private int originalIndex;
        private CanvasGroup canvasGroup;
        private GameObject placeholder;
        private LayoutElement layoutElement;

        void Awake()
        {
            highlight = gameObject.AddComponent<Highlight>();
            UpdateHighlights();
            canvasGroup = gameObject.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
            canvasGroup.blocksRaycasts = true;
            CreatePlaceholder();
            layoutElement = gameObject.AddComponent<LayoutElement>();
        }

        private void CreatePlaceholder()
        {
            placeholder = new GameObject("Placeholder");
            DrawPlaceholder();
            InsertPlaceholder();
            SizePlaceholder();
        }

        private void DrawPlaceholder()
        {
            var original = GetComponent<Image>();
            var image = placeholder.AddComponent<Image>();
            image.sprite = original.sprite;
            var faded = Color.white * new Color(1, 1, 1, 0.4f);
            image.color = faded;
            image.preserveAspect = original.preserveAspect;
            image.raycastTarget = false;
        }

        private void SizePlaceholder()
        {
            var original = GetComponent<RectTransform>();
            var rect = placeholder.GetComponent<RectTransform>();
            rect.anchorMin = original.anchorMin;
            rect.anchorMax = original.anchorMax;
            rect.offsetMin = original.offsetMin;
            rect.offsetMax = original.offsetMax;
            rect.rotation = original.rotation;
        }

        private void InsertPlaceholder()
        {
            placeholder.AttachTo(transform.parent.gameObject);
            placeholder.transform.SetSiblingIndex(transform.GetSiblingIndex() + 1);
            placeholder.gameObject.SetActive(false);
        }

        public void Represent(IInteractive interactive)
        {
            this.interactives.Add(interactive);
            interactive.Observe(UpdateHighlights);
        }

        internal void Unlink(IInteractive interactive)
        {
            this.interactives.Remove(interactive);
            interactive.UnobserveAll();
        }

        private void UpdateHighlights()
        {
            if (highlight == null)
            {
                return;
            }
            if (IsInteractive())
            {
                highlight.enabled = true;
            }
            else
            {
                highlight.enabled = false;
            }
        }

        private bool IsInteractive()
        {
            return interactives.Any(it => it.Active);
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            originalPosition = transform.position;
            BringToFront(eventData);
            placeholder.SetActive(true);
            layoutElement.ignoreLayout = true;
            canvasGroup.blocksRaycasts = false;
            foreach (var interactive in interactives)
            {
                if (interactive.Active)
                {
                    interactive.Activation.StartDragging();
                }
            }
        }

        private void BringToFront(PointerEventData eventData)
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

        async void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.blocksRaycasts = true;
            var raycast = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycast);
            var interactions = interactives
                .Where(it => it.Active)
                .Where(it => raycast.Where(r => r.gameObject == it.Activation.gameObject).Any())
                .Select(it => it.Interact());
            foreach (var interactive in interactives)
            {
                interactive.Activation.StopDragging();
            }
            await Task.WhenAll(interactions);
            PutBack();
        }

        private void PutBack()
        {
            transform.SetSiblingIndex(originalIndex);
            transform.position = originalPosition;
            layoutElement.ignoreLayout = false;
            placeholder.SetActive(false);
        }

        void OnDestroy()
        {
            foreach (var interactive in interactives)
            {
                interactive.UnobserveAll();
            }
        }
    }
}
