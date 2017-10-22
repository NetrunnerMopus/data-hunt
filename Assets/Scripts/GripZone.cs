using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GripZone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Color? original;
    private Image Image { get { return GetComponent<Image>(); } }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.selectedObject != null)
        {
            if (eventData.selectedObject.GetComponent<TopOfTheStack>() != null)
            {
                original = Image.color;
                Image.color = Color.green;
            }
        }
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (original != null)
        {
            Image.color = original.Value;
            original = null;
        }
    }
}
