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
        public readonly GameObject box;
        public readonly GameObject contents;
        public readonly IServer server;
        public CardPrinter Printer { get; private set; }
        private IDictionary<Card, GameObject> contentVisuals = new Dictionary<Card, GameObject>();
        private IceColumn ice;

        public ServerBox(GameObject box, IServer server, BoardParts parts)
        {
            this.box = box;
            this.server = server;
            var image = box.AddComponent<Image>();
            image.sprite = Resources.Load<Sprite>("Images/UI/9slice-solid-white");
            image.type = Image.Type.Sliced;
            image.fillCenter = false;
            image.color = new Color(1f, 1f, 1f, 0f);
            this.contents = CreateContents(box);
            this.ice = CreateIce(box, parts);
            Printer = parts.Print(this.contents);
        }


        private GameObject CreateContents(GameObject box)
        {
            var gameObject = new GameObject("Contents of " + server.Zone.Name);
            gameObject.AttachTo(box);
            var rect = gameObject.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.0f, 0.50f);
            rect.anchorMax = new Vector2(1.0f, 1.00f);
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            return gameObject;
        }

        private IceColumn CreateIce(GameObject box, BoardParts parts)
        {
            var gameObject = new GameObject("ICE protecting " + server.Zone.Name);
            gameObject.AttachTo(box);
            var rect = gameObject.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.0f, 0.00f);
            rect.anchorMax = new Vector2(1.0f, 0.50f);
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            return new IceColumn(gameObject, server.IceStack, parts);
        }

        void IZoneAdditionObserver.NotifyCardAdded(Card card)
        {
            var printedCard = Printer.Print(card);
            contentVisuals[card] = printedCard;
        }

        void IZoneRemovalObserver.NotifyCardRemoved(Card card)
        {
            var visual = contentVisuals[card];
            contentVisuals.Remove(card);
            Object.Destroy(visual);
        }
    }
}
