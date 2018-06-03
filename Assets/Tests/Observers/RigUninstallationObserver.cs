using model.cards;
using model.zones.runner;

namespace tests.observers
{
    class RigObserver : IUninstallationObserver
    {
        public Card LastUninstalled { get; private set; }

        void IUninstallationObserver.NotifyUninstalled(Card card)
        {
            LastUninstalled = card;
        }
    }
}