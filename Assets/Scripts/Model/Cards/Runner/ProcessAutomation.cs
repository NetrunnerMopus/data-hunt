using model.effects.runner;
using model.costs;
using model.effects;
using model.cards.types;

namespace model.cards.runner
{
    public class ProcessAutomation : Card
    {
        override public string FaceupArt { get { return "process-automation"; } }
        override public string Name { get { return "Process Automation"; } }
        override public Faction Faction { get { return Factions.MASQUE; } }
        override public int InfluenceCost { get { return 1; } }
        override public ICost PlayCost { get { return new RunnerCreditCost(0); } }
        override public IEffect Activation => new Sequence(new Gain(2), new Draw(1));
        override public IType Type { get { return new Event(); } }
    }
}