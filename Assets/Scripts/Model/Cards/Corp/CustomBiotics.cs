using model.cards.types;

namespace model.cards.corp
{
    public class CustomBiotics : ICard
    {
        string ICard.FaceupArt => "custom-biotics";
        string ICard.Name => "Custom Biotics";
        bool ICard.Faceup => true;
        Faction ICard.Faction => Factions.HAAS_BIOROID;
        int ICard.InfluenceCost => 0;
        ICost ICard.PlayCost => new costs.Nothing();
        IEffect ICard.Activation => new effects.Nothing();
        IType ICard.Type => new Identity();
    }
}