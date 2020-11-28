using model.cards.types;
using model.costs;
using model.effects;

namespace model.cards.corp
{
    public class IceWall : Card
    {
        override public string FaceupArt => "ice-wall";
        override public string Name => "Ice Wall";
        override public Faction Faction => Factions.WEYLAND_CONSORTIUM;
        override public int InfluenceCost => 1;
        override public ICost PlayCost => new CorpCreditCost(1);
        override public IEffect Activation => new Pass();
        override public IType Type => new Ice();
    }
}
