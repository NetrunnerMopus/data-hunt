using model.costs;
using model.cards.types;
using model.effects.corp;

namespace model.cards.corp
{
    public class HedgeFund : ICard
    {
        string ICard.FaceupArt => "hedge-fund";

        string ICard.Name => "Hedge Fund";

        bool ICard.Faceup => false;

        Faction ICard.Faction => Factions.SHADOW;

        int ICard.InfluenceCost => 0;

        ICost ICard.PlayCost => new CorpCreditCost(5);

        IEffect ICard.Activation => new Gain(9);

        IType ICard.Type => new Operation();
    }
}