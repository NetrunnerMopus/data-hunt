using model.cards;
using model.zones;
using model.zones.runner;

namespace tests.observers
{
    class GripObserver : IZoneAdditionObserver, IZoneRemovalObserver
    {
        public Card LastRemoved { get; private set; }
        public int TotalRemoved { get; private set; } = 0;
        public Card LastAdded { get; private set; }
        public int TotalAdded { get; private set; } = 0;

        void IZoneAdditionObserver.NotifyCardAdded(Card card)
        {
            LastAdded = card;
            TotalAdded++;
        }

        void IZoneRemovalObserver.NotifyCardRemoved(Card card)
        {
            LastRemoved = card;
            TotalRemoved++;
        }
    }
}