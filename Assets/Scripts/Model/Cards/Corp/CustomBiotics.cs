using model.cards.types;

namespace model.cards.corp
{
    public class CustomBiotics : Card
    {
        public CustomBiotics(Game game) : base(game) { }
        override public string FaceupArt => "custom-biotics";
        override public string Name => "Custom Biotics";
        override public Faction Faction => Factions.HAAS_BIOROID;
        override public int InfluenceCost { get { throw new System.Exception("Identities don't have an influence cost"); } }
        override public ICost PlayCost { get { throw new System.Exception("Identities don't have a play cost"); } }
        override public IEffect Activation => new effects.Nothing();
        override public IType Type => new Identity();
    }
}
