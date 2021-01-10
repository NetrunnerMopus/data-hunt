using model.cards.types;

namespace model.cards.corp
{
    public class TheShadow : Card
    {
        override public string FaceupArt => "the-shadow";
        override public string Name => "The Shadow";
        override public Faction Faction => Factions.SHADOW;
        override public int InfluenceCost { get { throw new System.Exception("Identities don't have an influence cost"); } }
        override public ICost PlayCost { get { throw new System.Exception("Identities don't have a play cost"); } }
        override public IEffect Activation => new effects.Nothing();
        override public IType Type => new Identity(); 
    }
}