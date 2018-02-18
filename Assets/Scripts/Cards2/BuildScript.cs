using costs;
using effects;
using effects.runner;

namespace cards
{
    public class BuildScript : ICard2
    {
        string ICard2.FaceupArt { get { return "build-script"; } }
        string ICard2.Name { get { return "Build Script"; } }
        bool ICard2.Faceup { get { return false; } }
        Faction ICard2.Faction { get { return Factions.MASK; } }
        int ICard2.InfluenceCost { get { return 1; } }
        ICost ICard2.PlayCost { get { return new RunnerCreditCost(0); } }
        IEffect ICard2.PlayEffect { get { return new Sequence(new Gain(1), new Draw(2), new SelfTrash(this)); } }
    }
}