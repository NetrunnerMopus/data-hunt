using NUnit.Framework;
using model;
using model.cards;
using System.Collections.Generic;
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
            var game = new MockGames().WithRunnerCards(new List<Card>());
            var gameFlowLog = new GameFlowLog();
            gameFlowLog.Display(game);
            game.Start();
            var balance = new LastBalanceObserver();
            var clicks = new SpentClicksObserver();
            game.corp.credits.Observe(balance);
            game.corp.clicks.Observe(clicks);
            var clickForCredit = game.corp.actionCard.credit;
            SkipPaidWindow(game);
            SkipPaidWindow(game);

            await game.corp.actionCard.TakeAction();
            await clickForCredit.Trigger(game);

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