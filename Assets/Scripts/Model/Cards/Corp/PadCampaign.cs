using System.Collections.Generic;
using System.Threading.Tasks;
using model.cards.types;
using model.choices.trash;
using model.costs;
using model.effects.corp;

namespace model.cards.corp
{
    public class PadCampaign : Card
    {

        override public string FaceupArt => "pad-campaign";
        override public string Name => "PAD Campaign";
        override public Faction Faction => Factions.SHADOW;
        override public int InfluenceCost => 0;
        override public ICost PlayCost => new CorpCreditCost(2);
        override public IEffect Activation => new PadCampaignActivation();
        override public IType Type => new Asset();
        override public IEnumerable<ITrashOption> TrashOptions(Game game) => new ITrashOption[] {
            new Leave(),
            new PayToTrash(new RunnerCreditCost(4), this)
        };

        private class PadCampaignActivation : IEffect
        {
            IEnumerable<string> IEffect.Graphics => new string[] {};
            async Task IEffect.Resolve(Game game)
            {
               game.corp.turn.WhenBegins(new Gain(1));
               await Task.CompletedTask;
            }
            void IEffect.Observe(IImpactObserver observer, Game game) { }
        }
    }
}
