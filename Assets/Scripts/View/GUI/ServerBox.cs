using model.cards;
using model.zones.corp;
using UnityEngine;
using UnityEngine.UI;

namespace view.gui
{
    public class ServerBox : IServerContentObserver
    {
        public readonly GameObject gameObject;
        public readonly IServer server;
        public CardPrinter Printer { get; private set; }

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

        void IServerContentObserver.NotifyCardInstalled(Card card)
        {
            var printedCard = Printer.PrintCorpFacedown(card.Name);
            var image = printedCard.GetComponent<Image>();
            var inServer = printedCard.AddComponent<CardInServer>();
            inServer.Represent(card, image);
        }
    }
}