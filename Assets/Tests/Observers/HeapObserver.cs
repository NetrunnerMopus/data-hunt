using model.cards;
using model.zones;
using model.zones.runner;

namespace tests.observers
{
    class HeapObserver : IZoneAdditionObserver
    {
        public Card LastAdded { get; private set; }
        public int TotalAdded { get; private set; } = 0;

        void IZoneAdditionObserver.NotifyCardAdded(Card card)
        {
            LastAdded = card;
            TotalAdded++;
        }
    }
}