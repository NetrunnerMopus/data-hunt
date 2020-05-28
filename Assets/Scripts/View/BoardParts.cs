using model.player;
using UnityEngine;
using view.gui;

namespace view
{
    public class BoardParts
    {
        public GameObject board;
        private IPerception perception;
        private CardZoom zoom;

        public BoardParts(GameObject board, IPerception perception, CardZoom zoom)
        {
            this.board = board;
            this.perception = perception;
            this.zoom = zoom;
        }

        public CardPrinter Print(GameObject parent)
        {
            return new CardPrinter(parent, perception, zoom);
        }
    }
}
