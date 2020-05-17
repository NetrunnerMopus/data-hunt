using System.Collections.Generic;
using System.Threading.Tasks;
using model.cards;
using model.player;
using model.timing;

namespace model.zones.corp
{
    public class ResearchAndDevelopment : IServer
    {
        public Zone Zone { get; } = new Zone("R&D");
        public IceColumn Ice { get; }
        private Shuffling shuffling;
        private Game game;
        private bool reshuffledDuringAccess = false;

        public ResearchAndDevelopment(Game game, Deck deck, Shuffling shuffling)
        {
            this.game = game;
            this.shuffling = shuffling;
            Ice = new IceColumn(game);
            foreach (var card in deck.cards)
            {
                card.MoveTo(Zone);
            }
        }

        public void Shuffle()
        {
            shuffling.Shuffle(Zone.Cards);
            reshuffledDuringAccess = true;
        }

        public bool HasCards() => Zone.Cards.Count > 0;

        public void Draw(int cards, Headquarters hq)
        {
            for (int i = 0; i < cards; i++)
            {
                if (HasCards())
                {
                    Zone.Cards[0].MoveTo(hq.Zone);
                }
            }
        }

        async Task IServer.Access(int accessCount, IPilot pilot, Game game)
        {
            reshuffledDuringAccess = false;
            var accessDepth = 0;
            for (var accessesLeft = accessCount; accessesLeft > 0; accessesLeft--)
            {
                if (accessDepth > Zone.Cards.Count)
                {
                    break;
                }
                var nextCard = Zone.Cards[accessDepth];
                // TODO show top of R&D and cards in root
                // TODO2: should draw facedown
                // TODO3: nice if we could display the pile, not just the top+root
                var cardToAccess = await pilot.ChooseACard().Declare("Which card to access now?", new List<Card> { nextCard }, game);
                accessDepth++;
                await new AccessCard(cardToAccess, game).AwaitEnd(); // TODO show the already seen on the side, maintaining proper order CR: 7.2.1.b
                if (reshuffledDuringAccess) // CR: 7.2.1.c
                {
                    accessDepth = 0;
                    reshuffledDuringAccess = false;
                }
            }
        }
    }
}