using model.costs;
using model.cards.types;

namespace model.cards.corp
{
    public class PadCampaign : ICard
    {
        string ICard.FaceupArt => "pad-campaign";

        string ICard.Name => "PAD Campaign";

        bool ICard.Faceup => false;

        Faction ICard.Faction => Factions.SHADOW;

        int ICard.InfluenceCost => 0;

        ICost ICard.PlayCost => new CorpCreditCost(2);

        IEffect ICard.Activation => new effects.Nothing();

        IType ICard.Type => new Asset();
    }
}