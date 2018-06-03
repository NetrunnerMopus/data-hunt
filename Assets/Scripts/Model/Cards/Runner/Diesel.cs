using model.effects.runner;
using model.costs;
using model.effects;
using model.cards.types;

namespace model.cards.runner
{
    public class Diesel : Card
    {
        override public string FaceupArt { get { return "diesel"; } }
        override public string Name { get { return "Diesel"; } }
        override public Faction Faction { get { return Factions.SHAPER; } }
        override public int InfluenceCost { get { return 2; } }
        override public ICost PlayCost => new RunnerCreditCost(0);
        override public IEffect Activation => new Draw(3);
        override public IType Type { get { return new Event(); } }
    }
}