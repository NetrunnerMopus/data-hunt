using model.cards;
using model.zones.runner;

namespace tests.observers
{
    class GripObserver : IGripAdditionObserver, IGripRemovalObserver
    {
        public Card LastRemoved { get; private set; }
        public int TotalRemoved { get; private set; } = 0;
        public Card LastAdded { get; private set; }
        public int TotalAdded { get; private set; } = 0;

        void IGripAdditionObserver.NotifyCardAdded(Card card)
        {
            LastAdded = card;
            TotalAdded++;
        }

        void IGripRemovalObserver.NotifyCardRemoved(Card card)
        {
            LastRemoved = card;
            TotalRemoved++;
        }
    }
}