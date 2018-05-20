using model.cards;
using model.zones.runner;

namespace tests.observers
{
    class HeapObserver : IHeapObserver
    {
        public ICard LastAdded { get; private set; }
        public int TotalAdded { get; private set; } = 0;

        void IHeapObserver.NotifyCardAdded(ICard card)
        {
            LastAdded = card;
            TotalAdded++;
        }
    }
}