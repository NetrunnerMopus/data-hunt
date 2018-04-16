using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace controller
{
    public class DropZone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Image Image { get { return GetComponent<Image>(); } }
        private Color Color { set { Image.color = value; } }
        private bool droppableDragged;

        public void StartDragging()
        {
            droppableDragged = true;
            UpdateHighlights();
        }

        public void StopDragging()
        {
            droppableDragged = false;
            UpdateHighlights();
        }

        void Start()
        {
            ResetHighlights();
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (droppableDragged)
            {
                Color = Color.green;
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
            var potentialHighlight = Color.green;
            potentialHighlight.a = 0.5f;
            Color = potentialHighlight;
        }

        private void ResetHighlights()
        {
            var neutral = Color.white;
            neutral.a = 0.2f;
            Color = neutral;
        }
    }
}