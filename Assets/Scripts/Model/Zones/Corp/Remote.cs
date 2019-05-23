using model.cards;
using System.Collections.Generic;

namespace model.zones.corp
{
    public class Remote : IServer, IInstallDestination
    {
        public Zone Zone => new Zone("Remote");
        public IceColumn Ice => new IceColumn();
        private List<Card> cards = new List<Card>();
        private HashSet<IServerContentObserver> observers = new HashSet<IServerContentObserver>();

        public void InstallWithin(Card card)
        {
            cards.Add(card);
            foreach (var observer in observers)
            {
                observer.NotifyCardInstalled(card);
            }
        }

        public void ObserveInstallations(IServerContentObserver observer)
        {
            observers.Add(observer);
        }

        void IInstallDestination.Host(Card card)
        {
            InstallWithin(card);
        }
    }

    public interface IServerContentObserver
    {
        void NotifyCardInstalled(Card card);
    }
}
