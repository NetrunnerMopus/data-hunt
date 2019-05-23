using UnityEngine;
using model.cards;
using model.zones.runner;
using System.Collections.Generic;
using model.play;
using controller;
using model;
using model.timing;
using model.zones;

namespace view.gui
{
    public class RigGrid : MonoBehaviour, IZoneAdditionObserver, IZoneRemovalObserver, IPaidAbilityObserver
    {
        private Game game;
        private DropZone playZone;
        private Dictionary<Card, GameObject> visuals = new Dictionary<Card, GameObject>();
        private CardPrinter printer;

        public void Construct(Game game, DropZone playZone)
        {
            this.game = game;
            this.playZone = playZone;
            game.runner.paidWindow.ObserveAbility(this);
            game.runner.zones.rig.zone.ObserveAdditions(this);
            game.runner.zones.rig.zone.ObserveRemovals(this);
        }

        void Awake()
        {
            printer = gameObject.AddComponent<CardPrinter>();
        }

        void IZoneAdditionObserver.NotifyCardAdded(Card card)
        {
            var visual = printer.Print(card);
            visuals[card] = visual;
        }

        void IZoneRemovalObserver.NotifyCardRemoved(Card card)
        {
            Destroy(visuals[card]);
        }

        void IPaidAbilityObserver.NotifyPaidAbilityAvailable(Ability ability, Card source)
        {
            var droppable = visuals[source].AddComponent<DroppableAbility>();
            droppable.Represent(ability, game, playZone);
        }
    }
}