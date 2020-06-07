using UnityEngine;
using model.cards;
using System.Collections.Generic;
using model.play;
using controller;
using model;
using model.timing;
using model.zones;

namespace view.gui
{
    public class RigGrid : IZoneAdditionObserver, IZoneRemovalObserver, IPaidAbilityObserver
    {
        private Game game;

        public DropZone playZone;
        public DropZone DropZone { get; private set; }
        private Dictionary<Card, GameObject> visuals = new Dictionary<Card, GameObject>();
        private CardPrinter printer;

        public RigGrid(GameObject gameObject, Game game, DropZone playZone, BoardParts parts)
        {
            this.game = game;
            this.playZone = playZone;
            this.printer = parts.Print(gameObject);
            game.runner.paidWindow.ObserveAbility(this);
            game.runner.zones.rig.zone.ObserveAdditions(this);
            game.runner.zones.rig.zone.ObserveRemovals(this);
            this.DropZone = gameObject.AddComponent<DropZone>();
        }
        void IZoneAdditionObserver.NotifyCardAdded(Card card)
        {
            var visual = printer.Print(card);
            visuals[card] = visual;
        }

        void IZoneRemovalObserver.NotifyCardRemoved(Card card)
        {
            Object.Destroy(visuals[card]);
        }

        void IPaidAbilityObserver.NotifyPaidAbilityAvailable(Ability ability, Card source)
        {
            var droppable = visuals[source].AddComponent<Droppable>();
            droppable.Represent(new InteractiveAbility(ability, game), playZone);
        }
    }
}
