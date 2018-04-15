using UnityEngine;
using model.cards;

namespace view
{
    public class HeapPile : MonoBehaviour
    {
        void Start()
        {
            gameObject.AddComponent<CardPrinter>();
        }

        public void Add(ICard card)
        {
            GetComponent<CardPrinter>().Print(card);
        }
    }
}