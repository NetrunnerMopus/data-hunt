using model.costs;
using model.cards.types;

namespace model.cards.runner
{
    public class SpyCamera : Card
    {
        override public string FaceupArt { get { return "spy-camera"; } }
        override public string Name { get { return "Spy Camera"; } }
        override public Faction Faction { get { return Factions.CRIMINAL; } }
        override public int InfluenceCost { get { return 1; } }
        override public ICost PlayCost { get { return new RunnerCreditCost(0); } }
        override public IEffect Activation => new effects.Nothing();
        override public IType Type { get { return new Hardware(); } }
    }
}