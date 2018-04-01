using model;
using UnityEngine;
using controller;

namespace view
{
    public class GripFan : MonoBehaviour
    {
        public PlayZone playZone;

        void Start()
        {
            gameObject.AddComponent<CardPrinter>();
        }

        public void AddCard(ICard card)
        {
            var visual = GetComponent<CardPrinter>().Print(card);
            var cardInGrip = visual.AddComponent<CardInGrip>();
            cardInGrip.Card = card;
            cardInGrip.playZone = playZone;
        }
    }
}