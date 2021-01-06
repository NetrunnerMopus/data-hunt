using System.Collections.Generic;
using model.cards;
using model.zones;
using model.zones.corp;
using UnityEngine;
using UnityEngine.UI;

namespace view.gui
{
    public class IceColumn : IZoneAdditionObserver, IZoneRemovalObserver
    {
        private readonly GameObject column;
        private readonly IceStack stack;
        private CardPrinter printer;
        private IDictionary<Card, GameObject> visuals = new Dictionary<Card, GameObject>();

        public IceColumn(GameObject column, IceStack stack, BoardParts parts)
        {
            this.column = column;
            this.stack = stack;
            var layout = column.AddComponent<VerticalLayoutGroup>();
            layout.childAlignment = TextAnchor.UpperCenter;
            layout.childControlWidth = true;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;
            printer = parts.Print(column);
            printer.Sideways = true;
            stack.Zone.ObserveAdditions(this);
            stack.Zone.ObserveRemovals(this);
        }

        void IZoneAdditionObserver.NotifyCardAdded(Card card)
        {
            var printedCard = printer.Print(card);
            visuals[card] = printedCard;
        }

        void IZoneRemovalObserver.NotifyCardRemoved(Card card)
        {
            var visual = visuals[card];
            visuals.Remove(card);
            Object.Destroy(visual);
        }
    }
}
