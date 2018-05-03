using UnityEngine;
using controller;
using model.cards;
using model;
using System.Collections.Generic;

namespace view.gui
{
    public class GripFan : MonoBehaviour, IGripAdditionObserver, IGripRemovalObserver
    {
        public Game Game { private get; set; }

        private Dictionary<ICard, GameObject> visuals = new Dictionary<ICard, GameObject>();

        void Awake()
        {
            gameObject.AddComponent<CardPrinter>();
        }

        void IGripAdditionObserver.NotifyCardAdded(ICard card)
        {
            var visual = GetComponent<CardPrinter>().Print(card);
            visuals[card] = visual;
            var type = card.Type;
            if (type.Playable)
            {
                var playable = visual.AddComponent<PlayableInGrip>();
                playable.Game = Game;
                playable.Card = card;
            }
            if (type.Installable)
            {
                var installable = visual.AddComponent<InstallableInGrip>();
                installable.Game = Game;
                installable.Card = card;
            }
        }

        void IGripRemovalObserver.NotifyCardRemoved(ICard card)
        {
            Destroy(visuals[card]);
            visuals.Remove(card);
        }
    }
}