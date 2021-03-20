using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using model.cards.types;
using model.choices.trash;

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
        override public IEffect Activation => new PadCampaignActivation(game.corp);
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
            private Corp corp;

            public PadCampaignActivation(Corp corp) => this.corp = corp;

            async Task IEffect.Resolve()
            {
                corp.turn.WhenBegins(corp.credits.Gaining(1));
                await Task.CompletedTask;
            }
        }
    }
}
