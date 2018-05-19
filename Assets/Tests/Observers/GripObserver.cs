using model;
using model.cards;

namespace tests.observers
{
    class GripObserver : IGripRemovalObserver
    {
        public ICard LastRemoved { get; private set; }

        void IGripRemovalObserver.NotifyCardRemoved(ICard card)
        {
            LastRemoved = card;
        }
    }
}