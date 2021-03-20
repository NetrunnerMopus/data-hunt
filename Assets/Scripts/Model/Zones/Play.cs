using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using model.cards;

namespace model.zones
{
    internal class Play : IEffect
    {
        private Card card;
        private Zone bin;
        private Zone playZone = new Zone("Play");
        public bool Impactful => card.Activation.Impactful;
        public event Action<IEffect, bool> ChangedImpact = delegate { };
        IEnumerable<string> IEffect.Graphics => new string[] { "Images/Cards/" + card.FaceupArt };

        public Play(Card card, Zone bin)
        {
            if (!card.Type.Playable)
            {
                throw new Exception("Cannot play an unplayable card " + card);
            }
            this.card = card;
            this.bin = bin;
            card.Activation.ChangedImpact += ChangedImpact;
        }

        async Task IEffect.Resolve()
        {
            card.FlipFaceUp();
            card.MoveTo(playZone);
            await card.Activate();
            card.Deactivate();
            card.MoveTo(bin);
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
