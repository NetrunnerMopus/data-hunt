using model.cards;
using model.play;
using model.play.corp;
using model.player;
using model.timing;
using model.timing.corp;
using model.zones;
using model.zones.corp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace model.ai
{
    public class CorpAi :
        IPilot,
        ICorpActionObserver,
        IHqDiscardObserver,
        IRezWindowObserver,
        IRezzableObserver,
        IActionPotentialObserver,
        IUsabilityObserver,
        IPaidWindowObserver,
        IPaidAbilityObserver
    {
        private Game game;
        private zones.corp.Zones zones;
        private Task Thinking() => Task.Delay(700);
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
            game.corp.actionCard.ObservePotentialActions(this);
            game.corp.turn.ObserveActions(this);
            game.corp.turn.rezWindow.ObserveWindow(this);
            game.corp.paidWindow.ObserveWindow(this);
            game.corp.paidWindow.ObserveAbility(this);
            zones.hq.ObserveDiscarding(this);
        }

        async Task<IEffect> IPilot.TriggerFromSimultaneous(List<IEffect> effects)
        {
            await Thinking();
            return effects.First();
        }

        async void ICorpActionObserver.NotifyActionTaking()
        {
            await Thinking();
            var randomLegalAction = legalActions.ElementAt(random.Next(0, legalActions.Count));
            UnityEngine.Debug.Log("Choosing to " + randomLegalAction);
            randomLegalAction.Trigger(game);
        }

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
                rezzable.ObserveRezzable(this);
            }
            game.corp.turn.rezWindow.Pass();
        }


        async Task IRezzableObserver.NotifyRezzable(Rezzable rezzable)
        {
            await Thinking();
            rezzable.Rez();
        }

        async Task IRezzableObserver.NotifyNotRezzable()
        {
            await Task.CompletedTask;
        }

        void IRezWindowObserver.NotifyRezWindowClosed()
        {
        }

        void IActionPotentialObserver.NotifyPotentialAction(Ability action)
        {
            actions.Add(action);
            action.ObserveUsability(this, game);
        }

        public void NotifyPaidAbilityAvailable(Ability ability, Card source)
        {
            paidAbilities.Add(ability);
            ability.ObserveUsability(this, game);
        }

        void IUsabilityObserver.NotifyUsable(bool usable, Ability ability)
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
                    ability.Trigger(game);
                }
            }
        }

        IChoice<Card> IPilot.ChooseACard() => new CardChoice();
        IChoice<IInstallDestination> IPilot.ChooseAnInstallDestination() => new InstallDestinationChoice();

        void IPaidWindowObserver.NotifyPaidWindowOpened(PaidWindow window)
        {
            window.Pass();
        }

        void IPaidWindowObserver.NotifyPaidWindowClosed(PaidWindow window)
        {
        }

        private class CardChoice : IChoice<Card>
        {
            Card IChoice<Card>.Declare(IEnumerable<Card> items) => items.First();
        }

        private class InstallDestinationChoice : IChoice<IInstallDestination>
        {
            IInstallDestination IChoice<IInstallDestination>.Declare(IEnumerable<IInstallDestination> items) => items.First();
        }
    }
}