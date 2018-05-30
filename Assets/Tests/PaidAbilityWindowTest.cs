using NUnit.Framework;
using model;
using model.cards;
using System.Collections.Generic;
using model.cards.runner;
using tests.observers;
using tests.mocks;
using view.log;

namespace tests
{
    public class PaidAbilityWindowTest
    {
        [Test, Timeout(1000)]
        public void ShouldPopHopper()
        {
            var runnerCards = new List<ICard>();
            for (int i = 0; i < 20; i++)
            {
                runnerCards.Add(new Diesel());
            }
            var game = new Game(new Decks().DemoCorp(), new Deck(runnerCards));
            var gameFlowLog = new GameFlowLog();
            gameFlowLog.Display(game);
            var passiveCorp = new PassiveCorp(game);
            var grip = game.runner.zones.grip;
            var heap = game.runner.zones.heap;
            var rig = game.runner.zones.rig;
            var actionCard = game.runner.actionCard;
            var clickForCredit = actionCard.credit;
            var gripObserver = new GripObserver();
            var rigObserver = new RigObserver();
            var heapObserver = new HeapObserver();
            var paidAbilityObserver = new PaidAbilityObserver();
            heap.Observe(heapObserver);
            rig.ObserveUninstallations(rigObserver);
            var hopper = new SportsHopper();
            var window = game.flow.paidWindow;
            window.ObserveAbility(paidAbilityObserver);

            game.Start();
            passiveCorp.SkipTurn();
            clickForCredit.Trigger(game);
            clickForCredit.Trigger(game);
            grip.Add(hopper);
            actionCard.Install(hopper).Trigger(game);
            var popHopper = paidAbilityObserver.NewestPaidAbility;
            window.Pass();
            clickForCredit.Trigger(game);
            window.Pass();
            window.Pass();
            passiveCorp.SkipTurn();
            window.Pass();
            window.Pass();
            clickForCredit.Trigger(game);
            window.Pass();
            clickForCredit.Trigger(game);
            grip.ObserveAdditions(gripObserver);
            popHopper.Trigger(game);
            window.Pass();
            clickForCredit.Trigger(game);
            clickForCredit.Trigger(game);

            Assert.AreEqual(3, gripObserver.TotalAdded);
            Assert.AreEqual(hopper, rigObserver.LastUninstalled);
            Assert.AreEqual(hopper, heapObserver.LastAdded);
        }
    }
}