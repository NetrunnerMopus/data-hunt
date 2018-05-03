using UnityEngine;
using controller;
using model.cards;
using model;

namespace view.gui
{
    public class GripFan : MonoBehaviour, IGripObserver
    {
        void Awake()
        {
            gameObject.AddComponent<CardPrinter>();
        }

        void IGripObserver.NotifyCardAdded(ICard card)
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