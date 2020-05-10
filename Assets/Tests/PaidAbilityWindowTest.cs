using model;
using model.cards;
using model.cards.runner;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using tests.mocks;
using tests.observers;
using view.log;


namespace tests
{
    public class PaidAbilityWindowTest
    {
        private Game game;
        private PassiveCorp passiveCorp;
        private FastForwardRunner ffRunner;
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
            game = new MockGames().WithRunnerCards(runnerCards);
            var gameFlowLog = new GameFlowLog();
            gameFlowLog.Display(game);
            passiveCorp = new PassiveCorp(game);
            ffRunner = new FastForwardRunner(game);
            paidAbilityObserver = new PaidAbilityObserver();
            hopper = new SportsHopper();
            game.runner.paidWindow.ObserveAbility(paidAbilityObserver);
        }

        [Test, Timeout(1000)]
        async public void ShouldPopHopper()
        {
            game.Start();
            await passiveCorp.SkipTurn();
            var zones = game.runner.zones;
            hopper.MoveTo(zones.grip.zone);
            var gripObserver = new GripObserver();
            var rigObserver = new RigObserver();
            var heapObserver = new HeapObserver();
            zones.grip.zone.ObserveAdditions(gripObserver);
            zones.rig.zone.ObserveRemovals(rigObserver);
            zones.heap.zone.ObserveAdditions(heapObserver);
            ffRunner.FastForwardToActionPhase();
            await game.runner.actionCard.Install(hopper).Trigger(game); // TODO `GenericInstall` refactoring broke this
            var popHopper = paidAbilityObserver.NewestPaidAbility;

            await popHopper.Trigger(game);

            Assert.AreEqual(3, gripObserver.TotalAdded);
            Assert.AreEqual(hopper, rigObserver.LastUninstalled);
            Assert.AreEqual(hopper, heapObserver.LastAdded);
        }

        [Test, Timeout(1000)]
        async public void ShouldUsePaidAbilityOnRunnerTurn()
        {
            game.Start();
            await passiveCorp.SkipTurn();
            hopper.MoveTo(game.runner.zones.grip.zone);

            ffRunner.FastForwardToActionPhase();
            await RunnerAction();
            await RunnerAction();
            await game.runner.actionCard.Install(hopper).Trigger(game);
            var popHopper = paidAbilityObserver.NewestPaidAbility;
            PassWindow();
            await RunnerAction();
            PassWindow();
            PassWindow();
            PassWindow();
            PassWindow();
            await CorpAction();
            PassWindow();
            await CorpAction();
            PassWindow();
            await CorpAction();
            PassWindow();
            passiveCorp.DiscardRandomCards();
            PassWindow();
            PassWindow();
            PassWindow();
            await RunnerAction();
            PassWindow();
            await RunnerAction();
            await popHopper.Trigger(game);
            PassWindow();
            await RunnerAction();
            await RunnerAction();
        }

        [Test, Timeout(1000)]
        async public void ShouldUsePaidAbilityOnCorpTurn()
        {
            game.Start();
            await passiveCorp.SkipTurn();
            hopper.MoveTo(game.runner.zones.grip.zone);

            await RunnerAction();
            await RunnerAction();
            await game.runner.actionCard.Install(hopper).Trigger(game);
            var popHopper = paidAbilityObserver.NewestPaidAbility;
            PassWindow();
            await RunnerAction();
            PassWindow();
            PassWindow();
            PassWindow();
            PassWindow();
            await CorpAction();
            PassWindow();
            await CorpAction();
            await popHopper.Trigger(game);
            PassWindow();
            await CorpAction();
            passiveCorp.DiscardRandomCards();
            await RunnerAction();
            await RunnerAction();
            await RunnerAction();
            await RunnerAction();
        }

        async private Task RunnerAction()
        {
            await game.runner.actionCard.TakeAction();
            await game.runner.actionCard.credit.Trigger(game);
            ffRunner.SkipPaidWindow();
        }

        async private Task CorpAction()
        {
            await game.corp.actionCard.TakeAction();
            await game.corp.actionCard.credit.Trigger(game);
        }

        private void PassWindow()
        {
            game.corp.paidWindow.Pass();
            game.runner.paidWindow.Pass();
        }
    }
}