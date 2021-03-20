using NUnit.Framework;
using model;
using tests.observers;
using tests.mocks;
using view.log;

namespace tests
{
    public class CorpActionCardTest
    {
        [Test]
        async public void ShouldClickForCredit()
        {
            var game = MockGames.StartUnpiloted();
            var gameFlowLog = new GameFlowLog();
            gameFlowLog.Display(game);
            var balance = new LastBalanceObserver();
            var clicks = new SpentClicksObserver();
            game.corp.credits.Changed += balance.RememberBalance;
            game.corp.clicks.Changed += clicks.RememberSpent;
            var clickForCredit = game.corp.Acting.credit;
            SkipPaidWindow(game);
            SkipPaidWindow(game);

            await game.corp.Acting.TakeAction();
            await clickForCredit.Trigger();

            Assert.AreEqual(6, balance.LastBalance);
            Assert.AreEqual(1, clicks.Spent);
        }

        private void SkipPaidWindow(Game game)
        {
            game.corp.paidWindow.Pass();
            game.runner.paidWindow.Pass();
        }
    }
}
