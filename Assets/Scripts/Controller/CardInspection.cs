using model.cards;
using UnityEngine;
using UnityEngine.EventSystems;
using view.gui;

namespace controller
{
    public class CardInspection : MonoBehaviour, IPointerClickHandler
    {
        private CardZoom zoom;
        private Card card;

        public CardInspection Represent(CardZoom zoom, Card card)
        {
            this.zoom = zoom;
            this.card = card;
            return this;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            zoom.Show(card);
        }
    }
}
