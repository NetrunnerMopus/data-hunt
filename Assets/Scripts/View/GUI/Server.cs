using model.cards;
using model.zones.corp;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace view.gui
{
    public class Server : MonoBehaviour, IServerContentObserver
    {
        internal CardPrinter Printer { get; private set; }

        void Awake()
        {
            var image = gameObject.AddComponent<Image>();
            image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");
            image.type = Image.Type.Sliced;
            image.fillCenter = false;
            Printer = gameObject.AddComponent<CardPrinter>();
        }

        void IServerContentObserver.NotifyCardInstalled(ICard card)
        {
            Printer.PrintCorpFacedown(card.Name);
        }
    }
}