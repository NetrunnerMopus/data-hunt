using UnityEngine;
using controller;
using model.cards;

namespace view.gui
{
    public class GripFan : MonoBehaviour, IGripView
    {
        public PlayZone playZone;
        public RigZone rigZone;

        void Start()
        {
            gameObject.AddComponent<CardPrinter>();
        }

        void IGripView.Add(ICard card)
        {
            var visual = GetComponent<CardPrinter>().Print(card);
            var cardInGrip = visual.AddComponent<CardInGrip>();
            cardInGrip.Card = card;
            cardInGrip.playZone = playZone;
            cardInGrip.rigZone = rigZone;
        }
    }
}