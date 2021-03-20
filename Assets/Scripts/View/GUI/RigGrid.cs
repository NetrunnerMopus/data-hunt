using System.Collections.Generic;
using controller;
using model;
using model.cards;
using model.play;
using model.timing;
using model.zones;
using UnityEngine;

namespace view.gui
{
    public class RigGrid : IPaidAbilityObserver
    {
        private DropZone paidWindowTrigger;
        public DropZone DropZone { get; private set; }
        private Dictionary<Card, GameObject> visuals = new Dictionary<Card, GameObject>();
        private CardPrinter printer;

        public RigGrid(GameObject gameObject, Runner runner, DropZone paidWindowTrigger, BoardParts parts)
        {
            this.paidWindowTrigger = paidWindowTrigger;
            this.printer = parts.Print(gameObject);
            runner.paidWindow.ObserveAbility(this);
            runner.zones.rig.zone.Added += RenderInstalledCard;
            runner.zones.rig.zone.Removed += DestroyUninstalledCard;
            this.DropZone = gameObject.AddComponent<DropZone>();
        }
        private void RenderInstalledCard(Zone zone, Card card)
        {
            visuals[card] = printer.Print(card);
        }

        private void DestroyUninstalledCard(Zone zone, Card card)
        {
            Object.Destroy(visuals[card]);
        }

        void IPaidAbilityObserver.NotifyPaidAbilityAvailable(Ability ability, Card source)
        {
            visuals[source]
                .AddComponent<Droppable>()
                .Represent(new InteractiveAbility(ability, paidWindowTrigger));
        }
    }
}
