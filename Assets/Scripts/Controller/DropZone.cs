using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using view.gui;

namespace controller
{
    public class DropZone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Image Image { get { return GetComponent<Image>(); } }
        private Color Color { set { Image.color = value; } }
        private bool droppableDragged;
        private Color originalColor = Color.magenta;
        private Highlight highlight;

        void Start()
        {
            originalColor = Image.color;
            highlight = gameObject.AddComponent<Highlight>();
            highlight.Rest = originalColor;
            StopDragging();
        }

        public void StartDragging()
        {
            originalColor = Image.color;
            highlight.Flash = Color.green;
            Image.raycastTarget = true;
            droppableDragged = true;
            UpdateHighlights();
        }

        public void StopDragging()
        {
            Image.raycastTarget = false;
            droppableDragged = false;
            UpdateHighlights();
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (droppableDragged)
            {
                highlight.Flash = Color.yellow;
            }
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            UpdateHighlights();
        }

        private void UpdateHighlights()
        {
            if (droppableDragged)
            {
                HighlightAvailability();
            }
            else
            {
                ResetHighlights();
            }
        }

        private void HighlightAvailability()
        {
            highlight.TurnOn();
        }

        private void ResetHighlights()
        {
            highlight.TurnOff();
            Color = originalColor;
        }
    }
}
