using costs;
using effects;

namespace cards2
{
    public class SureGamble : ICard2
    {
        string ICard2.FaceupArt { get { return "sure-gamble"; } }
        string ICard2.Name { get { return "Sure Gamble"; } }
        bool ICard2.Faceup { get { return true; } }
        Faction ICard2.Faction { get { return Factions.MASK; } }
        ICost ICard2.PlayCost { get { return new RunnerCreditCost(5); } }
        IEffect ICard2.PlayEffect { get { return new RunnerCreditGain(9); } }
    }
}