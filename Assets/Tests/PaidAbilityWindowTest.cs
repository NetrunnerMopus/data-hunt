using NUnit.Framework;
using model;
using model.cards;
using System.Collections.Generic;
using model.cards.runner;
using tests.observers;
using tests.mocks;
using view.log;
using model.play;
using model.timing;

namespace tests
{
    public class PaidAbilityWindowTest
    {
        private Game game;
        private PassiveCorp passiveCorp;
        private Ability runnerAction;
        private Ability corpAction;
        private PaidAbilityObserver paidAbilityObserver;
        private SportsHopper hopper;
        private PaidWindow window;

        [SetUp]
        public void SetUp()
        {
            var runnerCards = new List<ICard>();
            for (int i = 0; i < 20; i++)
            {
                runnerCards.Add(new Diesel());
            }
            game = new Game(new Decks().DemoCorp(), new Deck(runnerCards));
            var gameFlowLog = new GameFlowLog();
            gameFlowLog.Display(game);
            passiveCorp = new PassiveCorp(game);
            runnerAction = game.runner.actionCard.credit;
            corpAction = game.corp.actionCard.credit;
            paidAbilityObserver = new PaidAbilityObserver();
            hopper = new SportsHopper();
            window = game.flow.paidWindow;
            window.ObserveAbility(paidAbilityObserver);
        }

        [Test, Timeout(1000)]
        public void ShouldPopHopper()
        {
            game.Start();
            passiveCorp.SkipTurn();
            var zones = game.runner.zones;
            zones.grip.Add(hopper);
            var gripObserver = new GripObserver();
            var rigObserver = new RigObserver();
            var heapObserver = new HeapObserver();
            zones.grip.ObserveAdditions(gripObserver);
            zones.rig.ObserveUninstallations(rigObserver);
            zones.heap.Observe(heapObserver);
            game.runner.actionCard.Install(hopper).Trigger(game);
            var popHopper = paidAbilityObserver.NewestPaidAbility;

            popHopper.Trigger(game);

            Assert.AreEqual(3, gripObserver.TotalAdded);
            Assert.AreEqual(hopper, rigObserver.LastUninstalled);
            Assert.AreEqual(hopper, heapObserver.LastAdded);
        }

        [Test, Timeout(1000)]
        public void ShouldUsePaidAbilityOnRunnerTurn()
        {
            game.Start();
            passiveCorp.SkipTurn();
            game.runner.zones.grip.Add(hopper);

            runnerAction.Trigger(game);
            runnerAction.Trigger(game);
            game.runner.actionCard.Install(hopper).Trigger(game);
            var popHopper = paidAbilityObserver.NewestPaidAbility;
            window.Pass();
            runnerAction.Trigger(game);
            window.Pass();
            window.Pass();
            window.Pass();
            window.Pass();
            corpAction.Trigger(game);
            window.Pass();
            corpAction.Trigger(game);
            window.Pass();
            corpAction.Trigger(game);
            window.Pass();
            passiveCorp.DiscardRandomCards();
            window.Pass();
            window.Pass();
            window.Pass();
            runnerAction.Trigger(game);
            window.Pass();
            runnerAction.Trigger(game);
            popHopper.Trigger(game);
            window.Pass();
            runnerAction.Trigger(game);
            runnerAction.Trigger(game);
        }

        [Test, Timeout(1000)]
        public void ShouldUsePaidAbilityOnCorpTurn()
        {
            game.Start();
            passiveCorp.SkipTurn();
            game.runner.zones.grip.Add(hopper);

            runnerAction.Trigger(game);
            runnerAction.Trigger(game);
            game.runner.actionCard.Install(hopper).Trigger(game);
            var popHopper = paidAbilityObserver.NewestPaidAbility;
            window.Pass();
            runnerAction.Trigger(game);
            window.Pass();
            window.Pass();
            window.Pass();
            window.Pass();
            corpAction.Trigger(game);
            window.Pass();
            corpAction.Trigger(game);
            popHopper.Trigger(game);
            window.Pass();
            corpAction.Trigger(game);
            passiveCorp.DiscardRandomCards();
            runnerAction.Trigger(game);
            runnerAction.Trigger(game);
            runnerAction.Trigger(game);
            runnerAction.Trigger(game);
        }
    }
}