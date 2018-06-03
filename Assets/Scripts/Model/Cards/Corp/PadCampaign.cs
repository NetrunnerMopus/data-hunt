using model.costs;
using model.cards.types;

namespace model.cards.corp
{
    public class PadCampaign : Card
    {
        override public string FaceupArt => "pad-campaign";
        override public string Name => "PAD Campaign";
        override public Faction Faction => Factions.SHADOW;
        override public int InfluenceCost => 0;
        override public ICost PlayCost => new CorpCreditCost(2);
        override public IEffect Activation => new effects.Nothing();
        override public IType Type => new Asset();
    }
}