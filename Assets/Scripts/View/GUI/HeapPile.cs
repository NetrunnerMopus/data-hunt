using controller;
using model;
using UnityEngine;

namespace view.gui
{
    public class HeapPile
    {
        private GameObject gameObject;
        private CardPrinter printer;
        public DropZone DropZone { get; private set; }

        public HeapPile(GameObject gameObject, Runner runner, BoardParts parts)
        {
            this.gameObject = gameObject;
            this.printer = parts.Print(gameObject);
            this.DropZone = gameObject.AddComponent<DropZone>();
            runner.zones.heap.zone.Added += (zone, card) => printer.Print(card);
        }
    }
}
