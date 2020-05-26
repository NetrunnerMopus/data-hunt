using UnityEngine;
using model.cards;
using model.zones;
using controller;
using model;

namespace view.gui
{
    public class HeapPile : IZoneAdditionObserver
    {
        private GameObject gameObject;
        private CardPrinter printer;
        public DropZone DropZone { get; private set; }

        public HeapPile(GameObject gameObject, Game game, BoardParts parts)
        {
            this.gameObject = gameObject;
            this.printer = parts.Print(gameObject);
            this.DropZone = gameObject.AddComponent<DropZone>();
            game.runner.zones.heap.zone.ObserveAdditions(this);
        }

        void IZoneAdditionObserver.NotifyCardAdded(Card card)
        {
            printer.Print(card);
        }
    }
}