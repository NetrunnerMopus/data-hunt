using model.play;
using model.play.corp;
using model.player;
using model.timing.corp;
using model.zones.corp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace model.ai
{
    public class CorpAi : IPilot, ICorpActionObserver, IHqDiscardObserver, IRezWindowObserver, IRezzableObserver, IActionPotentialObserver, IUsabilityObserver
    {
        private Game game;
        private Zones zones;
        private Task Thinking() => Task.Delay(4000);
        private HashSet<Ability> legalActions = new HashSet<Ability>();
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
            game.flow.corpTurn.ObserveActions(this);
            game.flow.corpTurn.rezWindow.ObserveWindow(this);
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
            game.flow.corpTurn.rezWindow.Pass();
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
            action.ObserveUsability(this, game);
        }

        void IUsabilityObserver.NotifyUsable(bool usable, Ability ability)
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
    }
}