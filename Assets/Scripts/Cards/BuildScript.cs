using costs;
using effects;
using effects.runner;

namespace cards
{
    public class BuildScript : ICard
    {
        string ICard.FaceupArt { get { return "build-script"; } }
        string ICard.Name { get { return "Build Script"; } }
        bool ICard.Faceup { get { return false; } }
        Faction ICard.Faction { get { return Factions.MASK; } }
        int ICard.InfluenceCost { get { return 1; } }
        ICost ICard.PlayCost { get { return new RunnerCreditCost(0); } }
        IEffect ICard.PlayEffect { get { return new Sequence(new Gain(1), new Draw(2), new SelfTrash(this)); } }
    }
}