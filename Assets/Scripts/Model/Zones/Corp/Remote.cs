using model.cards;
using System.Collections.Generic;

namespace model.zones.corp
{
    internal class Remote
    {
        private List<ICard> cards = new List<ICard>();
        private HashSet<IServerContentObserver> observers = new HashSet<IServerContentObserver>();

        internal void InstallWithin(ICard card)
        {
            cards.Add(card);
            foreach (var observer in observers)
            {
                observer.NotifyCardInstalled(card);
            }
        }

        internal void ObserveInstallations(IServerContentObserver observer)
        {
            observers.Add(observer);
        }
    }

    internal interface IServerContentObserver
    {
        void NotifyCardInstalled(ICard card);
    }
}
