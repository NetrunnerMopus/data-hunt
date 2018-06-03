using model.cards.types;

namespace model.cards.corp
{
    public class CustomBiotics : Card
    {
        override public string FaceupArt => "custom-biotics";
        override public string Name => "Custom Biotics";
        override public Faction Faction => Factions.HAAS_BIOROID;
        override public int InfluenceCost => 0;
        override public ICost PlayCost => new costs.Nothing();
        override public IEffect Activation => new effects.Nothing();
        override public IType Type => new Identity();
    }
}