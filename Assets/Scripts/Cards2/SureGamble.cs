using costs;
using effects;
using effects.runner;

namespace cards
{
    public class SureGamble : ICard
    {
        string ICard.FaceupArt { get { return "sure-gamble"; } }
        string ICard.Name { get { return "Sure Gamble"; } }
        bool ICard.Faceup { get { return false; } }
        Faction ICard.Faction { get { return Factions.MASK; } }
        int ICard.InfluenceCost { get { return 0; } }
        ICost ICard.PlayCost { get { return new RunnerCreditCost(5); } }
        IEffect ICard.PlayEffect { get { return new Sequence(new Gain(9), new SelfTrash(this)); } }
    }
}