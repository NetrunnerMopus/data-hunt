using model.costs;
using model.cards.types;
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

        private class PadCampaignActivation : IEffect
        {
            void IEffect.Resolve(Game game)
            {
                game.flow.corpTurn.turnBeginningTriggers.Add(new Gain(1));
            }
            void IEffect.Observe(IImpactObserver observer, Game game) { }
        }
    }
}