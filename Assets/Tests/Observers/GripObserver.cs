using model.cards;
using model.zones.runner;

namespace tests.observers
{
    class GripObserver : IGripRemovalObserver
    {
        public ICard LastRemoved { get; private set; }
        public int TotalRemoved { get; private set; } = 0;

        void IGripRemovalObserver.NotifyCardRemoved(ICard card)
        {
            LastRemoved = card;
            TotalRemoved++;
        }
    }
}