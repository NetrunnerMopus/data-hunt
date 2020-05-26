using UnityEngine;
using view.gui;

namespace view
{
    public class BoardParts
    {
        public GameObject board;
        private CardZoom zoom;

        public BoardParts(GameObject board, CardZoom zoom)
        {
            this.board = board;
            this.zoom = zoom;
        }

        public CardPrinter Print(GameObject parent)
        {
            return new CardPrinter(parent, zoom);
        }
    }
}
