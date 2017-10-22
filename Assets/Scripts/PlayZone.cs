using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PlayZone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image Image { get { return GetComponent<Image>(); } }
    private Color Color { set { Image.color = value; } }

    void Start()
    {
        ResetHighlights();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (CardInGripDragged(eventData))
        {
            Color = Color.green;
        }
    }

    private bool CardInGripDragged(PointerEventData eventData)
    {
        if (eventData.selectedObject != null)
        {
            if (eventData.selectedObject.GetComponent<CardInGrip>() != null)
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
        if (CardInGripDragged(eventData))
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
