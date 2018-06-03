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
        private Game game;
        private DropZone playZone;
        private DropZone rigZone;
        private DropZone heapZone;
        private Dictionary<Card, GameObject> visuals = new Dictionary<Card, GameObject>();
        private CardPrinter printer;

        public void Contstruct(Game game, DropZone playZone, DropZone rigZone, DropZone heapZone)
        {
            this.game = game;
            this.playZone = playZone;
            this.rigZone = rigZone;
            this.heapZone = heapZone;
        }

        void Awake()
        {
            printer = gameObject.AddComponent<CardPrinter>();
        }

        void IGripAdditionObserver.NotifyCardAdded(Card card)
        {
            var visual = printer.Print(card);
            visuals[card] = visual;
            var type = card.Type;
            if (type.Playable)
            {
                visual
                    .AddComponent<DroppableAbility>()
                    .Represent(
                        game.runner.actionCard.Play(card),
                        game,
                        playZone
                    );
            }
            if (type.Installable)
            {
                visual
                     .AddComponent<DroppableAbility>()
                     .Represent(
                         game.runner.actionCard.Install(card),
                         game,
                         rigZone
                     );
            }
            visual
                .AddComponent<Discardable>()
                .Represent(
                    card,
                    game.runner.zones.grip,
                    game.runner.zones.heap,
                    heapZone
                );
        }

        void IGripRemovalObserver.NotifyCardRemoved(Card card)
        {
            Destroy(visuals[card]);
            visuals.Remove(card);
        }
    }
}