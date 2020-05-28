using UnityEngine;
using UnityEngine.EventSystems;

namespace controller
{
    public class CardUnzoom : MonoBehaviour, IPointerClickHandler
    {
        private GameObject zoom;
        public CardUnzoom Construct(GameObject zoom)
        {
            this.zoom = zoom;
            return this;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            zoom.SetActive(false);
        }
    }
}
