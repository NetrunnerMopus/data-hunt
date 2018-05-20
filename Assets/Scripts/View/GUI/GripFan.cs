using UnityEngine;
using controller;
using model.cards;
using model;
using System.Collections.Generic;
using model.zones.runner;

namespace view.gui
{
    public class GripFan : MonoBehaviour, IGripAdditionObserver, IGripRemovalObserver
    {
        public Game Game { private get; set; }

        private Dictionary<ICard, GameObject> visuals = new Dictionary<ICard, GameObject>();
        private DropZone playZone;
        private DropZone rigZone;
        private DropZone heapZone;

        void Awake()
        {
            gameObject.AddComponent<CardPrinter>();
            playZone = GameObject.Find("Play").AddComponent<DropZone>();
            rigZone = GameObject.Find("Rig").AddComponent<DropZone>();
            heapZone = GameObject.Find("Heap").AddComponent<DropZone>();
        }

        void IGripAdditionObserver.NotifyCardAdded(ICard card)
        {
            var visual = GetComponent<CardPrinter>().Print(card);
            visuals[card] = visual;
            var type = card.Type;
            if (type.Playable)
            {
                visual
                    .AddComponent<DroppableAbility>()
                    .Represent(
                        Game.runner.actionCard.Play(card),
                        Game,
                        playZone
                    );
            }
            if (type.Installable)
            {
                visual
                     .AddComponent<DroppableAbility>()
                     .Represent(
                         Game.runner.actionCard.Install(card),
                         Game,
                         rigZone
                     );
            }
            visual
                .AddComponent<Discardable>()
                .Represent(
                    card,
                    Game.runner.zones.grip,
                    Game.runner.zones.heap,
                    heapZone
                );
        }

        void IGripRemovalObserver.NotifyCardRemoved(ICard card)
        {
            Destroy(visuals[card]);
            visuals.Remove(card);
        }
    }
}