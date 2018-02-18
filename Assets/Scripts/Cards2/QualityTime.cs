using costs;
using effects;
using effects.runner;

namespace cards
{
    public class QualityTime : ICard2
    {
        string ICard2.FaceupArt { get { return "quality-time"; } }
        string ICard2.Name { get { return "Quality Time"; } }
        bool ICard2.Faceup { get { return false; } }
        Faction ICard2.Faction { get { return Factions.SHAPER; } }
        int ICard2.InfluenceCost { get { return 1; } }
        ICost ICard2.PlayCost { get { return new RunnerCreditCost(3); } }
        IEffect ICard2.PlayEffect { get { return new Sequence(new Draw(5), new SelfTrash(this)); } }
    }
}