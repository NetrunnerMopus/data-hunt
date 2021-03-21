using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using model.cards;
using model.choices;
using model.choices.trash;
using model.play;
using model.player;
using model.rez;
using model.steal;
using model.timing;
using model.timing.corp;
using model.zones;

namespace model.ai
{
    public class CorpAi :
        IPilot,
        IActionPotentialObserver
    {
        private Game game;
        private zones.corp.Zones zones;
        private Task Thinking() => Task.Delay(1700);
        private IList<Ability> actions = new List<Ability>();
        private IList<Ability> legalActions = new List<Ability>();
        private IList<CardAbility> paidAbilities = new List<CardAbility>();
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
            game.corp.turn.TakingAction += TakeAction;
            game.corp.Rezzing.Window.Opened += RezSomething;
            game.corp.paidWindow.Opened += Pass;
            game.corp.paidWindow.Added += RememberPaidAbility;
            game.corp.paidWindow.Removed += ForgetPaidAbility;
            zones.hq.DiscardingOne += DiscardOne;
        }

        async Task<IEffect> IPilot.TriggerFromSimultaneous(IList<IEffect> effects)
        {
            await Thinking();
            return effects.First();
        }

        private async Task TakeAction(ITurn turn)
        {
            await Thinking();
            var randomLegalAction = legalActions.ElementAt(random.Next(0, legalActions.Count));
            UnityEngine.Debug.Log("Choosing to " + randomLegalAction);
            await randomLegalAction.Trigger();
        }

        private void DiscardOne()
        {
            zones.hq.Discard(zones.hq.Random(), zones.archives);
        }

        async private Task RezSomething(RezWindow window, List<Ability> rezzables)
        {
            foreach (var rezzable in rezzables)
            {
                if (rezzable.Usable)
                {
                    await Thinking();
                    await rezzable.Trigger();
                }
            }
            window.Pass();
        }

        void IActionPotentialObserver.NotifyPotentialAction(Ability action)
        {
            actions.Add(action);
            action.UsabilityChanged += NotifyUsable;
        }

        private void RememberPaidAbility(PaidWindow window, CardAbility ability)
        {
            paidAbilities.Add(ability);
            ability.Ability.UsabilityChanged += NotifyUsable;
        }

        private void ForgetPaidAbility(PaidWindow window, CardAbility ability)
        {
            paidAbilities.Add(ability);
            ability.Ability.UsabilityChanged -= NotifyUsable;
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
            if (paidAbilities.Select(it => it.Ability).Contains(ability))
            {
                if (usable)
                {
                    await ability.Trigger();
                }
            }
        }

        IDecision<string, Card> IPilot.ChooseACard() => new CardChoice(random);
        IDecision<string, IInstallDestination> IPilot.ChooseAnInstallDestination() => new InstallDestinationChoice(random);

        private void Pass(PaidWindow window)
        {
            window.Pass();
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
