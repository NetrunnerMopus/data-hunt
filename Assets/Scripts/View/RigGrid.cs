using UnityEngine;
using model.cards;

namespace view
{
    public class RigGrid : MonoBehaviour
    {
        void Start()
        {
            gameObject.AddComponent<CardPrinter>();
        }

        public void Place(ICard card)
        {
            GetComponent<CardPrinter>().Print(card);
        }
    }
}