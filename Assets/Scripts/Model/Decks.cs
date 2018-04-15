using System.Collections.Generic;
using model.cards;
using model.cards.runner;

namespace model
{
    public class Decks
    {
        public Deck DemoRunner()
        {
            var cards = new List<ICard>();
            for (int i = 0; i < 4; i++)
            {
                cards.Add(new Diesel());
                cards.Add(new QualityTime());
                cards.Add(new SureGamble());
                cards.Add(new BuildScript());
                cards.Add(new ProcessAutomation());
                cards.Add(new Mongoose());
            }
            return new Deck(cards);
        }
    }
}