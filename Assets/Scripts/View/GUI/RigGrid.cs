using UnityEngine;
using model.cards;

namespace view.gui
{
    public class RigGrid : MonoBehaviour, IRigView
    {
        void Start()
        {
            gameObject.AddComponent<CardPrinter>();
        }

        void IRigView.Place(ICard card)
        {
            GetComponent<CardPrinter>().Print(card);
        }
    }
}