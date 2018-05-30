using model.cards;
using model.zones.runner;

namespace tests.observers
{
    class RigObserver : IUninstallationObserver
    {
        public ICard LastUninstalled { get; private set; }

        void IUninstallationObserver.NotifyUninstalled(ICard card)
        {
            LastUninstalled = card;
        }
    }
}