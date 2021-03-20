using System.Collections.Generic;
using System.Linq;
using model;
using model.cards;
using model.cards.runner;
using NUnit.Framework;
using tests.mocks;
using tests.observers;

namespace tests
{
    public class SureGambleTest
    {
        [Test]
        async public void ShouldPlay()
        {
            var game = MockGames.Unpiloted();
            var runnerCards = new List<Card>();
            for (int i = 0; i < 5; i++)
            {
                runnerCards.Add(new SureGamble(game));
            }
            var sureGamble = runnerCards.First();
            game.Start(Decks.DemoCorp(game), MockGames.MasqueDeck(game, runnerCards));
            await new PassiveCorp(game).SkipTurn();
            var grip = new ZoneObserver( game.runner.zones.grip.zone);
            var heap = new ZoneObserver( game.runner.zones.heap.zone);

            await game.runner.Acting.TakeAction();
            await game.runner.Acting.Play(sureGamble).Trigger();

            Assert.AreEqual(9, game.runner.credits.Balance);
            Assert.AreEqual(1, game.runner.clicks.Spent);
            Assert.AreEqual(sureGamble, grip.LastRemoved);
            Assert.AreEqual(sureGamble, heap.LastAdded);
        }
    }
}
