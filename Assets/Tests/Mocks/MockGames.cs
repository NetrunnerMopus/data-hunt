using model;
using model.cards;
using model.cards.runner;
using model.player;
using System.Collections.Generic;

namespace tests.mocks
{
    public class MockGames
    {
        public Game WithRunnerCards(List<Card> cards)
        {
            return new Game(
                corpPlayer: new Player(
                    deck: new Decks().DemoCorp(),
                    pilot: new NoPilot()
                ),
                runnerPlayer: new Player(
                    deck: new Deck(cards, new TheMasque()),
                    pilot: new NoPilot()
                )
            );
        }
    }
}
