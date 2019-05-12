using model.cards;
using System;
using System.Collections.Generic;

namespace model.zones.runner
{
    public class Rig : IInstallDestination
    {
        private List<Card> cards = new List<Card>();
        private HashSet<IInstallationObserver> installations = new HashSet<IInstallationObserver>();
        private HashSet<IUninstallationObserver> uninstallations = new HashSet<IUninstallationObserver>();

        public void Install(Card card)
        {
            cards.Add(card);
            foreach (var observer in installations)
            {
                observer.NotifyInstalled(card);
            }
        }

        public void Uninstall(Card card)
        {
            if (!cards.Contains(card))
            {
                throw new Exception("Tried to uninstall a card, which is not in the Rig: " + card);
            }
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

        void IInstallDestination.Host(Card card)
        {
            Install(card);
        }
    }

    public interface IInstallationObserver
    {
        void NotifyInstalled(Card card);
    }

    public interface IUninstallationObserver
    {
        void NotifyUninstalled(Card card);
    }
}
