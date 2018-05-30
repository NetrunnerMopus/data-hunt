using model.cards;
using model.zones.runner;

namespace tests.observers
{
    class GripObserver : IGripAdditionObserver, IGripRemovalObserver
    {
        public ICard LastRemoved { get; private set; }
        public int TotalRemoved { get; private set; } = 0;
        public ICard LastAdded { get; private set; }
        public int TotalAdded { get; private set; } = 0;

        void IGripAdditionObserver.NotifyCardAdded(ICard card)
        {
            LastAdded = card;
            TotalAdded++;
        }

        void IGripRemovalObserver.NotifyCardRemoved(ICard card)
        {
            LastRemoved = card;
            TotalRemoved++;
        }
    }
}