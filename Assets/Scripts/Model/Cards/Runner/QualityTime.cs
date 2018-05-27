using model.effects.runner;
using model.costs;
using model.effects;
using model.cards.types;

namespace model.cards.runner
{
    public class QualityTime : ICard
    {
        string ICard.FaceupArt { get { return "quality-time"; } }

        string ICard.Name { get { return "Quality Time"; } }

        bool ICard.Faceup { get { return false; } }

        Faction ICard.Faction { get { return Factions.SHAPER; } }

        int ICard.InfluenceCost { get { return 1; } }

        ICost ICard.PlayCost { get { return new RunnerCreditCost(3); } }

        IEffect ICard.Activation => new Draw(5);

        IType ICard.Type { get { return new Event(); } }
    }
}