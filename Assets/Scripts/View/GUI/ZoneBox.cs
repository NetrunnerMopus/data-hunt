using System.Collections.Generic;
using model.cards;
using model.zones;
using UnityEngine;

namespace view.gui
{
    public class ZoneBox : IZoneAdditionObserver, IZoneRemovalObserver
    {
        private readonly GameObject gameObject;
        private CardPrinter Printer;
        private IDictionary<Card, GameObject> visuals = new Dictionary<Card, GameObject>();

        public ZoneBox(GameObject gameObject, BoardParts parts)
        {
            this.gameObject = gameObject;
            Printer = parts.Print(gameObject);
        }

        public void Represent(Zone zone)
        {
            zone.ObserveAdditions(this);
            zone.ObserveRemovals(this);
        }

        void IZoneAdditionObserver.NotifyCardAdded(Card card)
        {
            var printedCard = Printer.Print(card);
            visuals[card] = printedCard;
        }

        void IZoneRemovalObserver.NotifyCardRemoved(Card card)
        {
            var visual = visuals[card];
            visuals.Remove(card);
            Object.Destroy(visual);
        }
    }
}
