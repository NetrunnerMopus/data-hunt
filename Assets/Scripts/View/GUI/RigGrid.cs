using UnityEngine;
using model.cards;
using model.zones.runner;

namespace view.gui
{
    public class RigGrid : MonoBehaviour, IInstallationObserver
    {
        void Start()
        {
            gameObject.AddComponent<CardPrinter>();
        }

        void IInstallationObserver.NotifyInstalled(ICard card)
        {
            GetComponent<CardPrinter>().Print(card);
        }
    }
}