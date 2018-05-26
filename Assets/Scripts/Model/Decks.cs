using System.Collections.Generic;
using model.cards;
using model.cards.corp;
using model.cards.runner;

namespace model
{
    public class Decks
    {
        public Deck DemoRunner()
        {
            var cards = new List<ICard>();
            for (int i = 0; i < 3; i++)
            {
                cards.Add(new Diesel());
                cards.Add(new QualityTime());
                cards.Add(new SureGamble());
                cards.Add(new BuildScript());
                cards.Add(new ProcessAutomation());
                cards.Add(new Mongoose());
                cards.Add(new SpyCamera());
                cards.Add(new Wyldside());
            }
            return new Deck(cards, 10006);
        }

        public Deck DemoCorp()
        {
            var cards = new List<ICard>();
            for (int i = 0; i < 9; i++)
            {
                cards.Add(new HedgeFund());
            }
            return new Deck(cards, 1234);
        }
    }
}