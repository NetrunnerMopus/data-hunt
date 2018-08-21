using UnityEngine;
using model.cards;
using model.zones.runner;
using System.Collections.Generic;
using model.play;
using controller;
using model;
using model.timing;

namespace view.gui
{
    public class RigGrid : IInstallationObserver, IUninstallationObserver, IPaidAbilityObserver
    {
        private readonly Game game;
        private readonly DropZone playZone;
        private readonly Dictionary<Card, GameObject> visuals = new Dictionary<Card, GameObject>();
        private CardPrinter printer;

        public RigGrid(GameObject gameObject, Game game, DropZone playZone)
        {
            printer = gameObject.AddComponent<CardPrinter>();
            this.game = game;
            this.playZone = playZone;
            game.flow.paidWindow.ObserveAbility(this);
            game.runner.zones.rig.ObserveInstallations(this);
            game.runner.zones.rig.ObserveUninstallations(this);
        }

        void IInstallationObserver.NotifyInstalled(Card card)
        {
            var visual = printer.Print(card);
            visuals[card] = visual;
        }

        void IUninstallationObserver.NotifyUninstalled(Card card)
        {
            Object.Destroy(visuals[card]);
        }

        void IPaidAbilityObserver.NotifyPaidAbilityAvailable(Ability ability, Card source)
        {
            var droppable = visuals[source].AddComponent<DroppableAbility>();
            droppable.Represent(ability, game, playZone);
        }
    }
}