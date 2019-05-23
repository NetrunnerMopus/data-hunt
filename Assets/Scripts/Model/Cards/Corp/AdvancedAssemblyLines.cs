using model.cards.types;
using model.costs;
using model.effects.corp;
using model.play;
using model.timing;
using model.zones;
using System.Collections.Generic;
using System.Linq;

namespace model.cards.corp
{
    public class AdvancedAssemblyLines : Card
    {
        override public string FaceupArt => "advanced-assembly-lines";
        override public string Name => "Advanced Assembly Lines";
        override public Faction Faction => Factions.HAAS_BIOROID;
        override public int InfluenceCost => 2;
        override public ICost PlayCost => new CorpCreditCost(1);
        override public IEffect Activation => new AdvancedAssemblyLinesActivation(this);
        override public IType Type => new Asset();

        private class AdvancedAssemblyLinesActivation : IEffect
        {
            private readonly Card card;

            public AdvancedAssemblyLinesActivation(Card card)
            {
                this.card = card;
            }

            void IEffect.Resolve(Game game)
            {
                IEffect gain = new Gain(3);
                gain.Resolve(game);
                var paidWindow = game.corp.paidWindow;
                var pop = new Ability(
                    cost: new Conjunction(paidWindow.Permission(), new SelfTrash(card)),
                    effect: new AdvancedAssemblyLinesInstall()
                );
                paidWindow.Add(pop, card);
            }

            void IEffect.Observe(IImpactObserver observer, Game game)
            {
                observer.NotifyImpact(true, this);
            }
        }

        private class AdvancedAssemblyLinesInstall : IEffect, ICardsObserver
        {
            private HashSet<IImpactObserver> observers = new HashSet<IImpactObserver>();
            private List<Card> installables = new List<Card>();

            void IEffect.Resolve(Game game)
            {
                var pilot = game.corp.pilot;
                var installable = pilot.ChooseACard().Declare(installables);
                var destination = pilot.ChooseAnInstallDestination().Declare(installable.FindInstallDestinations(game));
                var install = new Install(
                    card: installable,
                    destination: destination
                );
                UnityEngine.Debug.Log("Installing " + installable.Name + " in the " + destination);
                install.Resolve(game);
            }

            void IEffect.Observe(IImpactObserver observer, Game game)
            {
                observers.Add(observer);
                game.corp.zones.hq.Zone.ObserveCards(this);
            }

            void ICardsObserver.NotifyCards(List<Card> cards)
            {
                installables = cards.Where(card => (card.Type.Installable && !(card.Type is Agenda))).ToList();
                foreach (var observer in observers)
                {
                    observer.NotifyImpact(installables.Count > 0, this);
                }
            }
        }
    }
}
