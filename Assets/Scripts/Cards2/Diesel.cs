using costs;
using effects;
using effects.runner;

namespace cards2
{
    public class Diesel : ICard2
    {
        string ICard2.FaceupArt { get { return "diesel"; } }
        string ICard2.Name { get { return "Diesel"; } }
        bool ICard2.Faceup { get { return false; } }
        Faction ICard2.Faction { get { return Factions.SHAPER; } }
        int ICard2.InfluenceCost { get { return 2; } }
        ICost ICard2.PlayCost { get { return new RunnerCreditCost(0); } }
        IEffect ICard2.PlayEffect { get { return new Sequence(new Draw(3), new Trash(this)); } }
    }
}