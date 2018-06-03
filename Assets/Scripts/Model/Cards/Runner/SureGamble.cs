using model.effects.runner;
using model.costs;
using model.effects;
using model.cards.types;

namespace model.cards.runner
{
    public class SureGamble : Card
    {
        override public string FaceupArt { get { return "sure-gamble"; } }
        override public string Name { get { return "Sure Gamble"; } }
        override public Faction Faction { get { return Factions.MASK; } }
        override public int InfluenceCost { get { return 0; } }
        override public ICost PlayCost { get { return new RunnerCreditCost(5); } }
        override public IEffect Activation => new Gain(9);
        override public IType Type { get { return new Event(); } }
    }
}