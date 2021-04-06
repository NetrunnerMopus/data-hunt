using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using model.cards.types;
using model.choices.trash;
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
        override public IEffect Activation => new PadCampaignActivation(game);
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
            private Game game;

            public PadCampaignActivation(Game game) => this.game = game;

            async Task IEffect.Resolve()
            {
                game.Timing.CorpTurnDefined += RegisterDrip;
                await Task.CompletedTask;
            }

            private void RegisterDrip(CorpTurn turn)
            {
                turn.WhenBegins(game.corp.credits.Gaining(1));
            }
        }
    }
}
