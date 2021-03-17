using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using model.cards;
using model.zones;

namespace model.effects.runner
{
    public class Play : IEffect
    {
        public bool Impactful => card.Activation.Impactful;
        public event Action<IEffect, bool> ChangedImpact = delegate { };
        private Card card;
        private Zone playZone = new Zone("Play");
        IEnumerable<string> IEffect.Graphics => new string[] { "Images/Cards/" + card.FaceupArt };

        public Play(Card card)
        {
            this.card = card;
            card.Activation.ChangedImpact += ChangedImpact;
        }

        async Task IEffect.Resolve(Game game)
        {
            card.MoveTo(playZone);
            await card.Activate(game);
            card.Deactivate();
            card.MoveTo(game.runner.zones.heap.zone);
        }
    }
}
