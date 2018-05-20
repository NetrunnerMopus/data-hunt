using NUnit.Framework;
using model;
using model.cards;
using System.Collections.Generic;
using model.cards.runner;
using tests.observers;

namespace tests
{
    public class RunnerDiscardPhaseTest
    {
        [Test]
        public void ShouldDiscard()
        {
            var runnerCards = new List<ICard>();
            for (int i = 0; i < 20; i++)
            {
                runnerCards.Add(new Diesel());
            }
            var game = new Game(new Decks().DemoCorp(), new Deck(runnerCards));
            game.Start();
            SkipCorpTurn(game);
            var grip = game.runner.zones.grip;
            var heap = game.runner.zones.heap;
            var actionCard = game.runner.actionCard;
            var gripObserver = new GripObserver();
            var heapObserver = new HeapObserver();
            var clicksObserver = new SpentClicksObserver();
            grip.ObserveRemovals(gripObserver);
            heap.Observe(heapObserver);
            game.runner.clicks.Observe(clicksObserver);
            for (int i = 0; i < 3; i++)
            {
                var diesel = grip.Find<Diesel>();
                actionCard.Play(diesel).Trigger(game);
            }
            actionCard.draw.Trigger(game);

            for (int i = 0; i < 7; i++)
            {
                var card = grip.Find<Diesel>();
                game.runner.zones.grip.Discard(card, heap);
            }
            SkipCorpTurn(game);

            Assert.AreEqual(0, clicksObserver.Spent);
            Assert.AreEqual(10, gripObserver.TotalRemoved);
            Assert.AreEqual(10, heapObserver.TotalAdded);
        }

        private void SkipCorpTurn(Game game)
        {
            var clickForCredit = game.corp.actionCard.credit;
            for (int i = 0; i < 3; i++)
            {
                clickForCredit.Trigger(game);
            }
        }
    }
}