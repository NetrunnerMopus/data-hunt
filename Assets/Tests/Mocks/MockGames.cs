using System.Collections.Generic;
using model;
using model.cards;
using model.cards.runner;
using model.player;
using model.zones;

namespace tests.mocks
{
    public class MockGames
    {
        private MockGames() { }

        public static Game Unpiloted() => new Game(new NoPilot(), new NoPilot(), new Shuffling());
        public static Game StartUnpiloted()
        {
            var game = Unpiloted();
            game.Start(Decks.DemoCorp(game), Decks.DemoRunner(game));
            return game;
        }

        public static Deck MasqueDeck(Game game, List<Card> runnerCards) => new Deck(runnerCards, new TheMasque(game));
    }
}
