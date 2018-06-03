using model.effects.runner;
using model.costs;
using model.effects;
using model.cards.types;

namespace model.cards.runner
{
    public class QualityTime : Card
    {
        override public string FaceupArt { get { return "quality-time"; } }
        override public string Name { get { return "Quality Time"; } }
        override public Faction Faction { get { return Factions.SHAPER; } }
        override public int InfluenceCost { get { return 1; } }
        override public ICost PlayCost { get { return new RunnerCreditCost(3); } }
        override public IEffect Activation => new Draw(5);
        override public IType Type { get { return new Event(); } }
    }
}