using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using model.cards.types;
using model.choices.trash;
using model.play;
using model.timing;
using model.timing.corp;

namespace model.cards.corp
{
    public class PadCampaign : Card
    {
        public PadCampaign(Game game) : base(game) { }
        override public string FaceupArt => "pad-campaign";
        override public string Name => "PAD Campaign";
        override public Faction Faction => Factions.SHADOW;
        override public int InfluenceCost => 0;
        override public ICost PlayCost => game.corp.credits.PayingForPlaying(this, 2);
        override public IEffect Activation => new PadCampaignActivation(this, game);
        override public IType Type => new Asset(game);
        override public IList<ITrashOption> TrashOptions() => new List<ITrashOption> {
            new Leave(),
            new PayToTrash(4, this, game)
        };

        private class PadCampaignActivation : IEffect
        {
            public bool Impactful => true;
            public event Action<IEffect, bool> ChangedImpact = delegate { };
            IEnumerable<string> IEffect.Graphics => new string[] { };
            private CardAbility drip;
            private Game game;
            private IList<CorpDrawPhase> phases = new List<CorpDrawPhase>();

            public PadCampaignActivation(PadCampaign padCampaign, Game game)
            {
                this.game = game;
                this.drip = game.corp.credits.Gaining(1).ToMandatoryAbility().BelongingTo(padCampaign);
            }

            async Task IEffect.Resolve()
            {
                game.Timing.CorpTurnDefined += RegisterDrip;
                await Task.CompletedTask;
            }

            private void RegisterDrip(CorpTurn turn)
            {
                var phase = turn.drawPhase;
                phases.Add(phase);
                phase.TurnBegins.Add(drip);
            }

            void IEffect.Disable()
            {
                game.Timing.CorpTurnDefined -= RegisterDrip;
                foreach (var phase in phases)
                {
                    phase.TurnBegins.Remove(drip);
                }
            }
        }
    }
}
