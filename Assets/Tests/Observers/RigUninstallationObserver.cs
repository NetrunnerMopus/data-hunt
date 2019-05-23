using model.cards;
using model.zones;
using model.zones.runner;

namespace tests.observers
{
    class RigObserver : IZoneRemovalObserver
    {
        public Card LastUninstalled { get; private set; }

        void IZoneRemovalObserver.NotifyCardRemoved(Card card)
        {
            LastUninstalled = card;
        }
    }
}