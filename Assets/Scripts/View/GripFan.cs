using controller;
using model;
using UnityEngine;

namespace view
{
    public class GripFan : MonoBehaviour
    {
        public PlayZone playZone;

        public void AddCard(ICard card)
        {
            var visual = GetComponent<CardPrinter>().Print(card.Name, "Images/Cards/" + card.FaceupArt);
            var cardInGrip = visual.AddComponent<CardInGrip>();
            cardInGrip.Card = card;
            cardInGrip.playZone = playZone;
        }
    }
}