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
            var cards = new List<Card>();
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
                cards.Add(new SportsHopper());
            }
            return new Deck(cards, new OmarKeung(), 10006);
        }

        public Deck DemoCorp()
        {
            var cards = new List<Card>();
            for (int i = 0; i < 10; i++)
            {
                cards.Add(new HedgeFund());
                cards.Add(new PadCampaign());
                cards.Add(new AdvancedAssemblyLines());
            }
            return new Deck(cards, new TheShadow(), 1234);
        }
    }
}