using model.effects.runner;
using model.costs;
using model.effects;
using model.cards.types;

namespace model.cards.runner
{
    public class BuildScript : Card
    {
        override public string FaceupArt { get { return "build-script"; } }
        override public string Name { get { return "Build Script"; } }
        override public Faction Faction { get { return Factions.MASK; } }
        override public int InfluenceCost { get { return 1; } }
        override public ICost PlayCost { get { return new RunnerCreditCost(0); } }
        override public IEffect Activation => new Sequence(new Gain(1), new Draw(2));
        override public IType Type { get { return new Event(); } }
    }
}