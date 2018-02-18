using costs;
using effects;
using effects.runner;

namespace cards
{
    public class SureGamble : ICard2
    {
        string ICard2.FaceupArt { get { return "sure-gamble"; } }
        string ICard2.Name { get { return "Sure Gamble"; } }
        bool ICard2.Faceup { get { return false; } }
        Faction ICard2.Faction { get { return Factions.MASK; } }
        int ICard2.InfluenceCost { get { return 0; } }
        ICost ICard2.PlayCost { get { return new RunnerCreditCost(5); } }
        IEffect ICard2.PlayEffect { get { return new Sequence(new Gain(9), new SelfTrash(this)); } }
    }
}