using model.cards;
using System.Collections.Generic;

namespace model.zones.runner
{
    public class Rig
    {
        private List<ICard> cards = new List<ICard>();
        private HashSet<IInstallationObserver> installations = new HashSet<IInstallationObserver>();

        public void Install(ICard card)
        {
            cards.Add(card);
            foreach (var observer in installations)
            {
                observer.NotifyInstalled(card);
            }
        }

        public void ObserveInstallations(IInstallationObserver observer)
        {
            installations.Add(observer);
        }
    }

    public interface IInstallationObserver
    {
        void NotifyInstalled(ICard card);
    }
}
