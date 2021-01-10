using model.costs;
using model.cards.types;
using model.effects.corp;

namespace model.cards.corp
{
    public class HedgeFund : Card
    {
        override public string FaceupArt => "hedge-fund";
        override public string Name => "Hedge Fund";
        override public Faction Faction => Factions.SHADOW;
        override public int InfluenceCost => 0;
        override public ICost PlayCost => new CorpCreditCost(5);
        override public IEffect Activation => new Gain(9);
        override public IType Type => new Operation(); 
    }
}