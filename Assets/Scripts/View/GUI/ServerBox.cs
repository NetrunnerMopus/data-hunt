using System.Collections.Generic;
using model.cards;
using model.zones;
using model.zones.corp;
using UnityEngine;
using UnityEngine.UI;

namespace view.gui
{
    public class ServerBox
    {
        public readonly GameObject gameObject;
        public readonly IServer server;
        public CardPrinter Printer { get; private set; }
        private IDictionary<Card, GameObject> visuals = new Dictionary<Card, GameObject>();

        public ServerBox(GameObject gameObject, IServer server, BoardParts parts)
        {
            this.gameObject = gameObject;
            this.server = server;
            var image = gameObject.AddComponent<Image>();
            image.sprite = Resources.Load<Sprite>("Images/UI/9slice-solid-white");
            image.type = Image.Type.Sliced;
            image.fillCenter = false;
            image.color = new Color(1f, 1f, 1f, 0f);
            Printer = parts.Print(gameObject);

        }

        public void ShowCards()
        {
            server.Zone.Added += RenderInstalledCard;
            server.Zone.Removed += DestroyUninstalledCard;
            foreach (var card in server.Zone.Cards)
            {
                RenderInstalledCard(server.Zone, card);
            }
        }

        private void RenderInstalledCard(Zone zone, Card card)
        {
            var printedCard = Printer.Print(card);
            visuals[card] = printedCard;
        }

        private void DestroyUninstalledCard(Zone zone, Card card)
        {
            var visual = visuals[card];
            visuals.Remove(card);
            Object.Destroy(visual);
        }
    }
}
