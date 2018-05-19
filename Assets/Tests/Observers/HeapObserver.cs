using model;
using model.cards;

namespace tests.observers
{
    class HeapObserver : IHeapObserver
    {
        public ICard LastAdded { get; private set; }

        void IHeapObserver.NotifyCardAdded(ICard card)
        {
            LastAdded = card;
        }
    }
}