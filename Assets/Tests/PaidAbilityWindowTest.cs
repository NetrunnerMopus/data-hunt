using System.Collections.Generic;
using System.Threading.Tasks;
using model;
using model.cards;
using model.cards.runner;
using NUnit.Framework;
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
            game = MockGames.Unpiloted();
            var runnerCards = new List<Card>();
            for (int i = 0; i < 20; i++)
            {
                runnerCards.Add(new Diesel(game));
            }
            var gameFlowLog = new GameFlowLog();
            gameFlowLog.Display(game);
            passiveCorp = new PassiveCorp(game);
            ffRunner = new FastForwardRunner(game);
            paidAbilityObserver = new PaidAbilityObserver();
            hopper = new SportsHopper(game);
            game.runner.paidWindow.ObserveAbility(paidAbilityObserver);
            game.Start(Decks.DemoCorp(game), MockGames.MasqueDeck(game, runnerCards));
        }

        [Test, Timeout(1000)]
        async public void ShouldPopHopper()
        {
            await passiveCorp.SkipTurn();
            var zones = game.runner.zones;
            hopper.MoveTo(zones.grip.zone);
            var gripObserver = new ZoneObserver(zones.grip.zone);
            var rigObserver = new ZoneObserver(zones.rig.zone);
            var heapObserver = new ZoneObserver(zones.heap.zone);
            ffRunner.FastForwardToActionPhase();
            await game.runner.Acting.Install(hopper).Trigger(); // TODO `GenericInstall` refactoring broke this
            var popHopper = paidAbilityObserver.NewestPaidAbility;

            await popHopper.Trigger();

            Assert.AreEqual(3, gripObserver.TotalAdded);
            Assert.AreEqual(hopper, rigObserver.LastRemoved);
            Assert.AreEqual(hopper, heapObserver.LastAdded);
        }

        [Test, Timeout(1000)]
        async public void ShouldUsePaidAbilityOnRunnerTurn()
        {
            await passiveCorp.SkipTurn();
            hopper.MoveTo(game.runner.zones.grip.zone);

            ffRunner.FastForwardToActionPhase();
            await RunnerAction();
            await RunnerAction();
            await game.runner.Acting.Install(hopper).Trigger();
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
            await popHopper.Trigger();
            PassWindow();
            await RunnerAction();
            await RunnerAction();
        }

        [Test, Timeout(1000)]
        async public void ShouldUsePaidAbilityOnCorpTurn()
        {
            await passiveCorp.SkipTurn();
            hopper.MoveTo(game.runner.zones.grip.zone);

            await RunnerAction();
            await RunnerAction();
            await game.runner.Acting.Install(hopper).Trigger();
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
            await popHopper.Trigger();
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
            await game.runner.Acting.TakeAction();
            await game.runner.Acting.credit.Trigger();
            ffRunner.SkipPaidWindow();
        }

        async private Task CorpAction()
        {
            await game.corp.Acting.TakeAction();
            await game.corp.Acting.credit.Trigger();
        }

        private void PassWindow()
        {
            game.corp.paidWindow.Pass();
            game.runner.paidWindow.Pass();
        }
    }
}
