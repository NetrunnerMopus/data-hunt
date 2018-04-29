using UnityEngine;
using controller;
using model.cards;

namespace view.gui
{
    public class GripFan : MonoBehaviour, IGripView
    {
        void Awake()
        {
            gameObject.AddComponent<CardPrinter>();
        }

        void IGripView.Add(ICard card)
        {
            var visual = GetComponent<CardPrinter>().Print(card);
            var type = card.Type;
            if (type.Playable)
            {
                var playable = visual.AddComponent<PlayableInGrip>();
                playable.Card = card;
            }
            if (type.Installable)
            {
                var installable = visual.AddComponent<InstallableInGrip>();
                installable.Card = card;
            }
        }
    }
}