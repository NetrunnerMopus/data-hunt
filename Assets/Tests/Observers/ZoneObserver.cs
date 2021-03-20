using model.cards;
using model.zones;

namespace tests.observers
{
    class ZoneObserver
    {
        public Card LastRemoved { get; private set; }
        public int TotalRemoved { get; private set; } = 0;
        public Card LastAdded { get; private set; }
        public int TotalAdded { get; private set; } = 0;

        public ZoneObserver(Zone zone)
        {
            zone.Added += ObserveAdded;
            zone.Removed += ObserveRemoved;
        }

        private void ObserveAdded(Zone zone, Card card)
        {
            LastAdded = card;
            TotalAdded++;
        }

        private void ObserveRemoved(Zone zone, Card card)
        {
            LastRemoved = card;
            TotalRemoved++;
        }
    }
}
