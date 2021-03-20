using System.Collections.Generic;
using model.cards;
using model.cards.corp;
using model.cards.runner;

namespace model
{
    public class Decks
    {
        private Decks() { }

        public static Deck DemoRunner(Game game)
        {
            var cards = new List<Card>();
            for (int i = 0; i < 3; i++)
            {
                cards.Add(new Diesel(game));
                cards.Add(new QualityTime(game));
                cards.Add(new SureGamble(game));
                cards.Add(new BuildScript(game));
                cards.Add(new ProcessAutomation(game));
                cards.Add(new Mongoose(game));
                cards.Add(new SpyCamera(game));
                cards.Add(new Wyldside(game));
                cards.Add(new SportsHopper(game));
            }
            return new Deck(cards, new OmarKeung(game));
        }

        public static Deck DemoCorp(Game game)
        {
            var cards = new List<Card>();
            for (int i = 0; i < 5; i++)
            {
                cards.Add(new HedgeFund(game));
                cards.Add(new PadCampaign(game));
                cards.Add(new AdvancedAssemblyLines(game));
                cards.Add(new CorporateSalesTeam(game));
                cards.Add(new Bellona(game));
            }
            for (int i = 0; i < 2; i++)
            {
                cards.Add(new AnonymousTip(game));
                cards.Add(new VanityProject(game));
            }
            return new Deck(cards, new HaarpsichordStudios(game));
        }
    }
}
