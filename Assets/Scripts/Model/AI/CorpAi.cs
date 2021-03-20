using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using model.cards;
using model.choices;
using model.choices.trash;
using model.play;
using model.player;
using model.steal;
using model.timing;
using model.timing.corp;
using model.zones;
using model.zones.corp;

namespace model.ai
{
    public class CorpAi :
        IPilot,
        ICorpActionObserver,
        IHqDiscardObserver,
        IRezWindowObserver,
        IActionPotentialObserver,
        IPaidWindowObserver,
        IPaidAbilityObserver
    {
        private Game game;
        private zones.corp.Zones zones;
        private Task Thinking() => Task.Delay(1700);
        private HashSet<Ability> actions = new HashSet<Ability>();
        private HashSet<Ability> legalActions = new HashSet<Ability>();
        private HashSet<Ability> paidAbilities = new HashSet<Ability>();
        private Random random;

        public CorpAi(Random random)
        {
            this.random = random;
        }

        void IPilot.Play(Game game)
        {
            this.game = game;
            zones = game.corp.zones;
            game.corp.Acting.ObservePotentialActions(this);
            game.corp.turn.ObserveActions(this);
            game.corp.turn.rezWindow.ObserveWindow(this);
            game.corp.paidWindow.ObserveWindow(this);
            game.corp.paidWindow.ObserveAbility(this);
            zones.hq.ObserveDiscarding(this);
        }

        async Task<IEffect> IPilot.TriggerFromSimultaneous(IList<IEffect> effects)
        {
            await Thinking();
            return effects.First();
        }

        async void ICorpActionObserver.NotifyActionTaking()
        {
            await Thinking();
            var randomLegalAction = legalActions.ElementAt(random.Next(0, legalActions.Count));
            UnityEngine.Debug.Log("Choosing to " + randomLegalAction);
            await randomLegalAction.Trigger();
        }
        void ICorpActionObserver.NotifyActionTaken(Ability ability) { }

        void IHqDiscardObserver.NotifyDiscarding(bool discarding)
        {
            if (discarding)
            {
                zones.hq.Discard(zones.hq.Random(), zones.archives);
            }
        }

        void IRezWindowObserver.NotifyRezWindowOpened(List<Rezzable> rezzables)
        {
            foreach (var rezzable in rezzables)
            {
                rezzable.Changed += NotifyRezzable;
            }
            game.corp.turn.rezWindow.Pass();
        }


        async private Task NotifyRezzable(Rezzable rezzable)
        {
            if (rezzable.CanRez)
            {
                await Thinking();
                await rezzable.Rez(); // TODO debug this, Corp AI seems to never rez stuff anymore  
            }
        }

        void IRezWindowObserver.NotifyRezWindowClosed()
        {
        }

        void IActionPotentialObserver.NotifyPotentialAction(Ability action)
        {
            actions.Add(action);
            action.UsabilityChanged += NotifyUsable;
        }

        public void NotifyPaidAbilityAvailable(Ability ability, Card source)
        {
            paidAbilities.Add(ability);
            ability.UsabilityChanged += NotifyUsable;
        }

        async private void NotifyUsable(Ability ability, bool usable)
        {
            if (actions.Contains(ability))
            {
                if (usable)
                {
                    legalActions.Add(ability);
                }
                else
                {
                    legalActions.Remove(ability);
                }
            }
            if (paidAbilities.Contains(ability))
            {
                if (usable)
                {
                    await ability.Trigger();
                }
            }
        }

        IDecision<string, Card> IPilot.ChooseACard() => new CardChoice(random);
        IDecision<string, IInstallDestination> IPilot.ChooseAnInstallDestination() => new InstallDestinationChoice(random);

        void IPaidWindowObserver.NotifyPaidWindowOpened(PaidWindow window)
        {
            window.Pass();
        }

        void IPaidWindowObserver.NotifyPaidWindowClosed(PaidWindow window)
        {
        }

        IDecision<Card, ITrashOption> IPilot.ChooseTrashing()
        {
            throw new Exception("Corps shouldn't have to know how to trash");
        }

        IDecision<Card, IStealOption> IPilot.ChooseStealing()
        {
            throw new Exception("Corps shouldn't have to know how to steal");
        }

        private class CardChoice : IDecision<string, Card>
        {
            private Random random;

            public CardChoice(Random random)
            {
                this.random = random;
            }

            Task<Card> IDecision<string, Card>.Declare(string subject, IEnumerable<Card> options) => Task.FromResult(options.PickRandom(random));
        }

        private class InstallDestinationChoice : IDecision<string, IInstallDestination>
        {
            private Random random;

            public InstallDestinationChoice(Random random)
            {
                this.random = random;
            }

            Task<IInstallDestination> IDecision<string, IInstallDestination>.Declare(string subject, IEnumerable<IInstallDestination> options) => Task.FromResult(options.PickRandom(random));
        }
    }

    static class RandomEnumerables
    {
        public static T PickRandom<T>(this IEnumerable<T> items, Random random)
        {
            return items
                .OrderBy(it => random.Next())
                .First();
        }
    }
}
