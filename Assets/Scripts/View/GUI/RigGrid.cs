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
    public class RigGrid : MonoBehaviour, IInstallationObserver, IUninstallationObserver, IPaidAbilityObserver
    {
        private Game game;
        private DropZone playZone;
        private Dictionary<Card, GameObject> visuals = new Dictionary<Card, GameObject>();
        private CardPrinter printer;

        public void Construct(Game game, DropZone playZone)
        {
            this.game = game;
            this.playZone = playZone;
            game.flow.paidWindow.ObserveAbility(this);
            game.runner.zones.rig.ObserveInstallations(this);
            game.runner.zones.rig.ObserveUninstallations(this);
        }

        void Awake()
        {
            printer = gameObject.AddComponent<CardPrinter>();
        }

        void IInstallationObserver.NotifyInstalled(Card card)
        {
            var visual = printer.Print(card);
            visuals[card] = visual;
        }

        void IUninstallationObserver.NotifyUninstalled(Card card)
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