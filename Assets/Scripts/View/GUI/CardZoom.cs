using controller;
using model.cards;
using model.player;
using UnityEngine;
using UnityEngine.UI;

namespace view.gui
{
    public class CardZoom
    {
        private GameObject blanket;
        private RawCardPrinter rawCardPrinter;
        private IPerception perception;
        private GameObject zoomed;

        public CardZoom(GameObject board, IPerception perception)
        {
            this.perception = perception;
            blanket = CreateBlanket(board);
            rawCardPrinter = new RawCardPrinter(blanket);
        }

        private GameObject CreateBlanket(GameObject board)
        {
            var blanket = new GameObject("Card zoom")
            {
                layer = 5
            };
            blanket.SetActive(false);
            var image = blanket.AddComponent<Image>();
            image.color = new Color(0, 0, 0, 0.5f);
            blanket.transform.SetParent(board.transform);
            var rectangle = image.rectTransform;
            rectangle.anchorMin = new Vector2(0.00f, 0.00f);
            rectangle.anchorMax = new Vector2(0.40f, 1.00f);
            rectangle.offsetMin = Vector2.zero;
            rectangle.offsetMax = Vector2.zero;
            return blanket;
        }

        internal void Show(Card card)
        {
            if (zoomed != null) {
                Object.Destroy(zoomed);
            }
            blanket.SetActive(true);
            blanket.transform.SetAsLastSibling();
            zoomed = rawCardPrinter.Print("Zoomed in " + card.Name, FaceSprites.ChooseFace(card, perception));
            zoomed.AddComponent<CardUnzoom>().Construct(blanket);
        }
    }
}
