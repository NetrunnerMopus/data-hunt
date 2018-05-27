using model.costs;
using model.cards.types;

namespace model.cards.runner
{
    public class Mongoose : ICard
    {
        string ICard.FaceupArt { get { return "mongoose"; } }

        string ICard.Name { get { return "Mongoose"; } }

        bool ICard.Faceup { get { return false; } }

        Faction ICard.Faction { get { return Factions.CRIMINAL; } }

        int ICard.InfluenceCost { get { return 2; } }

        ICost ICard.PlayCost { get { return new RunnerCreditCost(3); } }

        IEffect ICard.Activation => new effects.Nothing();

        IType ICard.Type { get { return new Program(); } }
    }
}