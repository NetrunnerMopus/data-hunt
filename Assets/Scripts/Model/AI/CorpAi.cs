using model.cards.corp;
using model.play;
using model.play.corp;
using model.timing.corp;
using model.zones.corp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace model.ai
{
    public class CorpAi : ICorpActionObserver, IHqDiscardObserver, IRezWindowObserver, IRezzableObserver, IActionPotentialObserver, IUsabilityObserver
    {
        private Game game;
        private Zones zones;
        private Task Thinking() => Task.Delay(600);
        private HashSet<Ability> legalActions = new HashSet<Ability>();
        private Random random;

        public CorpAi(Game game, Random random)
        {
            this.game = game;
            this.random = random;
            zones = game.corp.zones;
        }

        public void Play()
        {
            game.corp.actionCard.ObservePotentialActions(this);
            game.flow.corpTurn.ObserveActions(this);
            game.flow.corpTurn.rezWindow.ObserveWindow(this);
            zones.hq.ObserveDiscarding(this);
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