using costs;
using effects;
using effects.runner;

namespace cards
{
    public class QualityTime : ICard
    {
        string ICard.FaceupArt { get { return "quality-time"; } }
        string ICard.Name { get { return "Quality Time"; } }
        bool ICard.Faceup { get { return false; } }
        Faction ICard.Faction { get { return Factions.SHAPER; } }
        int ICard.InfluenceCost { get { return 1; } }
        ICost ICard.PlayCost { get { return new RunnerCreditCost(3); } }
        IEffect ICard.PlayEffect { get { return new Sequence(new Draw(5), new SelfTrash(this)); } }
    }
}