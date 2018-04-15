using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace controller
{
    public class GripZone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Image Image { get { return GetComponent<Image>(); } }

        private Color Color { set { Image.color = value; } }

        void Start()
        {
            ResetHighlights();
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (TopOfTheStackIsDragged(eventData))
            {
                Color = Color.green;
            }
        }

        private bool TopOfTheStackIsDragged(PointerEventData eventData)
        {
            if (eventData.selectedObject != null)
            {
                if (eventData.selectedObject.GetComponent<TopOfTheStack>() != null)
                {
                    return true;
                }
            }
            return false;
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            UpdateHighlights(eventData);
        }

        public void UpdateHighlights(PointerEventData eventData)
        {
            if (TopOfTheStackIsDragged(eventData))
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