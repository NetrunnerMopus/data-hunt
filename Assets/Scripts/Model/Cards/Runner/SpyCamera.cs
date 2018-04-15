using model.effects.runner;
using model.costs;
using model.cards.types;

namespace model.cards.runner
{
    public class SpyCamera : ICard
    {
        string ICard.FaceupArt { get { return "spy-camera"; } }

        string ICard.Name { get { return "Spy Camera"; } }

        bool ICard.Faceup { get { return false; } }

        Faction ICard.Faction { get { return Factions.CRIMINAL; } }

        int ICard.InfluenceCost { get { return 1; } }

        ICost ICard.PlayCost { get { return new RunnerCreditCost(0); } }

        IEffect ICard.PlayEffect { get { return new Install(this); } }

        IType ICard.Type { get { return new Hardware(); } }
    }
}