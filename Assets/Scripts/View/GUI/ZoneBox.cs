using System.Collections.Generic;
using model;
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

        public ZoneBox(GameObject gameObject)
        {
            this.gameObject = gameObject;
            Printer = gameObject.AddComponent<CardPrinter>();
        }

        public void Represent(Zone zone)
        {
            zone.ObserveAdditions(this);
            zone.ObserveRemovals(this);
        }

        void IZoneAdditionObserver.NotifyCardAdded(Card card)
        {
            var printedCard = Print(card);
            visuals[card] = printedCard;
        }

        private GameObject Print(Card card)
        {
            if (card.Faceup)
            {
                return Printer.Print(card);
            }
            else if (card.Faction.Side == Side.RUNNER)
            {
                return Printer.PrintRunnerFacedown("Facedown");
            }
            else
            {
                return Printer.PrintCorpFacedown("Facedown");
            }
        }

        void IZoneRemovalObserver.NotifyCardRemoved(Card card)
        {
            var visual = visuals[card];
            visuals.Remove(card);
            Object.Destroy(visual);
        }
    }
}