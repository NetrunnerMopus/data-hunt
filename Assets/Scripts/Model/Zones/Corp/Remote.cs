using model.cards;
using System.Collections.Generic;

namespace model.zones.corp
{
    public class Remote : IServer
    {
        string IServer.Name => "Remote";
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
    }

    public interface IServerContentObserver
    {
        void NotifyCardInstalled(Card card);
    }
}
