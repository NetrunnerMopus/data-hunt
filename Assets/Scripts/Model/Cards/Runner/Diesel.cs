using model.effects.runner;
using model.costs;
using model.effects;
using model.cards.types;

namespace model.cards.runner
{
    public class Diesel : ICard
    {
        string ICard.FaceupArt { get { return "diesel"; } }

        string ICard.Name { get { return "Diesel"; } }

        bool ICard.Faceup { get { return false; } }

        Faction ICard.Faction { get { return Factions.SHAPER; } }

        int ICard.InfluenceCost { get { return 2; } }

        ICost ICard.PlayCost => new RunnerCreditCost(0);

        IEffect ICard.Activation => new Draw(3);

        IType ICard.Type { get { return new Event(); } }
    }
}