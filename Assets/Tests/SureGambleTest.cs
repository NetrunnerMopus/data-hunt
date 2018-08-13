using NUnit.Framework;
using model;
using model.cards;
using System.Collections.Generic;
using model.cards.runner;
using System.Linq;
using tests.observers;
using tests.mocks;

namespace tests
{
    public class SureGambleTest
    {
        [Test]
        public void ShouldPlay()
        {
            var runnerCards = new List<Card>();
            for (int i = 0; i < 5; i++)
            {
                runnerCards.Add(new SureGamble());
            }
            var sureGamble = runnerCards.First();
            var game = new MockGames().WithRunnerCards(runnerCards);
            game.Start();
            new PassiveCorp(game).SkipTurn();
            var balance = new LastBalanceObserver();
            var clicks = new SpentClicksObserver();
            var grip = new GripObserver();
            var heap = new HeapObserver();
            game.runner.credits.Observe(balance);
            game.runner.clicks.Observe(clicks);
            game.runner.zones.grip.ObserveRemovals(grip);
            game.runner.zones.heap.Observe(heap);
            var play = game.runner.actionCard.Play(sureGamble);

            play.Trigger(game);

            Assert.AreEqual(9, balance.LastBalance);
            Assert.AreEqual(1, clicks.Spent);
            Assert.AreEqual(sureGamble, grip.LastRemoved);
            Assert.AreEqual(sureGamble, heap.LastAdded);
        }
    }
}