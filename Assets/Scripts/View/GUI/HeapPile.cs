using UnityEngine;
using model.cards;

namespace view.gui
{
    public class HeapPile : MonoBehaviour, IHeapView
    {
        void Start()
        {
            gameObject.AddComponent<CardPrinter>();
        }

        void IHeapView.Add(ICard card)
        {
            GetComponent<CardPrinter>().Print(card);
        }
    }
}