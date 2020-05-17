using System.Collections.Generic;
using model.cards;
using model.zones;
using model.zones.corp;
using UnityEngine;
using UnityEngine.UI;

namespace view.gui
{
    public class ServerBox : IZoneAdditionObserver, IZoneRemovalObserver
    {
        public readonly GameObject gameObject;
        public readonly IServer server;
        public CardPrinter Printer { get; private set; }
        private IDictionary<Card, GameObject> visuals = new Dictionary<Card, GameObject>();

        public ServerBox(GameObject gameObject, IServer server)
        {
            this.gameObject = gameObject;
            this.server = server;
            var image = gameObject.AddComponent<Image>();
            image.sprite = Resources.Load<Sprite>("Images/UI/9slice-solid-white");
            image.type = Image.Type.Sliced;
            image.fillCenter = false;
            Printer = gameObject.AddComponent<CardPrinter>();
        }

        void IZoneAdditionObserver.NotifyCardAdded(Card card)
        {
            var printedCard = Printer.PrintFlippable(card);
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