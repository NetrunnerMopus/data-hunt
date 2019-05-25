using UnityEngine;
using model.cards;
using model.zones.runner;
using model.zones;

namespace view.gui
{
    public class HeapPile : MonoBehaviour, IZoneAdditionObserver
    {
        void Start()
        {
            gameObject.AddComponent<CardPrinter>();
        }

        void IZoneAdditionObserver.NotifyCardAdded(Card card)
        {
            GetComponent<CardPrinter>().Print(card);
        }
    }
}