using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using model.cards.types;
using model.choices.trash;
using model.effects.corp;

namespace model.cards.corp
{
    public class PadCampaign : Card
    {
        public PadCampaign(Game game) : base(game) { }
        override public string FaceupArt => "pad-campaign";
        override public string Name => "PAD Campaign";
        override public Faction Faction => Factions.SHADOW;
        override public int InfluenceCost => 0;
        override public ICost PlayCost => game.Costs.Rez(this, 2);
        override public IEffect Activation => new PadCampaignActivation();
        override public IType Type => new Asset();
        override public IList<ITrashOption> TrashOptions(Game game) => new List<ITrashOption> {
            new Leave(),
            new PayToTrash(game.Costs.Trash(this, 4), this)
        };

        private class PadCampaignActivation : IEffect
        {
            public bool Impactful => true;
            public event Action<IEffect, bool> ChangedImpact = delegate { };
            IEnumerable<string> IEffect.Graphics => new string[] { };
            async Task IEffect.Resolve(Game game)
            {
                game.corp.turn.WhenBegins(new Gain(1));
                await Task.CompletedTask;
            }
        }
    }
}
