using model.cards;
using System.Collections.Generic;

namespace model.zones.runner
{
    public class Rig
    {
        private List<ICard> cards = new List<ICard>();
        private HashSet<IInstallationObserver> installations = new HashSet<IInstallationObserver>();
        private HashSet<IUninstallationObserver> uninstallations = new HashSet<IUninstallationObserver>();

        public void Install(ICard card)
        {
            cards.Add(card);
            foreach (var observer in installations)
            {
                observer.NotifyInstalled(card);
            }
        }

        public void Uninstall(ICard card)
        {
            cards.Remove(card);
            foreach (var observer in uninstallations)
            {
                observer.NotifyUninstalled(card);
            }
        }

        public void ObserveInstallations(IInstallationObserver observer)
        {
            installations.Add(observer);
        }

        public void ObserveUninstallations(IUninstallationObserver observer)
        {
            uninstallations.Add(observer);
        }
    }

    public interface IInstallationObserver
    {
        void NotifyInstalled(ICard card);
    }

    public interface IUninstallationObserver
    {
        void NotifyUninstalled(ICard card);
    }
}
