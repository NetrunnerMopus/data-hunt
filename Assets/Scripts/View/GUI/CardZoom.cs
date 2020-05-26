using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using controller;
using model;
using model.cards;
using model.choices;
using UnityEngine;
using UnityEngine.UI;

namespace view.gui
{
    public class CardZoom
    {
        private GameObject blanket;
        private RawCardPrinter rawCardPrinter;

        public CardZoom(GameObject board)
        {
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
            blanket.SetActive(true);
            blanket.transform.SetAsLastSibling();
            rawCardPrinter.Print("Zoomed in " + card.Name, Resources.Load<Sprite>("Images/Cards/" + card.FaceupArt));
        }
    }
}
