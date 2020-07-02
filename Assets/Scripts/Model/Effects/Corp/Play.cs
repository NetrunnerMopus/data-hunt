using model.cards;
using model.zones;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.effects.corp
{
    public class Play : IEffect
    {
        private Card card;
        private Zone playZone = new Zone("Play");

        IEnumerable<string> IEffect.Graphics  => new string[] { "Images/Cards/" + card.FaceupArt };

        public Play(Card card)
        {
            this.card = card;
        }

        async Task IEffect.Resolve(Game game)
        {
            card.FlipFaceUp();
            card.MoveTo(playZone);
            await card.Activate(game);
            card.Deactivate();
            card.MoveTo(game.corp.zones.archives.Zone);
        }

        void IEffect.Observe(IImpactObserver observer, Game game)
        {
            card.Activation.Observe(observer, game);
        }

        public override bool Equals(object obj)
        {
            var play = obj as Play;
            return play != null &&
                   EqualityComparer<Card>.Default.Equals(card, play.card);
        }

        public override int GetHashCode()
        {
            return -964371657 + EqualityComparer<Card>.Default.GetHashCode(card);
        }

        public override string ToString() => "Play(card=" + card + ")";
    }
}
