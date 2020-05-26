using UnityEngine;
using controller;
using model.cards;
using model;
using System.Collections.Generic;
using model.zones;

namespace view.gui
{
    public class GripFan : IZoneAdditionObserver, IZoneRemovalObserver
    {
        private GameObject gameObject;
        private Game game;
        private DropZone playZone;
        private DropZone rigZone;
        private DropZone heapZone;
        private CardZoom zoom;
        private Dictionary<Card, GameObject> visuals = new Dictionary<Card, GameObject>();
        private CardPrinter printer;
        public DropZone DropZone { get; private set; }

        public GripFan(GameObject gameObject, Game game, DropZone playZone, DropZone rigZone, DropZone heapZone, BoardParts parts)
        {
            this.gameObject = gameObject;
            this.game = game;
            this.playZone = playZone;
            this.rigZone = rigZone;
            this.heapZone = heapZone;
            this.printer = parts.Print(gameObject);
            this.DropZone = gameObject.AddComponent<DropZone>();
            game.runner.zones.grip.zone.ObserveAdditions(this);
            game.runner.zones.grip.zone.ObserveRemovals(this);
        }

        void IZoneAdditionObserver.NotifyCardAdded(Card card)
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

        void IZoneRemovalObserver.NotifyCardRemoved(Card card)
        {
            Object.Destroy(visuals[card]);
            visuals.Remove(card);
        }
    }
}