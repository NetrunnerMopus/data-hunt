using model.cards;
using System.Collections.Generic;

namespace model.effects.corp
{
    public class Play : IEffect
    {
        private Card card;

        public Play(Card card)
        {
            this.card = card;
        }

        void IEffect.Resolve(Game game)
        {
            card.FlipFaceUp();
            game.corp.zones.hq.Zone.Remove(card);
            card.Activation.Resolve(game);
            game.corp.zones.archives.Add(card);
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