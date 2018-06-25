using model.cards;
using model.zones.corp;
using UnityEngine;
using UnityEngine.UI;

namespace view.gui
{
    public class Server : MonoBehaviour, IServerContentObserver
    {
        public CardPrinter Printer { get; private set; }

        void Awake()
        {
            var image = gameObject.AddComponent<Image>();
            image.sprite = Resources.Load<Sprite>("Images/UI/9slice-solid-white");
            image.type = Image.Type.Sliced;
            image.fillCenter = false;
            Printer = gameObject.AddComponent<CardPrinter>();
        }

        void IServerContentObserver.NotifyCardInstalled(Card card)
        {
            Printer.PrintCorpFacedown(card.Name);
        }
    }
}