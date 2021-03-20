using System.Collections.Generic;
using controller;
using model;
using model.cards;
using model.zones;
using UnityEngine;

namespace view.gui
{
    public class GripFan
    {
        private GameObject gameObject;
        private Runner runner;
        private DropZone playZone;
        private DropZone rigZone;
        private DropZone heapZone;
        private CardZoom zoom;
        private Dictionary<Card, GameObject> visuals = new Dictionary<Card, GameObject>();
        private CardPrinter printer;
        public DropZone DropZone { get; private set; }

        public GripFan(GameObject gameObject, Runner runner, DropZone playZone, DropZone rigZone, DropZone heapZone, BoardParts parts)
        {
            this.gameObject = gameObject;
            this.runner = runner;
            this.playZone = playZone;
            this.rigZone = rigZone;
            this.heapZone = heapZone;
            this.printer = parts.Print(gameObject);
            this.DropZone = gameObject.AddComponent<DropZone>();
            runner.zones.grip.zone.Added += RenderNewCard;
            runner.zones.grip.zone.Removed += DestroyRemovedCard;
        }

        private void RenderNewCard(Zone zone, Card card)
        {
            var visual = printer.Print(card);
            visuals[card] = visual;
            var droppable = visual.AddComponent<Droppable>();
            droppable.Reorderable = true;
            var type = card.Type;
            if (type.Playable)
            {
                droppable.Represent(new InteractiveAbility(runner.Acting.Play(card), playZone));
            }
            if (type.Installable)
            {
                droppable.Represent(new InteractiveAbility(runner.Acting.Install(card), rigZone));
            }
            droppable.Represent(new InteractiveDiscard(card, heapZone, runner));
        }

        private void DestroyRemovedCard(Zone zone, Card card)
        {
            Object.Destroy(visuals[card]);
            visuals.Remove(card);
        }
    }
}
