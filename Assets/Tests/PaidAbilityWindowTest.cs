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
        private Game game;
        private PassiveCorp passiveCorp;
        private PaidAbilityObserver paidAbilityObserver;
        private SportsHopper hopper;

        [SetUp]
        public void SetUp()
        {
            var runnerCards = new List<Card>();
            for (int i = 0; i < 20; i++)
            {
                runnerCards.Add(new Diesel());
            }
            game = new Game(new Decks().DemoCorp(), new Deck(runnerCards));
            var gameFlowLog = new GameFlowLog();
            gameFlowLog.Display(game);
            passiveCorp = new PassiveCorp(game);
            paidAbilityObserver = new PaidAbilityObserver();
            hopper = new SportsHopper();
            game.flow.paidWindow.ObserveAbility(paidAbilityObserver);
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

            RunnerAction();
            RunnerAction();
            game.runner.actionCard.Install(hopper).Trigger(game);
            var popHopper = paidAbilityObserver.NewestPaidAbility;
            PassWindow();
            RunnerAction();
            PassWindow();
            PassWindow();
            PassWindow();
            PassWindow();
            CorpAction();
            PassWindow();
            CorpAction();
            PassWindow();
            CorpAction();
            PassWindow();
            passiveCorp.DiscardRandomCards();
            PassWindow();
            PassWindow();
            PassWindow();
            RunnerAction();
            PassWindow();
            RunnerAction();
            popHopper.Trigger(game);
            PassWindow();
            RunnerAction();
            RunnerAction();
        }

        [Test, Timeout(1000)]
        public void ShouldUsePaidAbilityOnCorpTurn()
        {
            game.Start();
            passiveCorp.SkipTurn();
            game.runner.zones.grip.Add(hopper);

            RunnerAction();
            RunnerAction();
            game.runner.actionCard.Install(hopper).Trigger(game);
            var popHopper = paidAbilityObserver.NewestPaidAbility;
            PassWindow();
            RunnerAction();
            PassWindow();
            PassWindow();
            PassWindow();
            PassWindow();
            CorpAction();
            PassWindow();
            CorpAction();
            popHopper.Trigger(game);
            PassWindow();
            CorpAction();
            passiveCorp.DiscardRandomCards();
            RunnerAction();
            RunnerAction();
            RunnerAction();
            RunnerAction();
        }

        private void RunnerAction()
        {
            game.runner.actionCard.credit.Trigger(game);
        }

        private void CorpAction()
        {
            game.corp.actionCard.credit.Trigger(game);
        }

        private void PassWindow()
        {
            game.flow.paidWindow.Pass();
        }
    }
}