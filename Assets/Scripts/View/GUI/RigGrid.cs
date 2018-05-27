using UnityEngine;
using model.cards;
using model.zones.runner;
using System.Collections.Generic;
using model.timing.runner;
using model.play;
using controller;
using model;

namespace view.gui
{
    public class RigGrid : MonoBehaviour, IInstallationObserver, IUninstallationObserver, IPaidAbilityObserver
    {
        private Game game;
        private DropZone playZone;
        private Dictionary<ICard, GameObject> visuals = new Dictionary<ICard, GameObject>();
        private CardPrinter printer;

        internal void Construct(Game game, DropZone playZone)
        {
            this.game = game;
            this.playZone = playZone;
            game.runner.turn.paidWindow.ObserveAbility(this);
            game.runner.zones.rig.ObserveInstallations(this);
            game.runner.zones.rig.ObserveUninstallations(this);
        }

        void Awake()
        {
            printer = gameObject.AddComponent<CardPrinter>();
        }

        void IInstallationObserver.NotifyInstalled(ICard card)
        {
            var visual = printer.Print(card);
            visuals[card] = visual;
        }

        void IUninstallationObserver.NotifyUninstalled(ICard card)
        {
            Destroy(visuals[card]);
        }

        void IPaidAbilityObserver.NotifyPaidAbilityAvailable(Ability ability, ICard source)
        {
            var droppable = visuals[source].AddComponent<DroppableAbility>();
            droppable.Represent(ability, game, playZone);
        }
    }
}